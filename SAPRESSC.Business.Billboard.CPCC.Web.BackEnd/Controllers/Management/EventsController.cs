using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Common;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.SecurityShared;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.Filter;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Context;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Facades.Manager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Controllers.Management
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Area("cpccbillboard")]
    public class EventsController : Core.Web.Management.Controllers.AbstractHabilitableEntityManagementFilteredController<Event, Guid, EventInputViewModel, EventDisplayViewModel, EventFilterModel>
    {
        private const string POSTER_IMAGE_FILE_FIELD_NAME = "PosterImageFile";
        private const string PROMO_VIDEO_FILE_FIELD_NAME = "PromoVideoFile";

        private readonly Microsoft.Extensions.Options.IOptions<FfmpegOptions> ffmpegOptions;

        private readonly IEntityRepository<Thumbnail, Guid> thumbnailRepository;
        private readonly IEntityRepository<Video, Guid> videoRepository;
        private readonly IEntityRepository<Image, Guid> imageRepository;
        private readonly IMediaFileManagerFacade mediaFileManagerFacade;
        private readonly IEntityRepository<Personality, long> personalityRepository;
        private readonly Microsoft.EntityFrameworkCore.DbContext dbContext;

        public EventsController(Microsoft.Extensions.Options.IOptions<FfmpegOptions> ffmpegOptions, IEntityRepository<Thumbnail, Guid> thumbnailRepository, IEntityRepository<Video, Guid> videoRepository, Microsoft.EntityFrameworkCore.DbContext dbContext, IEntityRepository<Personality, long> personalityRepository, IMediaFileManagerFacade mediaFileManagerFacade, IEntityRepository<Image, Guid> imageRepository, IEntityRepository<Event, Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<EventsController> localizer, ILogger<EventsController> logger) : base(repository, localizer, logger)
        {
            this.ffmpegOptions = ffmpegOptions;
            this.thumbnailRepository = thumbnailRepository;
            this.videoRepository = videoRepository;
            this.imageRepository = imageRepository;
            this.mediaFileManagerFacade = mediaFileManagerFacade;
            this.personalityRepository = personalityRepository;
            this.dbContext = dbContext;
        }

        protected override string GetFilterSessionID()
        {
            return "cpccbillboard-events-management-filter";
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.EventManagerViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.EventManagerViewer)]
        public override IActionResult Filter(EventFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.EventManagerCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.EventManagerCreator)]
        public override IActionResult Create(EventInputViewModel modelInput)
        {
            if (Request.Form.Files.Count > 0)
            {
                var formPosterImageFile = Request.Form.Files.First(f => f.Name == POSTER_IMAGE_FILE_FIELD_NAME);

                var validationRequired = false;

                if (formPosterImageFile.Length > 0)
                {
                    ModelState.ClearValidationState(POSTER_IMAGE_FILE_FIELD_NAME);

                    modelInput.PosterImageFile = formPosterImageFile;

                    validationRequired = true;
                }

                var formPromoVideoFile = Request.Form.Files.FirstOrDefault(f => f.Name == PROMO_VIDEO_FILE_FIELD_NAME);

                if (formPromoVideoFile != null && formPromoVideoFile.Length > 0)
                {
                    modelInput.PromoVideoFile = formPromoVideoFile;

                    validationRequired = true;
                }

                if (validationRequired) TryUpdateModelAsync(modelInput).Wait();
            }

            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetInvalidMessage()
            };

            ValidateCreate(modelInput);

            try
            {
                if (ModelState.IsValid)
                {
                    var entity = modelInput.Export();

                    PreCreate(entity, modelInput);

                    if (modelInput.PromoVideoFile != null)
                    {
                        using (var memoryStream = new System.IO.MemoryStream())
                        {
                            modelInput.PromoVideoFile.CopyTo(memoryStream);

                            var extension = System.IO.Path.GetExtension(modelInput.PromoVideoFile.FileName);

                            var thumbnailId = Guid.NewGuid();

                            var videoId = Guid.NewGuid();

                            mediaFileManagerFacade.SaveVideoFileContent(videoId, memoryStream.ToArray(), extension);

                            mediaFileManagerFacade.EnsureThumbnailsDirectoryExist();

                            try
                            {
                                var videoMediaFile = new MediaToolkit.Model.MediaFile(mediaFileManagerFacade.GetVideoFilePath(videoId, extension));

                                var thumbnailMediaFile = new MediaToolkit.Model.MediaFile(mediaFileManagerFacade.GetThumbnailFilePath(thumbnailId, Cinema.Web.Common.Constants.VIDEO_THUMBNAIL_EXTENSION));

                                using (var engine = new MediaToolkit.Engine(ffmpegOptions.Value.Path))
                                {
                                    engine.GetMetadata(videoMediaFile);

                                    var resolutionParts = videoMediaFile.Metadata.VideoData.FrameSize.Split('x').Select(s => int.Parse(s)).ToArray();

                                    var minResolutionPart = Math.Min(resolutionParts[0], resolutionParts[1]);

                                    if (minResolutionPart > 720) throw new InvalidOperationException();

                                    engine.GetThumbnail(videoMediaFile, thumbnailMediaFile, new MediaToolkit.Options.ConversionOptions());
                                }

                                try
                                {
                                    var videoMediaFileInfo = new System.IO.FileInfo(videoMediaFile.Filename);
                                    var thumbnailMediaFileInfo = new System.IO.FileInfo(thumbnailMediaFile.Filename);

                                    var thumbnail = new Thumbnail
                                    {
                                        Id = thumbnailId,
                                        ContentType = "image/jpeg",
                                        Length = thumbnailMediaFileInfo.Length,
                                        About = "Promo Event " + entity.Id
                                    };

                                    var video = new Video
                                    {
                                        Id = videoId,
                                        Thumbnail = thumbnail,
                                        Extension = extension,
                                        Length = videoMediaFileInfo.Length,
                                        Duration = videoMediaFile.Metadata.Duration,
                                        Format = videoMediaFile.Metadata.VideoData.Format,
                                        FrameSize = videoMediaFile.Metadata.VideoData.FrameSize,
                                        ColorModel = videoMediaFile.Metadata.VideoData.ColorModel,
                                        Fps = videoMediaFile.Metadata.VideoData.Fps,
                                        AudioFormat = videoMediaFile.Metadata.AudioData.Format,
                                        AudioChannelOutput = videoMediaFile.Metadata.AudioData.ChannelOutput,
                                        About = "Promo Event " + entity.Id
                                    };

                                    entity.PromoVideo = video;
                                }
                                catch (Exception ex)
                                {
                                    mediaFileManagerFacade.DeleteThumbnailFile(thumbnailId, Cinema.Web.Common.Constants.VIDEO_THUMBNAIL_EXTENSION);

                                    throw ex;
                                }
                            }
                            catch (Exception ex)
                            {
                                mediaFileManagerFacade.DeleteVideoFile(videoId, extension);

                                throw ex;
                            }
                        }
                    }

                    using (var memoryStream = new System.IO.MemoryStream())
                    {
                        modelInput.PosterImageFile.CopyTo(memoryStream);

                        var image = new Image { ContentType = modelInput.PosterImageFile.ContentType, Length = modelInput.PosterImageFile.Length, About = "Event " + entity.Id };

                        mediaFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray());

                        entity.PosterImage = image;
                    }

                    Repository.Add(entity, User.Identity.Name);

                    PostCreate(entity, modelInput);

                    notification.Type = NotificationType.Success;
                    notification.Message = GetCreationSuccessMessage();

                    TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                    return RedirectToAction(GetDefaultActionName());
                }
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError(PROMO_VIDEO_FILE_FIELD_NAME, "La resolución del archivo de video es superior a la permisible para garantizar la estabilidad del sistema (720p máximo).");
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return View(ConfigurePostFailCreateValidation(modelInput));
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.EventManagerEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.EventManagerEditor)]
        public override IActionResult Edit(EventInputViewModel modelInput)
        {
            ModelState.ClearValidationState(POSTER_IMAGE_FILE_FIELD_NAME);
            ModelState.MarkFieldValid(POSTER_IMAGE_FILE_FIELD_NAME);

            if (Request.Form.Files.Count > 0)
            {
                var formPosterImageFile = Request.Form.Files.FirstOrDefault(f => f.Name == POSTER_IMAGE_FILE_FIELD_NAME);

                var validationRequired = false;

                if (formPosterImageFile != null && formPosterImageFile.Length > 0)
                {
                    modelInput.PosterImageFile = formPosterImageFile;

                    validationRequired = true;
                }

                var formPromoVideoFile = Request.Form.Files.FirstOrDefault(f => f.Name == PROMO_VIDEO_FILE_FIELD_NAME);

                if (formPromoVideoFile != null && formPromoVideoFile.Length > 0)
                {
                    modelInput.PromoVideoFile = formPromoVideoFile;

                    validationRequired = true;
                }

                if (validationRequired)
                {
                    TryUpdateModelAsync(modelInput).Wait();

                    if (modelInput.PosterImageFile == null)
                    {
                        ModelState.ClearValidationState(POSTER_IMAGE_FILE_FIELD_NAME);
                        ModelState.MarkFieldValid(POSTER_IMAGE_FILE_FIELD_NAME);
                    }
                }
            }

            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetInvalidMessage()
            };

            ValidateEdit(modelInput);

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = Repository.Get(modelInput.Id);

                    modelInput.Mergue(entity);

                    if (modelInput.PromoVideoFile == null)
                    {
                        if (modelInput.ResetPromoVideo && entity.PromoVideo != null)
                        {
                            var thumbnail = entity.PromoVideo.Thumbnail;

                            var videoId = entity.PromoVideo.Id;
                            var videoExtension = entity.PromoVideo.Extension;

                            videoRepository.Delete(entity.PromoVideo, User.Identity.Name);

                            var thumbnailId = thumbnail.Id;

                            thumbnailRepository.Delete(thumbnail, User.Identity.Name);

                            try
                            {
                                mediaFileManagerFacade.DeleteThumbnailFile(thumbnailId, Cinema.Web.Common.Constants.VIDEO_THUMBNAIL_EXTENSION);
                                mediaFileManagerFacade.DeleteVideoFile(videoId, videoExtension);
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        using (var memoryStream = new System.IO.MemoryStream())
                        {
                            modelInput.PromoVideoFile.CopyTo(memoryStream);

                            var extension = System.IO.Path.GetExtension(modelInput.PromoVideoFile.FileName);

                            var thumbnailId = Guid.NewGuid();

                            var videoId = Guid.NewGuid();

                            mediaFileManagerFacade.SaveVideoFileContent(videoId, memoryStream.ToArray(), extension);

                            mediaFileManagerFacade.EnsureThumbnailsDirectoryExist();

                            try
                            {
                                var videoMediaFile = new MediaToolkit.Model.MediaFile(mediaFileManagerFacade.GetVideoFilePath(videoId, extension));

                                var thumbnailMediaFile = new MediaToolkit.Model.MediaFile(mediaFileManagerFacade.GetThumbnailFilePath(thumbnailId, Cinema.Web.Common.Constants.VIDEO_THUMBNAIL_EXTENSION));

                                using (var engine = new MediaToolkit.Engine(ffmpegOptions.Value.Path))
                                {
                                    engine.GetMetadata(videoMediaFile);

                                    var resolutionParts = videoMediaFile.Metadata.VideoData.FrameSize.Split('x').Select(s => int.Parse(s)).ToArray();

                                    var minResolutionPart = Math.Min(resolutionParts[0], resolutionParts[1]);

                                    if (minResolutionPart > 720) throw new InvalidOperationException();

                                    engine.GetThumbnail(videoMediaFile, thumbnailMediaFile, new MediaToolkit.Options.ConversionOptions());
                                }

                                try
                                {
                                    var videoMediaFileInfo = new System.IO.FileInfo(videoMediaFile.Filename);
                                    var thumbnailMediaFileInfo = new System.IO.FileInfo(thumbnailMediaFile.Filename);

                                    var thumbnail = new Thumbnail
                                    {
                                        Id = thumbnailId,
                                        ContentType = "image/jpeg",
                                        Length = thumbnailMediaFileInfo.Length,
                                        About = "Promo Event " + entity.Id
                                    };

                                    var video = new Video
                                    {
                                        Id = videoId,
                                        Thumbnail = thumbnail,
                                        Extension = extension,
                                        Length = videoMediaFileInfo.Length,
                                        Duration = videoMediaFile.Metadata.Duration,
                                        Format = videoMediaFile.Metadata.VideoData.Format,
                                        FrameSize = videoMediaFile.Metadata.VideoData.FrameSize,
                                        ColorModel = videoMediaFile.Metadata.VideoData.ColorModel,
                                        Fps = videoMediaFile.Metadata.VideoData.Fps,
                                        AudioFormat = videoMediaFile.Metadata.AudioData.Format,
                                        AudioChannelOutput = videoMediaFile.Metadata.AudioData.ChannelOutput,
                                        About = "Promo Event " + entity.Id
                                    };

                                    entity.PromoVideo = video;
                                }
                                catch (Exception ex)
                                {
                                    mediaFileManagerFacade.DeleteThumbnailFile(thumbnailId, Cinema.Web.Common.Constants.VIDEO_THUMBNAIL_EXTENSION);

                                    throw ex;
                                }
                            }
                            catch (Exception ex)
                            {
                                mediaFileManagerFacade.DeleteVideoFile(videoId, extension);

                                throw ex;
                            }
                        }
                    }

                    if (modelInput.PosterImageFile != null)
                    {
                        using (var memoryStream = new System.IO.MemoryStream())
                        {
                            modelInput.PosterImageFile.CopyTo(memoryStream);

                            var image = new Image { ContentType = modelInput.PosterImageFile.ContentType, Length = modelInput.PosterImageFile.Length, About = "Event " + entity.Id };

                            mediaFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray());

                            entity.PosterImage = image;
                        }
                    }

                    Repository.Update(entity, User.Identity.Name);

                    PostEdit(entity, modelInput);

                    notification.Type = NotificationType.Success;
                    notification.Message = GetEditionSuccessMessage();

                    TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                    return RedirectToAction("edit", new { key = entity.Id });
                }
                catch (InvalidOperationException)
                {
                    ModelState.AddModelError(PROMO_VIDEO_FILE_FIELD_NAME, "La resolución del archivo de video es superior a la permisible para garantizar la estabilidad del sistema (720p máximo).");

                    TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                    return View(ConfigurePostFailEditValidation(modelInput));
                }
                catch (Exception e)
                {
                    logger.LogError(1, e.InnerException, e.Message, null);

                    notification.Message = GetNotFoundMessage();
                }

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return View(ConfigurePostFailEditValidation(modelInput));
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.EventManagerEditor)]
        public override ActionResult Enable(Guid key)
        {
            return base.Enable(key);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.EventManagerEditor)]
        public override ActionResult Disable(Guid key)
        {
            return base.Disable(key);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.EventManagerRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.EventManagerRemover)]
        public override IActionResult Delete(EventDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PostEdit(Event entity, EventInputViewModel modelInput)
        {
            base.PostEdit(entity, modelInput);

            if (modelInput.PosterImageFile != null)
            {
                imageRepository.Delete(imageRepository.Get(modelInput.PosterImageId), User.Identity.Name);

                mediaFileManagerFacade.DeleteImageFile(modelInput.PosterImageId);
            }

            if (modelInput.PromoVideoFile != null && modelInput.PromoVideoId != Guid.Empty)
            {
                var video = videoRepository.Get(modelInput.PromoVideoId);

                var thumbnail = video.Thumbnail;

                var videoExtension = video.Extension;

                videoRepository.Delete(video, User.Identity.Name);

                var thumbnailId = thumbnail.Id;

                thumbnailRepository.Delete(thumbnail, User.Identity.Name);

                try
                {
                    mediaFileManagerFacade.DeleteThumbnailFile(thumbnailId, Cinema.Web.Common.Constants.VIDEO_THUMBNAIL_EXTENSION);
                    mediaFileManagerFacade.DeleteVideoFile(modelInput.PromoVideoId, videoExtension);
                }
                catch { }
            }
        }

        protected override void PostDelete(Event entity)
        {
            base.PostDelete(entity);

            mediaFileManagerFacade.DeleteImageFile(entity.PosterImage.Id);

            imageRepository.Delete(entity.PosterImage, User.Identity.Name);

            if (entity.PromoVideo != null)
            {
                mediaFileManagerFacade.DeleteVideoFile(entity.PromoVideo.Id, entity.PromoVideo.Extension);

                mediaFileManagerFacade.DeleteThumbnailFile(entity.PromoVideo.Thumbnail.Id, Cinema.Web.Common.Constants.VIDEO_THUMBNAIL_EXTENSION);

                var thumbnail = entity.PromoVideo.Thumbnail;

                videoRepository.Delete(entity.PromoVideo, User.Identity.Name);

                thumbnailRepository.Delete(thumbnail, User.Identity.Name);
            }
        }

        protected override EventInputViewModel ConfigurePostFailEditValidation(EventInputViewModel modelInput)
        {
            var entity = Repository.Get(modelInput.Id);

            modelInput.PosterImageId = entity.PosterImage.Id;

            return base.ConfigurePostFailEditValidation(modelInput);
        }

        #region Event WithIns Management

        [Route("edit/{key}/[action]")]
        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.EventManagerEditor)]
        public IActionResult WithIns(Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var model = new EventWithInsViewModel
                {
                    Event = entity,
                    Personalities = new List<SelectListItem>()
                };

                var personalitites = personalityRepository.GetAll();

                foreach (var item in personalitites)
                {
                    model.Personalities.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name + " " + item.LastName });
                }

                return View(model);
            }
            catch (Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetNotFoundMessage()
            };

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction(GetDefaultActionName());
        }

        [Route("edit/withins/addpicking")]
        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.EventManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddWithIn(WithInInputViewModel modelInput)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetInvalidMessage()
            };

            Event @event = null;

            try
            {
                @event = Repository.Get(modelInput.EventId);
            }
            catch { }

            if (@event == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            if (ModelState.IsValid)
            {
                var bridge = new EventPersonalityBridge
                {
                    Event = @event,
                    EventId = @event.Id,
                    Personality = personalityRepository.Get(modelInput.PersonalityId),
                    PersonalityId = modelInput.PersonalityId,
                    IsPresenter = modelInput.IsPresenter
                };

                if (@event.EventPersonalityBridges == null)
                {
                    @event.EventPersonalityBridges = new List<EventPersonalityBridge>();
                }

                @event.EventPersonalityBridges.Add(bridge);

                Repository.Update(@event, User.Identity.Name);

                notification.Type = NotificationType.Success;
                notification.Message = GetCreationSuccessMessage();
            }
            else
            {
                notification.Messages = new List<string>();

                foreach (var item in ModelState)
                {
                    if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

                    foreach (var error in item.Value.Errors)
                    {
                        notification.Messages.Add(error.ErrorMessage);
                    }
                }
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction("withins", new { key = @event.Id });
        }

        [Route("edit/withins/addcreating")]
        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.EventManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPersonalityWithIn(PersonalityWithInCreateInputViewModel modelInput)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetInvalidMessage()
            };

            Event @event = null;

            try
            {
                @event = Repository.Get(modelInput.EventId);
            }
            catch { }

            if (@event == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            if (personalityRepository.GetAll().Any(entity => entity.Name == modelInput.Name && entity.LastName == modelInput.LastName))
            {
                ModelState.AddModelError("Name", localizer["UniqueValueConvinationValidationErrorMessage", localizer["Name"], localizer["LastName"]]);
            }

            if (ModelState.IsValid)
            {
                var personality = new Personality
                {
                    Name = modelInput.Name,
                    LastName = modelInput.LastName,
                    BornDate = modelInput.BornDate,
                    Sex = modelInput.Sex,
                    About = modelInput.About
                };

                personalityRepository.Add(personality, User.Identity.Name);

                var bridge = new EventPersonalityBridge
                {
                    Event = @event,
                    EventId = @event.Id,
                    Personality = personality,
                    PersonalityId = personality.Id,
                    IsPresenter = modelInput.IsPresenter
                };

                if (@event.EventPersonalityBridges == null)
                {
                    @event.EventPersonalityBridges = new List<EventPersonalityBridge>();
                }

                @event.EventPersonalityBridges.Add(bridge);

                Repository.Update(@event, User.Identity.Name);

                notification.Type = NotificationType.Success;
                notification.Message = GetCreationSuccessMessage();
            }
            else
            {
                notification.Messages = new List<string>();

                foreach (var item in ModelState)
                {
                    if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

                    foreach (var error in item.Value.Errors)
                    {
                        notification.Messages.Add(error.ErrorMessage);
                    }
                }
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction("withins", new { key = @event.Id });
        }

        [Route("edit/withins/delete")]
        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.EventManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteWithIn(WithInDisplayViewModel modelDisplay)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetNotFoundMessage()
            };

            Event @event = null;

            try
            {
                @event = Repository.Get(modelDisplay.EventId);
            }
            catch { }

            if (@event == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            try
            {
                var entity = dbContext.Set<EventPersonalityBridge>().FirstOrDefault(e => e.EventId == @event.Id && e.PersonalityId == modelDisplay.PersonalityId);

                dbContext.Set<EventPersonalityBridge>().Remove(entity);

                if (dbContext is ITraceableDbContext)
                {
                    (dbContext as ITraceableDbContext).SaveChanges(User.Identity.Name);
                }
                else
                {
                    dbContext.SaveChanges();
                }

                notification.Type = NotificationType.Success;
                notification.Message = GetDeletionSuccessMessage();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                notification.Type = NotificationType.Warning;
                notification.Message = GetOnDeletionDependecyConstrainErrorMessage();
            }
            catch (Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction("withins", new { key = @event.Id });
        }

        [HttpPost]
        [Route("edit/withins/setispresenter")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult WithInSetIsPresenter(WithInInputViewModel modelInput)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetNotFoundMessage()
            };

            Event @event = null;

            try
            {
                @event = Repository.Get(modelInput.EventId);
            }
            catch { }

            if (@event == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            try
            {
                var entity = dbContext.Set<EventPersonalityBridge>().FirstOrDefault(e => e.EventId == @event.Id && e.PersonalityId == modelInput.PersonalityId);

                entity.IsPresenter = modelInput.IsPresenter;

                dbContext.Set<EventPersonalityBridge>().Update(entity);

                if (dbContext is ITraceableDbContext)
                {
                    (dbContext as ITraceableDbContext).SaveChanges(User.Identity.Name);
                }
                else
                {
                    dbContext.SaveChanges();
                }

                notification.Type = NotificationType.Success;
                notification.Message = GetEditionSuccessMessage();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                notification.Type = NotificationType.Warning;
                notification.Message = GetOnDeletionDependecyConstrainErrorMessage();
            }
            catch (Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction("withins", new { key = @event.Id });
        }

        #endregion
    }
}
