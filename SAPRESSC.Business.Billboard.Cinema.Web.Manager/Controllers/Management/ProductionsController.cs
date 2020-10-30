using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.SecurityShared;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.Filter;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Facades.Manager;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Controllers.Management
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Area("cinemabillboard")]
    public class ProductionsController : Core.Web.Management.Controllers.AbstractEntityManagementFilteredController<Production, long, ProductionInputViewModel, ProductionDisplayViewModel, ProductionFilterModel>
    {
        private const string POSTER_IMAGE_FILE_FIELD_NAME = "PosterImageFile";
        private const string PLACARD_IMAGE_FILE_FIELD_NAME = "PlacardImageFile";
        private const string TRAILER_VIDEO_FILE_FIELD_NAME = "TrailerVideoFile";

        private readonly IEntityRepository<Thumbnail, System.Guid> thumbnailRepository;
        private readonly IEntityRepository<Video, System.Guid> videoRepository;
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IMediaFileManagerFacade mediaFileManager;
        private readonly IEntityRepository<Language, int> languageRepository;
        private readonly IEntityRepository<Genre, int> genreRepository;
        private readonly IEntityRepository<ProducerHouse, long> producerHouseRepository;
        private readonly IEntityRepository<Country, int> countryRepository;
        private readonly IEntityRepository<Award, int> awardRepository;
        private readonly IEntityRepository<AwardWinning, int> awardWinningRepository;
        private readonly IEntityRepository<Personality, long> personalityRepository;
        private readonly IEntityRepository<Participation, long> participationRepository;
        private readonly IEntityRepository<Clasification, int> clasificationRepository;
        private readonly IEntityRepository<Category, System.Guid> categoryRepository;
        private readonly IEntityRepository<Collection, System.Guid> collectionRepository;
        private readonly MediaToolkit.Services.IMediaToolkitService mediaToolkitService;

        public ProductionsController(MediaToolkit.Services.IMediaToolkitService mediaToolkitService, IEntityRepository<Collection, System.Guid> collectionRepository, IEntityRepository<Category, System.Guid> categoryRepository, IEntityRepository<Thumbnail, System.Guid> thumbnailRepository, IEntityRepository<Video, System.Guid> videoRepository, IEntityRepository<Clasification, int> clasificationRepository, IEntityRepository<Participation, long> participationRepository, IEntityRepository<Personality, long> personalityRepository, IEntityRepository<AwardWinning, int> awardWinningRepository, IEntityRepository<Award, int> awardRepository, IEntityRepository<Country, int> countryRepository, IEntityRepository<ProducerHouse, long> producerHouseRepository, IEntityRepository<Genre, int> genreRepository, IEntityRepository<Language, int> languageRepository, IMediaFileManagerFacade mediaFileManager, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<Production, long> repository, Microsoft.Extensions.Localization.IStringLocalizer<ProductionsController> localizer, ILogger<ProductionsController> logger) : base(repository, localizer, logger)
        {
            this.thumbnailRepository = thumbnailRepository;
            this.videoRepository = videoRepository;
            this.imageRepository = imageRepository;
            this.mediaFileManager = mediaFileManager;
            this.languageRepository = languageRepository;
            this.genreRepository = genreRepository;
            this.producerHouseRepository = producerHouseRepository;
            this.countryRepository = countryRepository;
            this.awardRepository = awardRepository;
            this.awardWinningRepository = awardWinningRepository;
            this.personalityRepository = personalityRepository;
            this.participationRepository = participationRepository;
            this.clasificationRepository = clasificationRepository;
            this.categoryRepository = categoryRepository;
            this.collectionRepository = collectionRepository;
            this.mediaToolkitService = mediaToolkitService;
        }

        protected override string GetFilterSessionID()
        {
            return "cinemabillboard-productions-management-filter";
        }

        protected override ProductionFilterModel GetFilterModel()
        {
            var filterModel = base.GetFilterModel();

            if (filterModel == null)
            {
                filterModel = new ProductionFilterModel();
            }

            filterModel.Countries = new List<SelectListItem>
            {
                new SelectListItem("-", "0")
            };

            filterModel.Countries.AddRange(countryRepository.GetAll().Select(entity => new SelectListItem
            {
                Text = entity.Name,
                Value = entity.Id.ToString()
            }));

            filterModel.Genres = new List<SelectListItem>
            {
                new SelectListItem("-", "0")
            };

            filterModel.Genres.AddRange(genreRepository.GetAll().Select(entity => new SelectListItem
            {
                Text = entity.Name,
                Value = entity.Id.ToString()
            }));

            filterModel.Personalities = new List<SelectListItem>
            {
                new SelectListItem("-", "0")
            };

            filterModel.Personalities.AddRange(personalityRepository.GetAll().OrderBy(entity => entity.Name).Select(entity => new SelectListItem
            {
                Text = string.Format("{0} {1}", entity.Name.Trim(), entity.LastName.Trim()),
                Value = entity.Id.ToString()
            }));

            return filterModel;
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerViewer)]
        public override IActionResult Filter(ProductionFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerCreator)]
        public override IActionResult Create()
        {
            var languageCount = languageRepository.Count();
            var countryCount = countryRepository.Count();
            var ClasificationCount = clasificationRepository.Count();

            if (languageCount <= 0 || countryCount <= 0 || ClasificationCount <= 0)
            {
                var notification = new NotificationViewModel
                {
                    Type = NotificationType.Warning,
                    Title = "RequiredDataIncompleteTitle",
                    Message = "RequiredDataIncompleteMessage",
                    Messages = new List<string>()
                };

                if (languageCount <= 0)
                {
                    notification.Messages.Add("AtLastOneLanguageRequiredMessage");
                }

                if (countryCount <= 0)
                {
                    notification.Messages.Add("AtLastOneCountryRequiredMessage");
                }

                if (ClasificationCount <= 0)
                {
                    notification.Messages.Add("AtLastOneClasificationRequiredMessage");
                }

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction("index");
            }

            return base.Create();
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerCreator)]
        public override IActionResult Create(ProductionInputViewModel modelInput)
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

                var formPlacardImageFile = Request.Form.Files.FirstOrDefault(f => f.Name == PLACARD_IMAGE_FILE_FIELD_NAME);

                if (formPlacardImageFile != null && formPlacardImageFile.Length > 0)
                {
                    modelInput.PlacardImageFile = formPlacardImageFile;

                    validationRequired = true;
                }

                var formTrailerVideoFile = Request.Form.Files.FirstOrDefault(f => f.Name == TRAILER_VIDEO_FILE_FIELD_NAME);

                if (formTrailerVideoFile != null && formTrailerVideoFile.Length > 0)
                {
                    modelInput.TrailerVideoFile = formTrailerVideoFile;

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

                    if (modelInput.TrailerVideoFile != null)
                    {
                        using (var memoryStream = new System.IO.MemoryStream())
                        {
                            modelInput.TrailerVideoFile.CopyTo(memoryStream);

                            var extension = System.IO.Path.GetExtension(modelInput.TrailerVideoFile.FileName);

                            var thumbnailId = System.Guid.NewGuid();

                            var videoId = System.Guid.NewGuid();

                            mediaFileManager.SaveVideoFileContent(videoId, memoryStream.ToArray(), extension);

                            mediaFileManager.EnsureThumbnailsDirectoryExist();

                            try
                            {
                                var videoPath = mediaFileManager.GetVideoFilePath(videoId, extension);
                                var thumbnailPath = mediaFileManager.GetThumbnailFilePath(thumbnailId, Constants.VIDEO_THUMBNAIL_EXTENSION);

                                var getMetadataFfTask = new MediaToolkit.Tasks.FfTaskGetMetadata(videoPath);

                                var getMEtadataTask = mediaToolkitService.ExecuteAsync(getMetadataFfTask);

                                getMEtadataTask.RunSynchronously();

                                var getMetadataResult = getMEtadataTask.Result;

                                var videoMediaStream = getMetadataResult.Metadata.Streams.First(stream => stream.CodecType == "video");
                                var auduioMediaStream = getMetadataResult.Metadata.Streams.FirstOrDefault(stream => stream.CodecType == "audio");

                                var videoDuration = System.TimeSpan.FromSeconds(double.Parse(getMetadataResult.Metadata.Format.Duration.Replace(".", ",")));

                                var saveThumbnailFfTask = new MediaToolkit.Tasks.FfTaskSaveThumbnail(
                                    videoPath,
                                    thumbnailPath,
                                    System.TimeSpan.FromSeconds(10)
                                );

                                var saveThumbnailTask = mediaToolkitService.ExecuteAsync(saveThumbnailFfTask);

                                saveThumbnailTask.RunSynchronously();

                                if (videoMediaStream.Height > 720) throw new System.InvalidOperationException();

                                try
                                {
                                    var thumbnailMediaFileInfo = new System.IO.FileInfo(thumbnailPath);

                                    var thumbnail = new Thumbnail
                                    {
                                        Id = thumbnailId,
                                        ContentType = "image/jpeg",
                                        Length = thumbnailMediaFileInfo.Length,
                                        About = "Trailer Production " + entity.Id
                                    };

                                    var video = new Video
                                    {
                                        Id = videoId,
                                        Thumbnail = thumbnail,
                                        Extension = extension,
                                        Length = memoryStream.Length,
                                        Duration = videoDuration,
                                        Format = string.Format("{0} ({1})", videoMediaStream.CodecName, videoMediaStream.CodecLongName),
                                        FrameSize = string.Format("{0}x{1}", videoMediaStream.Width, videoMediaStream.Height),
                                        ColorModel = videoMediaStream.PixFmt,
                                        Fps = 0,
                                        AudioFormat = auduioMediaStream?.CodecLongName,
                                        AudioChannelOutput = auduioMediaStream?.ChannelLayout,
                                        About = "Trailer Production " + entity.Id
                                    };

                                    entity.TrailerVideo = video;
                                }
                                catch (System.Exception ex)
                                {
                                    mediaFileManager.DeleteThumbnailFile(thumbnailId, Constants.VIDEO_THUMBNAIL_EXTENSION);

                                    throw ex;
                                }
                            }
                            catch (System.Exception ex)
                            {
                                mediaFileManager.DeleteVideoFile(videoId, extension);

                                throw ex;
                            }
                        }
                    }

                    if (modelInput.PlacardImageFile != null)
                    {
                        using (var memoryStream = new System.IO.MemoryStream())
                        {
                            modelInput.PlacardImageFile.CopyTo(memoryStream);

                            var image = new Image { ContentType = modelInput.PlacardImageFile.ContentType, Length = modelInput.PlacardImageFile.Length, About = "Placard for Production " + entity.Id };

                            mediaFileManager.SaveImageFileContent(image.Id, memoryStream.ToArray());

                            entity.PlacardImage = image;
                        }
                    }

                    using (var memoryStream = new System.IO.MemoryStream())
                    {
                        modelInput.PosterImageFile.CopyTo(memoryStream);

                        var image = new Image { ContentType = modelInput.PosterImageFile.ContentType, Length = modelInput.PosterImageFile.Length, About = "Poster for Production " + entity.Id };

                        mediaFileManager.SaveImageFileContent(image.Id, memoryStream.ToArray());

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
            catch (System.InvalidOperationException)
            {
                ModelState.AddModelError(TRAILER_VIDEO_FILE_FIELD_NAME, "La resolución del archivo de video es superior a la permisible para garantizar la estabilidad del sistema (720p máximo).");
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return View(ConfigurePostFailCreateValidation(modelInput));
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerEditor)]
        public override IActionResult Edit(long key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerEditor)]
        public override IActionResult Edit(ProductionInputViewModel modelInput)
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

                var formPlacardImageFile = Request.Form.Files.FirstOrDefault(f => f.Name == PLACARD_IMAGE_FILE_FIELD_NAME);

                if (formPlacardImageFile != null && formPlacardImageFile.Length > 0)
                {
                    modelInput.PlacardImageFile = formPlacardImageFile;

                    validationRequired = true;
                }

                var formTrailerVideoFile = Request.Form.Files.FirstOrDefault(f => f.Name == TRAILER_VIDEO_FILE_FIELD_NAME);

                if (formTrailerVideoFile != null && formTrailerVideoFile.Length > 0)
                {
                    modelInput.TrailerVideoFile = formTrailerVideoFile;

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

                    PreEdit(entity, modelInput);

                    if (modelInput.TrailerVideoFile == null)
                    {
                        if (modelInput.ResetTrailerVideo && entity.TrailerVideo != null)
                        {
                            var thumbnail = entity.TrailerVideo.Thumbnail;

                            var videoId = entity.TrailerVideo.Id;
                            var videoExtension = entity.TrailerVideo.Extension;

                            videoRepository.Delete(entity.TrailerVideo, User.Identity.Name);

                            var thumbnailId = thumbnail.Id;

                            thumbnailRepository.Delete(thumbnail, User.Identity.Name);

                            try
                            {
                                mediaFileManager.DeleteThumbnailFile(thumbnailId, Constants.VIDEO_THUMBNAIL_EXTENSION);
                                mediaFileManager.DeleteVideoFile(videoId, videoExtension);
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        using (var memoryStream = new System.IO.MemoryStream())
                        {
                            modelInput.TrailerVideoFile.CopyTo(memoryStream);

                            var extension = System.IO.Path.GetExtension(modelInput.TrailerVideoFile.FileName);

                            var thumbnailId = System.Guid.NewGuid();

                            var videoId = System.Guid.NewGuid();

                            mediaFileManager.SaveVideoFileContent(videoId, memoryStream.ToArray(), extension);

                            mediaFileManager.EnsureThumbnailsDirectoryExist();

                            try
                            {
                                var videoPath = mediaFileManager.GetVideoFilePath(videoId, extension);
                                var thumbnailPath = mediaFileManager.GetThumbnailFilePath(thumbnailId, Constants.VIDEO_THUMBNAIL_EXTENSION);

                                var getMetadataFfTask = new MediaToolkit.Tasks.FfTaskGetMetadata(videoPath);

                                var getMEtadataTask = mediaToolkitService.ExecuteAsync(getMetadataFfTask);

                                getMEtadataTask.RunSynchronously();

                                var getMetadataResult = getMEtadataTask.Result;

                                var videoMediaStream = getMetadataResult.Metadata.Streams.First(stream => stream.CodecType == "video");
                                var auduioMediaStream = getMetadataResult.Metadata.Streams.FirstOrDefault(stream => stream.CodecType == "audio");

                                var videoDuration = System.TimeSpan.FromSeconds(double.Parse(getMetadataResult.Metadata.Format.Duration.Replace(".", ",")));

                                var saveThumbnailFfTask = new MediaToolkit.Tasks.FfTaskSaveThumbnail(
                                    videoPath,
                                    thumbnailPath,
                                    System.TimeSpan.FromSeconds(10)
                                );

                                var saveThumbnailTask = mediaToolkitService.ExecuteAsync(saveThumbnailFfTask);

                                saveThumbnailTask.RunSynchronously();

                                if (videoMediaStream.Height > 720) throw new System.InvalidOperationException();

                                try
                                {
                                    var thumbnailMediaFileInfo = new System.IO.FileInfo(thumbnailPath);

                                    var thumbnail = new Thumbnail
                                    {
                                        Id = thumbnailId,
                                        ContentType = "image/jpeg",
                                        Length = thumbnailMediaFileInfo.Length,
                                        About = "Trailer Production " + entity.Id
                                    };

                                    var video = new Video
                                    {
                                        Id = videoId,
                                        Thumbnail = thumbnail,
                                        Extension = extension,
                                        Length = memoryStream.Length,
                                        Duration = videoDuration,
                                        Format = string.Format("{0} ({1})", videoMediaStream.CodecName, videoMediaStream.CodecLongName),
                                        FrameSize = string.Format("{0}x{1}", videoMediaStream.Width, videoMediaStream.Height),
                                        ColorModel = videoMediaStream.PixFmt,
                                        Fps = 0,
                                        AudioFormat = auduioMediaStream?.CodecLongName,
                                        AudioChannelOutput = auduioMediaStream?.ChannelLayout,
                                        About = "Trailer Production " + entity.Id
                                    };

                                    entity.TrailerVideo = video;
                                }
                                catch (System.Exception ex)
                                {
                                    mediaFileManager.DeleteThumbnailFile(thumbnailId, Constants.VIDEO_THUMBNAIL_EXTENSION);

                                    throw ex;
                                }
                            }
                            catch (System.Exception ex)
                            {
                                mediaFileManager.DeleteVideoFile(videoId, extension);

                                throw ex;
                            }
                        }
                    }

                    if (modelInput.PlacardImageFile == null)
                    {
                        if (modelInput.ResetPlacardImage && entity.PlacardImage != null)
                        {
                            var oldPlacard = entity.PlacardImage;

                            entity.PlacardImage = null;

                            mediaFileManager.DeleteImageFile(oldPlacard.Id);

                            imageRepository.Delete(oldPlacard, User.Identity.Name);
                        }
                    }
                    else
                    {
                        var oldPlacard = entity.PlacardImage;

                        using (var memoryStream = new System.IO.MemoryStream())
                        {
                            modelInput.PlacardImageFile.CopyTo(memoryStream);

                            memoryStream.Position = 0;

                            entity.PlacardImage = new Image { ContentType = modelInput.PlacardImageFile.ContentType, Length = modelInput.PlacardImageFile.Length, About = "Placard for Production " + entity.Id };

                            mediaFileManager.SaveImageFileContent(entity.PlacardImage.Id, memoryStream.ToArray());
                        }

                        if (oldPlacard != null)
                        {
                            mediaFileManager.DeleteImageFile(oldPlacard.Id);

                            imageRepository.Delete(oldPlacard, User.Identity.Name);
                        }
                    }

                    if (modelInput.PosterImageFile != null)
                    {
                        using (var memoryStream = new System.IO.MemoryStream())
                        {
                            modelInput.PosterImageFile.CopyTo(memoryStream);

                            var image = new Image { ContentType = modelInput.PosterImageFile.ContentType, Length = modelInput.PosterImageFile.Length, About = "Poster for Production " + entity.Id };

                            mediaFileManager.SaveImageFileContent(image.Id, memoryStream.ToArray());

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
                catch (System.InvalidOperationException)
                {
                    ModelState.AddModelError(TRAILER_VIDEO_FILE_FIELD_NAME, "La resolución del archivo de video es superior a la permisible para garantizar la estabilidad del sistema (720p máximo).");

                    TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                    return View(ConfigurePostFailEditValidation(modelInput));
                }
                catch (System.Exception e)
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

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerRemover)]
        public override IActionResult Delete(long key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerRemover)]
        public override IActionResult Delete(ProductionDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PopulateViewModelDisplayExtras(ProductionDisplayViewModel modelDisplay, Production entity)
        {
            base.PopulateViewModelDisplayExtras(modelDisplay, entity);

            if (entity.CategoryId != null && entity.CategoryId.Value != System.Guid.Empty)
            {
                var category = categoryRepository.Get(entity.CategoryId.Value);

                modelDisplay.Category = category.GetTreeName();
            }

            if (entity.CollectionId != null && entity.CollectionId.Value != System.Guid.Empty)
            {
                var collection = collectionRepository.Get(entity.CollectionId.Value);

                modelDisplay.Collection = collection.GetTreeName();
            }
        }

        protected override void PreCreate(Production entity, ProductionInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.Country = countryRepository.Get(modelInput.CountryId);
            entity.Language = languageRepository.Get(modelInput.LanguageId);
            entity.Clasification = clasificationRepository.Get(modelInput.ClasificationId);
        }

        protected override void PreEdit(Production entity, ProductionInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            entity.Country = countryRepository.Get(modelInput.CountryId);
            entity.Language = languageRepository.Get(modelInput.LanguageId);
            entity.Clasification = clasificationRepository.Get(modelInput.ClasificationId);
        }

        protected override void PostDelete(Production entity)
        {
            base.PostDelete(entity);

            mediaFileManager.DeleteImageFile(entity.PosterImage.Id);

            imageRepository.Delete(entity.PosterImage, User.Identity.Name);

            if (entity.PlacardImage != null)
            {
                imageRepository.Delete(entity.PlacardImage, User.Identity.Name);

                mediaFileManager.DeleteImageFile(entity.PlacardImage.Id);
            }

            if (entity.TrailerVideo != null)
            {
                mediaFileManager.DeleteVideoFile(entity.TrailerVideo.Id, entity.TrailerVideo.Extension);

                mediaFileManager.DeleteThumbnailFile(entity.TrailerVideo.Thumbnail.Id, Constants.VIDEO_THUMBNAIL_EXTENSION);

                videoRepository.Delete(entity.TrailerVideo, User.Identity.Name);
            }
        }

        protected override void PostCreate(Production entity, ProductionInputViewModel modelInput)
        {
            base.PostCreate(entity, modelInput);

            var genres = new List<Genre>();

            if (modelInput.SelectableGenres != null)
            {
                foreach (var item in modelInput.SelectableGenres.Where(item => item.Selected))
                {
                    genres.Add(genreRepository.Get(item.Id));
                }
            }

            entity.SetGenres(genres);

            var producerHouses = new List<ProducerHouse>();

            if (modelInput.SelectableProducerHouses != null)
            {
                foreach (var item in modelInput.SelectableProducerHouses.Where(item => item.Selected))
                {
                    producerHouses.Add(producerHouseRepository.Get(item.Id));
                }
            }

            entity.SetProducerHouses(producerHouses);

            Repository.Update(entity, User.Identity.Name);
        }

        protected override void PostEdit(Production entity, ProductionInputViewModel modelInput)
        {
            base.PostEdit(entity, modelInput);

            if (modelInput.PosterImageFile != null)
            {
                imageRepository.Delete(imageRepository.Get(modelInput.PosterImageId), User.Identity.Name);

                mediaFileManager.DeleteImageFile(modelInput.PosterImageId);
            }

            if (modelInput.TrailerVideoFile != null && modelInput.TrailerVideoId != System.Guid.Empty)
            {
                var video = videoRepository.Get(modelInput.TrailerVideoId);

                var thumbnail = video.Thumbnail;

                var videoExtension = video.Extension;

                videoRepository.Delete(video, User.Identity.Name);

                var thumbnailId = thumbnail.Id;

                thumbnailRepository.Delete(thumbnail, User.Identity.Name);

                try
                {
                    mediaFileManager.DeleteThumbnailFile(thumbnailId, Constants.VIDEO_THUMBNAIL_EXTENSION);
                    mediaFileManager.DeleteVideoFile(modelInput.TrailerVideoId, videoExtension);
                }
                catch { }
            }

            entity.SetGenres(null);
            entity.SetProducerHouses(null);

            Repository.Update(entity, User.Identity.Name);

            var genres = new List<Genre>();

            if (modelInput.SelectableGenres != null)
            {
                foreach (var item in modelInput.SelectableGenres.Where(item => item.Selected))
                {
                    genres.Add(genreRepository.Get(item.Id));
                }
            }

            entity.SetGenres(genres);

            var producerHouses = new List<ProducerHouse>();

            if (modelInput.SelectableProducerHouses != null)
            {
                foreach (var item in modelInput.SelectableProducerHouses.Where(item => item.Selected))
                {
                    producerHouses.Add(producerHouseRepository.Get(item.Id));
                }
            }

            entity.SetProducerHouses(producerHouses);

            Repository.Update(entity, User.Identity.Name);
        }

        protected override void ValidateCreate(ProductionInputViewModel modelInput)
        {
            if (modelInput.LanguageId <= 0)
            {
                ModelState.AddModelError("LanguageId", localizer["LanguageSelectionRequiredMessage"]);
            }

            if (modelInput.CountryId <= 0)
            {
                ModelState.AddModelError("CountryId", localizer["CountrySelectionRequiredMessage"]);
            }

            if (modelInput.ClasificationId <= 0)
            {
                ModelState.AddModelError("ClasificationId", localizer["ClasificationSelectionRequiredMessage"]);
            }

            base.ValidateCreate(modelInput);
        }

        protected override void ValidateEdit(ProductionInputViewModel modelInput)
        {
            if (modelInput.LanguageId <= 0)
            {
                ModelState.AddModelError("LanguageId", localizer["LanguageSelectionRequiredMessage"]);
            }

            if (modelInput.CountryId <= 0)
            {
                ModelState.AddModelError("CountryId", localizer["CountrySelectionRequiredMessage"]);
            }

            if (modelInput.ClasificationId <= 0)
            {
                ModelState.AddModelError("ClasificationId", localizer["ClasificationSelectionRequiredMessage"]);
            }

            base.ValidateEdit(modelInput);
        }

        private void PopulateModelInputForAvailableCountries(ProductionInputViewModel inputModel)
        {
            var entities = countryRepository.GetAll();

            var selectListItems = new List<SelectListItem>();

            foreach (var entity in entities)
            {
                selectListItems.Add(new SelectListItem { Value = entity.Id.ToString(), Text = entity.Name });
            }

            inputModel.Countries = selectListItems;
        }

        private void PopulateModelInputForAvailableClasifications(ProductionInputViewModel inputModel)
        {
            var entities = clasificationRepository.GetAll();

            var selectListItems = new List<SelectListItem>();

            foreach (var entity in entities)
            {
                selectListItems.Add(new SelectListItem { Value = entity.Id.ToString(), Text = entity.Name });
            }

            inputModel.Clasifications = selectListItems;
        }

        private void PopulateModelInputForAvailableLanguages(ProductionInputViewModel inputModel)
        {
            var entities = languageRepository.GetAll();

            var selectListItems = new List<SelectListItem>();

            foreach (var entity in entities)
            {
                selectListItems.Add(new SelectListItem { Value = entity.Id.ToString(), Text = entity.Name });
            }

            inputModel.Languages = selectListItems;
        }

        private void PopulateModelInputForAvailableGenres(ProductionInputViewModel modelInput)
        {
            var entities = genreRepository.GetAll().ToList();

            modelInput.SelectableGenres = entities.Select(genre => new SelectionItemInputViewModel<int>
            {
                Id = genre.Id,
                Selected = false,
                Label = genre.Name
            }).ToList();
        }

        private void PopulateModelInputForAvailableProducerHouses(ProductionInputViewModel modelInput)
        {
            var entities = producerHouseRepository.GetAll().ToList();

            modelInput.SelectableProducerHouses = entities.Select(org => new SelectionItemInputViewModel<long>
            {
                Id = org.Id,
                Selected = false,
                Label = org.Name
            }).ToList();
        }
               
        private void PopulateModelInputForAvailableCollections(ProductionInputViewModel inputModel)
        {
            inputModel.AvailableCollectionSelectListItems.Add(new SelectListItem
            {
                Value = System.Guid.Empty.ToString(),
                Text = "Ninguna"
            });

            var entities = collectionRepository.GetAll().ToList().OrderBy(e => e.GetTreeId()).ToArray();

            foreach (var entity in entities)
            {
                inputModel.AvailableCollectionSelectListItems.Add(new SelectListItem { Value = entity.Id.ToString(), Text = entity.GetTreeName() });
            }
        }

        private void PopulateModelInputForAvailableCategories(ProductionInputViewModel inputModel)
        {
            inputModel.AvailableCategorySelectListItems.Add(new SelectListItem
            {
                Value = System.Guid.Empty.ToString(),
                Text = "Ninguna"
            });

            var entities = categoryRepository.GetAll().ToList().OrderBy(e => e.GetTreeId()).ToArray();

            foreach (var entity in entities)
            {
                inputModel.AvailableCategorySelectListItems.Add(new SelectListItem { Value = entity.Id.ToString(), Text = entity.GetTreeName() });
            }
        }

        protected override ProductionInputViewModel ConfigureCreate(ProductionInputViewModel modelInput)
        {
            PopulateModelInputForAvailableClasifications(modelInput);
            PopulateModelInputForAvailableCountries(modelInput);
            PopulateModelInputForAvailableGenres(modelInput);
            PopulateModelInputForAvailableLanguages(modelInput);
            PopulateModelInputForAvailableProducerHouses(modelInput);
            PopulateModelInputForAvailableCollections(modelInput);
            PopulateModelInputForAvailableCategories(modelInput);

            return base.ConfigureCreate(modelInput);
        }

        protected override ProductionInputViewModel ConfigurePostFailCreateValidation(ProductionInputViewModel modelInput)
        {
            PopulateModelInputForAvailableClasifications(modelInput);
            PopulateModelInputForAvailableCountries(modelInput);
            PopulateModelInputForAvailableGenres(modelInput);
            PopulateModelInputForAvailableLanguages(modelInput);
            PopulateModelInputForAvailableProducerHouses(modelInput);
            PopulateModelInputForAvailableCollections(modelInput);
            PopulateModelInputForAvailableCategories(modelInput);

            return base.ConfigurePostFailCreateValidation(modelInput);
        }

        protected override ProductionInputViewModel ConfigureEdit(Production entity, ProductionInputViewModel modelInput)
        {
            PopulateModelInputForAvailableClasifications(modelInput);
            PopulateModelInputForAvailableCountries(modelInput);
            PopulateModelInputForAvailableGenres(modelInput);
            PopulateModelInputForAvailableLanguages(modelInput);
            PopulateModelInputForAvailableProducerHouses(modelInput);
            PopulateModelInputForAvailableCollections(modelInput);
            PopulateModelInputForAvailableCategories(modelInput);

            var genres = entity.GetGenres();

            foreach (var item in modelInput.SelectableGenres)
            {
                item.Selected = genres.Any(genre => genre.Id == item.Id);
            }

            var producerHouses = entity.GetProducerHouses();

            foreach (var item in modelInput.SelectableProducerHouses)
            {
                item.Selected = producerHouses.Any(org => org.Id == item.Id);
            }

            return base.ConfigureEdit(entity, modelInput);
        }

        protected override ProductionInputViewModel ConfigurePostFailEditValidation(ProductionInputViewModel modelInput)
        {
            PopulateModelInputForAvailableClasifications(modelInput);
            PopulateModelInputForAvailableCountries(modelInput);
            PopulateModelInputForAvailableGenres(modelInput);
            PopulateModelInputForAvailableLanguages(modelInput);
            PopulateModelInputForAvailableProducerHouses(modelInput);
            PopulateModelInputForAvailableCollections(modelInput);
            PopulateModelInputForAvailableCategories(modelInput);

            return base.ConfigurePostFailEditValidation(modelInput);
        }

        [Route("edit/{key}/[action]")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerEditor)]
        public IActionResult AwardWinnings(long key)
        {
            if (awardRepository.Count() <= 0)
            {
                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(new NotificationViewModel
                {
                    Type = NotificationType.Warning,
                    Title = "RequiredDataIncompleteTitle",
                    Message = "RequiredDataIncompleteMessage",
                    Messages = new string[] { "AtLastOneAwardRequiredMessage" }
                });

                return RedirectToAction(GetDefaultActionName());
            }

            try
            {
                var entity = Repository.Get(key);

                var model = new ProductionAwardWinningsViewModel
                {
                    Display = new ProductionDisplayViewModel(),
                    Awards = new List<SelectListItem>()
                };

                model.Display.Import(entity);

                var awards = awardRepository.GetAll();

                foreach (var award in awards)
                {
                    model.Awards.Add(new SelectListItem { Value = award.Id.ToString(), Text = award.Name });
                }

                return View(model);
            }
            catch (System.Exception e)
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

        [Route("edit/awardwinnings/add")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAwardWinning(AwardWinningInputViewModel modelInput)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetInvalidMessage()
            };

            Production production = null;

            try
            {
                production = Repository.Get(modelInput.ProductionId);
            }
            catch { }

            if (production == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            if (ModelState.IsValid)
            {
                var entity = modelInput.Export();

                entity.Production = production;
                entity.Award = awardRepository.Get(modelInput.AwardId);

                if (production.AwardWinnings == null)
                {
                    production.AwardWinnings = new List<AwardWinning>();
                }

                production.AwardWinnings.Add(entity);

                Repository.Update(production, User.Identity.Name);

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

            return RedirectToAction("awardwinnings", new { key = production.Id });
        }

        [Route("edit/awardwinnings/edit")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAwardWinning(AwardWinningInputViewModel modelInput)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetInvalidMessage()
            };

            Production production = null;

            try
            {
                production = Repository.Get(modelInput.ProductionId);
            }
            catch { }

            if (production == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            if (ModelState.IsValid)
            {
                var entity = awardWinningRepository.FindOneBy(e => e.Id == modelInput.Id && e.Production.Id == production.Id);

                if (entity != null)
                {
                    modelInput.Mergue(entity);

                    entity.Award = awardRepository.Get(modelInput.AwardId);

                    awardWinningRepository.Update(entity, User.Identity.Name);

                    notification.Type = NotificationType.Success;
                    notification.Message = GetEditionSuccessMessage();
                }
                else
                {
                    notification.Message = GetNotFoundMessage();
                }
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

            return RedirectToAction("awardwinnings", new { key = production.Id });
        }

        [Route("edit/awardwinnings/delete")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAwardWinning(AwardWinningDisplayViewModel modelDisplay)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetNotFoundMessage()
            };

            Production production = null;

            try
            {
                production = Repository.Get(modelDisplay.ProductionId);
            }
            catch { }

            if (production == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            try
            {
                var entity = awardWinningRepository.FindOneBy(e => e.Id == modelDisplay.Id && e.Production.Id == production.Id);

                awardWinningRepository.Delete(entity, User.Identity.Name);

                notification.Type = NotificationType.Success;
                notification.Message = GetDeletionSuccessMessage();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                notification.Type = NotificationType.Warning;
                notification.Message = GetOnDeletionDependecyConstrainErrorMessage();
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction("awardwinnings", new { key = production.Id });
        }

        // Working on Participations

        [Route("edit/{key}/[action]")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerEditor)]
        public IActionResult Participations(long key)
        {
            if (personalityRepository.Count() <= 0)
            {
                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(new NotificationViewModel
                {
                    Type = NotificationType.Warning,
                    Title = "RequiredDataIncompleteTitle",
                    Message = "RequiredDataIncompleteMessage",
                    Messages = new string[] { "AtLastOnePersonalityRequiredMessage" }
                });

                return RedirectToAction(GetDefaultActionName());
            }

            try
            {
                var entity = Repository.Get(key);

                var model = new ProductionParticipationsManagementViewModel
                {
                    Display = new ProductionDisplayViewModel(),
                    Personalities = new List<SelectListItem>(),
                    Participations = entity.Participations,
                    ProductionRoles = new List<SelectListItem>
                    {
                        new SelectListItem { Value = ProductionRole.Production.ToString(), Text = localizer[ProductionRole.Production.ToString()] },
                        new SelectListItem { Value = ProductionRole.Direction.ToString(), Text = localizer[ProductionRole.Direction.ToString()] },
                        new SelectListItem { Value = ProductionRole.Interpretation.ToString(), Text = localizer[ProductionRole.Interpretation.ToString()] },
                        new SelectListItem { Value = ProductionRole.Screenplay.ToString(), Text = localizer[ProductionRole.Screenplay.ToString()] },
                        new SelectListItem { Value = ProductionRole.Photography.ToString(), Text = localizer[ProductionRole.Photography.ToString()] },
                        new SelectListItem { Value = ProductionRole.Sound.ToString(), Text =  localizer[ProductionRole.Sound.ToString()] },
                        new SelectListItem { Value = ProductionRole.Edition.ToString(), Text = localizer[ProductionRole.Edition.ToString()] },
                        new SelectListItem { Value = ProductionRole.ArtisticManagement.ToString(), Text =  localizer[ProductionRole.ArtisticManagement.ToString()] }
                    }
            };

                model.Display.Import(entity);

                var personalities = personalityRepository.GetAll();

                foreach (var personality in personalities)
                {
                    model.Personalities.Add(new SelectListItem { Value = personality.Id.ToString(), Text = personality.Name + " " + personality.LastName });
                }

                return View(model);
            }
            catch (System.Exception e)
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

        [Route("edit/participations/addpicking")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddParticipation(ParticipationInputViewModel modelInput)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetInvalidMessage()
            };

            Production production = null;

            try
            {
                production = Repository.Get(modelInput.ProductionId);
            }
            catch { }

            if (production == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            if (ModelState.IsValid)
            {
                var entity = modelInput.Export();

                entity.Production = production;
                entity.Personality = personalityRepository.Get(modelInput.PersonalityId);

                if (production.Participations == null)
                {
                    production.Participations = new List<Participation>();
                }

                production.Participations.Add(entity);

                Repository.Update(production, User.Identity.Name);

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

            return RedirectToAction("participations", new { key = production.Id });
        }

        [Route("edit/participations/addcreating")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPersonalityParticipation(PersonalityParticipationCreateViewModel modelInput)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetInvalidMessage()
            };

            Production production = null;

            try
            {
                production = Repository.Get(modelInput.ProductionId);
            }
            catch { }

            if (production == null)
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
                var entity = new Participation
                {
                    Production = production,
                    Personality = new Personality
                    {
                        Name = modelInput.Name,
                        LastName = modelInput.LastName,
                        BornDate = modelInput.BornDate,
                        Sex = modelInput.Sex,
                        About = modelInput.About
                    },
                    Role = modelInput.ProductionRole,
                    Specification = modelInput.Specification
                };

                if (production.Participations == null)
                {
                    production.Participations = new List<Participation>();
                }

                production.Participations.Add(entity);

                Repository.Update(production, User.Identity.Name);

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

            return RedirectToAction("participations", new { key = production.Id });
        }

        [Route("edit/participations/edit")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditParticipation(ParticipationInputViewModel modelInput)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetInvalidMessage()
            };

            Production production = null;

            try
            {
                production = Repository.Get(modelInput.ProductionId);
            }
            catch { }

            if (production == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            if (ModelState.IsValid)
            {
                var entity = participationRepository.FindOneBy(e => e.Id == modelInput.Id && e.Production.Id == production.Id);

                if (entity != null)
                {
                    modelInput.Mergue(entity);

                    entity.Personality = personalityRepository.Get(modelInput.PersonalityId);

                    participationRepository.Update(entity, User.Identity.Name);

                    notification.Type = NotificationType.Success;
                    notification.Message = GetEditionSuccessMessage();
                }
                else
                {
                    notification.Message = GetNotFoundMessage();
                }
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

            return RedirectToAction("participations", new { key = production.Id });
        }

        [Route("edit/participations/delete")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteParticipation(ParticipationDisplayViewModel modelDisplay)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetNotFoundMessage()
            };

            Production production = null;

            try
            {
                production = Repository.Get(modelDisplay.ProductionId);
            }
            catch { }

            if (production == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            try
            {
                var entity = participationRepository.FindOneBy(e => e.Id == modelDisplay.Id && e.Production.Id == production.Id);

                participationRepository.Delete(entity, User.Identity.Name);

                notification.Type = NotificationType.Success;
                notification.Message = GetDeletionSuccessMessage();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                notification.Type = NotificationType.Warning;
                notification.Message = GetOnDeletionDependecyConstrainErrorMessage();
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction("participations", new { key = production.Id });
        }

        // Working on CoCountries

        [Route("edit/{key}/[action]")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerEditor)]
        public IActionResult CoCountries(long key)
        {
            try
            {
                var entity = Repository.Get(key);

                var model = new ProductionCoCountriesManagementViewModel
                {
                    Display = new ProductionDisplayViewModel(),
                    CountrySelecItems = new List<SelectListItem>(),
                    Countries = new List<Country>()
                };

                foreach (var bridge in entity.CountryCoProductionBridges)
                {
                    model.Countries.Add(bridge.Country);
                }

                model.Display.Import(entity);

                var countries = countryRepository.GetAll();

                foreach (var country in countries)
                {
                    model.CountrySelecItems.Add(new SelectListItem { Value = country.Id.ToString(), Text = country.Name });
                }

                return View(model);
            }
            catch (System.Exception e)
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

        [Route("edit/cocountries/add")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCoCountry(CoCountryInputViewModel modelInput)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetInvalidMessage()
            };

            Production production = null;

            try
            {
                production = Repository.Get(modelInput.ProductionId);
            }
            catch { }

            if (production == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            if (production.Country.Id == modelInput.CountryId)
            {
                ModelState.AddModelError("CountryId", localizer["ProductionCountryColisionMesssage"]);
            }

            if (ModelState.IsValid)
            {
                var bridge = new CountryCoProductionBridge
                {
                    Production = production,
                    ProductionId = production.Id,
                    Country = countryRepository.Get(modelInput.CountryId),
                    CountryId = modelInput.CountryId
                };

                production.CountryCoProductionBridges.Add(bridge);

                Repository.Update(production, User.Identity.Name);

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

            return RedirectToAction("cocountries", new { key = production.Id });
        }

        [Route("edit/cocountries/delete")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCoCountry(CoCountryDisplayViewModel model)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetNotFoundMessage()
            };

            Production production = null;

            try
            {
                production = Repository.Get(model.ProductionId);
            }
            catch { }

            if (production == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            try
            {
                var target = production.CountryCoProductionBridges.First(bridge => bridge.CountryId == model.CountryId);

                production.CountryCoProductionBridges.Remove(target);

                Repository.Update(production, User.Identity.Name);

                notification.Type = NotificationType.Success;
                notification.Message = GetDeletionSuccessMessage();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                notification.Type = NotificationType.Warning;
                notification.Message = GetOnDeletionDependecyConstrainErrorMessage();
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction("cocountries", new { key = production.Id });
        }
    }
}
