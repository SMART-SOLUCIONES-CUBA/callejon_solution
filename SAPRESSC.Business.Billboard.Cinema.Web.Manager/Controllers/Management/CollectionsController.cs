using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.SecurityShared;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Controllers.Management
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Area("cinemabillboard")]
    public class CollectionsController : Core.Web.Management.Controllers.AbstractEntityManagementController<Collection, System.Guid, CollectionInputViewModel, CollectionDisplayViewModel>
    {
        private const string POSTER_IMAGE_FILE_FIELD_NAME = "PosterImageFile";
        private const string PLACARD_IMAGE_FILE_FIELD_NAME = "PlacardImageFile";

        private readonly IEntityRepository<Category, System.Guid> categoryRepository;
        private readonly IEntityRepository<Middleware.Media.Persistence.Entities.Image, System.Guid> imageRepository;
        private readonly Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade mediaFileManagerFacade;

        public CollectionsController(Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade mediaFileManagerFacade, IEntityRepository<Middleware.Media.Persistence.Entities.Image, System.Guid> imageRepository, IEntityRepository<Category, System.Guid> categoryRepository, IEntityRepository<Collection, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<CollectionsController> localizer, ILogger<CollectionsController> logger) : base(repository, localizer, logger)
        {
            this.categoryRepository = categoryRepository;
            this.imageRepository = imageRepository;
            this.mediaFileManagerFacade = mediaFileManagerFacade;
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CollectionManagerViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CollectionManagerCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CollectionManagerCreator)]
        public override IActionResult Create(CollectionInputViewModel modelInput)
        {
            if (Request.Form.Files.Count > 0)
            {
                foreach (var formFile in Request.Form.Files)
                {
                    if (formFile.Length > 0)
                    {
                        ModelState.ClearValidationState(formFile.Name);

                        switch (formFile.Name)
                        {
                            case POSTER_IMAGE_FILE_FIELD_NAME:
                                modelInput.PosterImageFile = formFile;

                                break;

                            case PLACARD_IMAGE_FILE_FIELD_NAME:
                                modelInput.PlacardImageFile = formFile;

                                break;
                        }
                    }
                }

                TryUpdateModelAsync(modelInput).Wait();
            }

            try
            {
                return base.Create(modelInput);
            }
            catch (Common.InvalidModelException e)
            {
                ModelState.AddModelError(e.Field, e.Message);
            }
            catch (System.Exception e)
            {
                throw e;
            }

            return null;
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CollectionManagerEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CollectionManagerEditor)]
        public override IActionResult Edit(CollectionInputViewModel modelInput)
        {
            if (Request.Form.Files.Count > 0)
            {
                foreach (var formFile in Request.Form.Files)
                {
                    if (formFile.Length > 0)
                    {
                        ModelState.ClearValidationState(formFile.Name);

                        switch (formFile.Name)
                        {
                            case POSTER_IMAGE_FILE_FIELD_NAME:
                                modelInput.PosterImageFile = formFile;

                                break;

                            case PLACARD_IMAGE_FILE_FIELD_NAME:
                                modelInput.PlacardImageFile = formFile;

                                break;
                        }
                    }
                }

                TryUpdateModelAsync(modelInput).Wait();
            }

            if (modelInput.PosterImageFile == null)
            {
                ModelState.ClearValidationState(POSTER_IMAGE_FILE_FIELD_NAME);
                ModelState.MarkFieldValid(POSTER_IMAGE_FILE_FIELD_NAME);
            }

            if (modelInput.PlacardImageFile == null)
            {
                ModelState.ClearValidationState(PLACARD_IMAGE_FILE_FIELD_NAME);
                ModelState.MarkFieldValid(PLACARD_IMAGE_FILE_FIELD_NAME);
            }

            try
            {
                return base.Edit(modelInput);
            }
            catch (Common.InvalidModelException e)
            {
                ModelState.AddModelError(e.Field, e.Message);
            }
            catch (System.Exception e)
            {
                throw e;
            }

            return null;
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CollectionManagerRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CollectionManagerRemover)]
        public override IActionResult Delete(CollectionDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PopulateViewModelDisplayExtras(CollectionDisplayViewModel modelDisplay, Collection entity)
        {
            base.PopulateViewModelDisplayExtras(modelDisplay, entity);

            if (entity.CategoryId != System.Guid.Empty)
            {
                modelDisplay.Category = new CategoryDisplayViewModel();

                modelDisplay.Category.Import(categoryRepository.Get(entity.CategoryId));
            }
        }

        protected override void PreCreate(Collection entity, CollectionInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            Middleware.Media.Persistence.Entities.Image poster = null;
            Middleware.Media.Persistence.Entities.Image placard = null;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.PosterImageFile.CopyTo(memoryStream);

                memoryStream.Position = 0;

                using (var imageSkData = SkiaSharp.SKData.Create(memoryStream))
                {
                    using (var imageSkBitmap = SkiaSharp.SKBitmap.Decode(imageSkData))
                    {
                        if (imageSkBitmap.Width >= imageSkBitmap.Height)
                        {
                            throw new Common.InvalidModelException(POSTER_IMAGE_FILE_FIELD_NAME, localizer["PortraitImageRequiredMessage"]);
                        }

                        poster = new Middleware.Media.Persistence.Entities.Image
                        {
                            ContentType = modelInput.PosterImageFile.ContentType,
                            Length = modelInput.PosterImageFile.Length,
                            Width = imageSkBitmap.Width,
                            Height = imageSkBitmap.Height
                        };
                    }
                }
            }

            if (modelInput.PlacardImageFile != null)
            {
                using (var memoryStream = new System.IO.MemoryStream())
                {
                    modelInput.PlacardImageFile.CopyTo(memoryStream);

                    memoryStream.Position = 0;

                    using (var imageSkData = SkiaSharp.SKData.Create(memoryStream))
                    {
                        using (var imageSkBitmap = SkiaSharp.SKBitmap.Decode(imageSkData))
                        {
                            if (imageSkBitmap.Width <= imageSkBitmap.Height)
                            {
                                throw new Common.InvalidModelException(PLACARD_IMAGE_FILE_FIELD_NAME, localizer["LandscapeImageRequiredMessage"]);
                            }

                            placard = new Middleware.Media.Persistence.Entities.Image
                            {
                                ContentType = modelInput.PlacardImageFile.ContentType,
                                Length = modelInput.PlacardImageFile.Length,
                                Width = imageSkBitmap.Width,
                                Height = imageSkBitmap.Height
                            };
                        }
                    }
                }
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.PosterImageFile.CopyTo(memoryStream);

                memoryStream.Position = 0;

                entity.PosterImage = poster;

                entity.PosterImage.About = string.Format("Poster for Collection:{0}", entity.Id);

                mediaFileManagerFacade.SaveImageFileContent(entity.PosterImage.Id, memoryStream.ToArray());
            }

            if (modelInput.PlacardImageFile != null)
            {
                using (var memoryStream = new System.IO.MemoryStream())
                {
                    modelInput.PlacardImageFile.CopyTo(memoryStream);

                    memoryStream.Position = 0;

                    entity.PlacardImage = placard;

                    entity.PlacardImage.About = string.Format("Placard for Collection:{0}", entity.Id);

                    mediaFileManagerFacade.SaveImageFileContent(entity.PlacardImage.Id, memoryStream.ToArray());
                }
            }
        }

        protected override void PreEdit(Collection entity, CollectionInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            Middleware.Media.Persistence.Entities.Image newPoster = null;
            Middleware.Media.Persistence.Entities.Image newPlacard = null;

            if (modelInput.PosterImageFile != null)
            {
                using (var memoryStream = new System.IO.MemoryStream())
                {
                    modelInput.PosterImageFile.CopyTo(memoryStream);

                    memoryStream.Position = 0;

                    using (var imageSkData = SkiaSharp.SKData.Create(memoryStream))
                    {
                        using (var imageSkBitmap = SkiaSharp.SKBitmap.Decode(imageSkData))
                        {
                            if (imageSkBitmap.Width >= imageSkBitmap.Height)
                            {
                                throw new Common.InvalidModelException(POSTER_IMAGE_FILE_FIELD_NAME, localizer["PortraitImageRequiredMessage"]);
                            }

                            newPoster = new Middleware.Media.Persistence.Entities.Image
                            {
                                ContentType = modelInput.PosterImageFile.ContentType,
                                Length = modelInput.PosterImageFile.Length,
                                Width = imageSkBitmap.Width,
                                Height = imageSkBitmap.Height
                            };
                        }
                    }
                }
            }

            if (modelInput.PlacardImageFile != null)
            {
                using (var memoryStream = new System.IO.MemoryStream())
                {
                    modelInput.PlacardImageFile.CopyTo(memoryStream);

                    memoryStream.Position = 0;

                    using (var imageSkData = SkiaSharp.SKData.Create(memoryStream))
                    {
                        using (var imageSkBitmap = SkiaSharp.SKBitmap.Decode(imageSkData))
                        {
                            if (imageSkBitmap.Width <= imageSkBitmap.Height)
                            {
                                throw new Common.InvalidModelException(PLACARD_IMAGE_FILE_FIELD_NAME, localizer["LandscapeImageRequiredMessage"]);
                            }

                            newPlacard = new Middleware.Media.Persistence.Entities.Image
                            {
                                ContentType = modelInput.PlacardImageFile.ContentType,
                                Length = modelInput.PlacardImageFile.Length,
                                Width = imageSkBitmap.Width,
                                Height = imageSkBitmap.Height
                            };
                        }
                    }
                }
            }

            if (newPoster != null)
            {
                var oldPoster = entity.PosterImage;

                using (var memoryStream = new System.IO.MemoryStream())
                {
                    modelInput.PosterImageFile.CopyTo(memoryStream);

                    memoryStream.Position = 0;

                    entity.PosterImage = newPoster;

                    entity.PosterImage.About = string.Format("Poster for Collection:{0}", entity.Id);

                    mediaFileManagerFacade.SaveImageFileContent(entity.PosterImage.Id, memoryStream.ToArray());
                }

                mediaFileManagerFacade.DeleteImageFile(oldPoster.Id);

                imageRepository.Delete(oldPoster, User.Identity.Name);
            }

            if (newPlacard != null)
            {
                var oldPlacard = entity.PlacardImage;

                using (var memoryStream = new System.IO.MemoryStream())
                {
                    modelInput.PlacardImageFile.CopyTo(memoryStream);

                    memoryStream.Position = 0;

                    entity.PlacardImage = newPlacard;

                    entity.PlacardImage.About = string.Format("Placard for Collection:{0}", entity.Id);

                    mediaFileManagerFacade.SaveImageFileContent(entity.PlacardImage.Id, memoryStream.ToArray());
                }

                if (oldPlacard != null)
                {
                    mediaFileManagerFacade.DeleteImageFile(oldPlacard.Id);

                    imageRepository.Delete(oldPlacard, User.Identity.Name);
                }
            }
            else if (modelInput.ClearPlacard && entity.PlacardImage != null)
            {
                var oldPlacard = entity.PlacardImage;

                entity.PlacardImage = null;

                mediaFileManagerFacade.DeleteImageFile(oldPlacard.Id);

                imageRepository.Delete(oldPlacard, User.Identity.Name);
            }
        }

        protected override void PostDelete(Collection entity)
        {
            base.PostDelete(entity);

            if (entity.PosterImage != null)
            {
                imageRepository.Delete(entity.PosterImage, User.Identity.Name);

                mediaFileManagerFacade.DeleteImageFile(entity.PosterImage.Id);
            }

            if (entity.PlacardImage != null)
            {
                imageRepository.Delete(entity.PlacardImage, User.Identity.Name);

                mediaFileManagerFacade.DeleteImageFile(entity.PlacardImage.Id);
            }
        }

        protected override CollectionInputViewModel ConfigureCreate(CollectionInputViewModel modelInput)
        {
            var result = base.ConfigureCreate(modelInput);

            PopulateModelInputForAvailableParents(modelInput);
            PopulateModelInputForAvailableCategories(modelInput);

            return result;
        }

        protected override CollectionInputViewModel ConfigureEdit(Collection entity, CollectionInputViewModel modelInput)
        {
            var result = base.ConfigureEdit(entity, modelInput);

            PopulateModelInputForAvailableParents(modelInput);
            PopulateModelInputForAvailableCategories(modelInput);

            return result;
        }

        protected override CollectionInputViewModel ConfigurePostFailCreateValidation(CollectionInputViewModel modelInput)
        {
            var result = base.ConfigurePostFailCreateValidation(modelInput);

            PopulateModelInputForAvailableParents(modelInput);
            PopulateModelInputForAvailableCategories(modelInput);

            return result;
        }

        protected override CollectionInputViewModel ConfigurePostFailEditValidation(CollectionInputViewModel modelInput)
        {
            var result = base.ConfigurePostFailEditValidation(modelInput);

            PopulateModelInputForAvailableParents(modelInput);
            PopulateModelInputForAvailableCategories(modelInput);

            return result;
        }

        private void PopulateModelInputForAvailableParents(CollectionInputViewModel inputModel)
        {
            inputModel.AvailableCollectionSelectListItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = System.Guid.Empty.ToString(),
                Text = "Ninguna"
            });

            var entities = Repository.GetAll().ToList().Where(e => !e.GetTreeId().Contains(inputModel.Id.ToString())).OrderBy(e => e.GetTreeId()).ToArray();

            foreach (var entity in entities)
            {
                inputModel.AvailableCollectionSelectListItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.GetTreeName() });
            }
        }

        private void PopulateModelInputForAvailableCategories(CollectionInputViewModel inputModel)
        {
            inputModel.AvailableCategorySelectListItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = System.Guid.Empty.ToString(),
                Text = "Ninguna"
            });

            var entities = categoryRepository.FindBy(e => e.Id != inputModel.Id).ToList().OrderBy(e => e.GetTreeId()).ToArray();

            foreach (var entity in entities)
            {
                inputModel.AvailableCategorySelectListItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.GetTreeName() });
            }
        }
    }
}
