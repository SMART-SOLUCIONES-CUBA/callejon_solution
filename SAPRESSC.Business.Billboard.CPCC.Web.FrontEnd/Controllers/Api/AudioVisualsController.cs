using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Common.Extensions;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Extensions;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("mediastore")]
    public class AudioVisualsController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly Microsoft.Extensions.Options.IOptions<Cinema.Web.Common.Options.MediaStoreOptions> mediaStoreOptions;
        private readonly IEntityRepository<Production, long> productionRepository;
        private readonly IEntityRepository<Search, long> searchRepository;
        private readonly IEntityRepository<Category, System.Guid> categoryRepository;
        private readonly IEntityRepository<Middleware.Region.Persistence.Entities.Country, int> countryRepository;
        private readonly IEntityRepository<ProducerHouse, long> producerHouseRepository;
        private readonly MediaToolkit.Services.IMediaToolkitService mediaToolkitService;

        public AudioVisualsController(MediaToolkit.Services.IMediaToolkitService mediaToolkitService, IEntityRepository<ProducerHouse, long> producerHouseRepository, IEntityRepository<Middleware.Region.Persistence.Entities.Country, int> countryRepository, IEntityRepository<Category, System.Guid> categoryRepository, IEntityRepository<Search, long> searchRepository, IEntityRepository<Production, long> productionRepository, Microsoft.Extensions.Options.IOptions<Cinema.Web.Common.Options.MediaStoreOptions> mediaStoreOptions, Microsoft.Extensions.Localization.IStringLocalizer<AudioVisualsController> localizer, ILogger<AudioVisualsController> logger) : base(localizer, logger)
        {
            this.mediaStoreOptions = mediaStoreOptions;
            this.productionRepository = productionRepository;
            this.searchRepository = searchRepository;
            this.categoryRepository = categoryRepository;
            this.countryRepository = countryRepository;
            this.producerHouseRepository = producerHouseRepository;
            this.mediaToolkitService = mediaToolkitService;
        }

        [HttpGet("populars")]
        [HttpGet("populars/sheet/{sheet}/limit/{limit}")]
        public virtual async System.Threading.Tasks.Task<IActionResult> GetPopularsAsync(int sheet = 1, int limit = 20)
        {
            return await BuildPaginationModelActionResultAsync(sheet, limit, item => item, true, new Helpers.Comparison.ProductionPopularityComparisonHelper());
        }

        [HttpGet("lasts")]
        [HttpGet("lasts/sheet/{sheet}/limit/{limit}")]
        public virtual async System.Threading.Tasks.Task<IActionResult> GetLastsAsync(int sheet = 1, int limit = 20)
        {
            return await BuildPaginationModelActionResultAsync(sheet, limit, entity => entity.Id, true);
        }

        [HttpGet("recomendeds")]
        [HttpGet("recomendeds/sheet/{sheet}/limit/{limit}")]
        public virtual async System.Threading.Tasks.Task<IActionResult> GetRecomendedsAsync(int sheet = 1, int limit = 20)
        {
            return await BuildPaginationModelActionResultAsync(sheet, limit, RecomendationOrdering, true);
        }

        [HttpPost("search/sheet/{sheet}")]
        [HttpPost("search/sheet/{sheet}/limit/{limit}")]
        public virtual async System.Threading.Tasks.Task<IActionResult> SearhAsync([FromBody]SearchModel search, int sheet = 1, int limit = 20)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (search.Id <= 0)
                    {
                        var entity = new Search
                        {
                            SubscriptionId = search.SubscriptionId,
                            TitleTemathic = search.TitleTemathic,
                            Year = search.Year,
                            Category = search.Category == null ? null : categoryRepository.Get(search.Category.Id),
                            Country = search.Country == null ? null : countryRepository.Get(search.Country.Id),
                            ProducerHouse = search.ProducerHouse == null ? null : producerHouseRepository.Get(search.ProducerHouse.Id)                        
                        };

                        if (search.Genres != null)
                        {
                            entity.GenreBridges.AddRange(search.Genres.Select(item => new SearchGenreBridge
                            {
                                Search = entity,
                                GenreId = item.Id
                            }));
                        }

                        if (search.Tags != null)
                        {
                            entity.TagBridges.AddRange(search.Tags.Select(item => new SearchTagBridge
                            {
                                Search = entity,
                                TagId = item.Id
                            }));
                        }

                        if (search.Personalities != null)
                        {
                            entity.PersonalityBridges.AddRange(search.Personalities.Select(item => new SearchPersonalityBridge
                            {
                                Search = entity,
                                PersonalityId = item.Id
                            }));
                        }

                        entity = searchRepository.Add(entity, User.Identity.Name);

                        search.Id = entity.Id;
                    }

                    return await BuildPaginationModelActionResultAsync(sheet, limit, e => e.Title, false, searchModel: search);
                }
                catch (System.Exception e)
                {
                    logger.LogError(e, e.Message);

                    return Conflict();
                }
            }

            return BadRequest();
        }

        protected async System.Threading.Tasks.Task<IActionResult> BuildPaginationModelActionResultAsync<TOrderingKey>(int page, int limit, System.Func<Production, TOrderingKey> ordering, bool descending, System.Collections.Generic.IComparer<TOrderingKey> comparer = null, SearchModel searchModel = null)
        {
            IQueryable<Production> selection = null;

            System.Linq.Expressions.Expression<System.Func<Production, bool>> filterSelector = null;

            if (searchModel != null)
            {
                if (searchModel.Category != null)
                {
                    filterSelector = SearchToSelector(searchModel, GetCategoryDepthIds(categoryRepository.Get(searchModel.Category.Id)));
                }
                else
                {
                    filterSelector = SearchToSelector(searchModel);
                }
            }

            if (filterSelector != null)
            {
                selection = productionRepository.FindBy(entity => entity.CategoryId != null).Where(filterSelector);
            }
            else
            {
                selection = productionRepository.FindBy(entity => entity.CategoryId != null);
            }

            var maxCount = selection.Count();

            var paginationModel = new PaginationModel<AudioVisualModel>
            {
                Limit = limit,
                Page = 0,
                HasNext = false,
                HasPrevious = false,
                List = new System.Collections.Generic.List<AudioVisualModel>()
            };

            if (maxCount > 0)
            {
                var rest = maxCount % limit;

                var maxPageCount = maxCount / limit;

                if (rest > 0)
                    maxPageCount += 1;

                if (page < 0 || page > maxPageCount)
                {
                    page = 1;
                }

                var entities = new System.Collections.Generic.List<Production>();

                if (!descending)
                {
                    if (comparer != null)
                    {
                        entities = selection.OrderBy(ordering, comparer).Skip((page - 1) * limit).Take(limit).ToList();
                    }
                    else
                    {
                        entities = selection.OrderBy(ordering).Skip((page - 1) * limit).Take(limit).ToList();
                    }
                }
                else
                {
                    if (comparer != null)
                    {
                        entities = selection.OrderByDescending(ordering, comparer).Skip((page - 1) * limit).Take(limit).ToList();
                    }
                    else
                    {
                        entities = selection.OrderByDescending(ordering).Skip((page - 1) * limit).Take(limit).ToList();
                    }
                }

                paginationModel = new PaginationModel<AudioVisualModel>
                {
                    Limit = limit,
                    Page = page,
                    HasPrevious = page > 1,
                    HasNext = page < maxPageCount,
                    MaxPageCount = maxPageCount,
                    List = new System.Collections.Generic.List<AudioVisualModel>(),
                    SerializedContext = searchModel?.Id.ToString()
                };

                var updateRequiredEntities = new System.Collections.Generic.List<Production>();

                foreach (var entity in entities)
                {
                    try
                    {
                        var updateWillRequired = entity.AudioVisual == null;

                        paginationModel.List.Add(await EntityToModelAsync(entity));

                        if (updateWillRequired && entity.AudioVisual != null)
                        {
                            updateRequiredEntities.Add(entity);
                        }
                    }
                    catch (System.Exception e)
                    {
                        logger.LogWarning(e, "XXXXXXXXXXXXXXXXXXXXX La entidad Production con el Id \"{0}\" y el Nombre \"{1}\" fue omitida por producirse un error en su procesazamiento.", entity.Id, entity.Title);
                    }
                }

                if (updateRequiredEntities.Count > 0)
                {
                    var counter = 1;

                    var updateRequiredEntitiesCount = updateRequiredEntities.Count;

                    foreach (var item in updateRequiredEntities)
                    {
                        productionRepository.Update(item, "system", counter == updateRequiredEntitiesCount);

                        counter++;
                    }
                }
            }

            return Ok(paginationModel);
        }

        private System.Linq.Expressions.Expression<System.Func<Production, bool>> SearchToSelector(SearchModel model, System.Collections.Generic.List<System.Guid> categoryDepthIds = null)
        {
            System.Linq.Expressions.Expression<System.Func<Production, bool>> result = null;

            if (!string.IsNullOrEmpty(model.TitleTemathic) && !string.IsNullOrWhiteSpace(model.TitleTemathic))
            {
                logger.LogInformation("SSSSSSSSSSSSSSSSS model.TitleTemathic: {0}", model.TitleTemathic);

                result = entity => entity.Title.Contains(model.TitleTemathic);
            }

            if (model.Year > 0)
            {
                logger.LogInformation("SSSSSSSSSSSSSSSSS model.Year: {0}", model.Year);

                System.Linq.Expressions.Expression<System.Func<Production, bool>> expression = entity => entity.Year == model.Year;

                if (result == null)
                {
                    result = expression;
                }
                else
                {
                    result = result.CombineWithAndAlso(expression);
                }
            }

            if (model.Country != null)
            {
                System.Linq.Expressions.Expression<System.Func<Production, bool>> expression = entity => entity.Country.Id == model.Country.Id || entity.CountryCoProductionBridges.Any(bridge => bridge.CountryId == model.Country.Id);

                if (result == null)
                {
                    result = expression;
                }
                else
                {
                    result = result.CombineWithAndAlso(expression);
                }
            }

            if (model.Category != null)
            {
                System.Linq.Expressions.Expression<System.Func<Production, bool>> expression = entity => entity.CategoryId == model.Category.Id || categoryDepthIds.Any(item => item == entity.CategoryId);

                if (result == null)
                {
                    result = expression;
                }
                else
                {
                    result = result.CombineWithAndAlso(expression);
                }
            }

            if (model.ProducerHouse != null)
            {
                System.Linq.Expressions.Expression<System.Func<Production, bool>> expression = entity => entity.ProducerHouseProductionBridges.Any(bridge => bridge.ProducerHouseId == model.ProducerHouse.Id);

                if (result == null)
                {
                    result = expression;
                }
                else
                {
                    result = result.CombineWithAndAlso(expression);
                }
            }

            if (model.Genres != null && model.Genres.Length > 0)
            {
                System.Linq.Expressions.Expression<System.Func<Production, bool>> expression = entity => entity.ProductionGenreBridges.Any(bridge => model.Genres.Any(item => item.Id == bridge.GenreId));

                if (result == null)
                {
                    result = expression;
                }
                else
                {
                    result = result.CombineWithAndAlso(expression);
                }
            }

            if (model.Personalities != null && model.Personalities.Length > 0)
            {
                System.Linq.Expressions.Expression<System.Func<Production, bool>> expression = entity => entity.Participations.Any(bridge => model.Personalities.Any(item => item.Id == bridge.Personality.Id));

                if (result == null)
                {
                    result = expression;
                }
                else
                {
                    result = result.CombineWithAndAlso(expression);
                }
            }

            if (model.Tags != null && model.Tags.Length > 0)
            {
                System.Linq.Expressions.Expression<System.Func<Production, bool>> expression = entity => entity.TagBridges.Any(bridge => model.Tags.Any(item => item.Id == bridge.TagId));

                if (result == null)
                {
                    result = expression;
                }
                else
                {
                    result = result.CombineWithAndAlso(expression);
                }
            }

            logger.LogInformation("SSSSSSSSSSSSSSSSS expresion: {0}", result);

            return result;
        }

        private System.Func<Production, object> RecomendationOrdering(Production entity)
        {
            // TODO Implement expected behavior!!
            return item => item.Rate;
        }

        private async System.Threading.Tasks.Task<AudioVisualModel> EntityToModelAsync(Production entity)
        {
            var category = CategoryIdToModel(entity.CategoryId.Value);

            category.StorageRelativePath = CategoryToStorageRelativePath(category);

            var model = new AudioVisualModel
            {
                Id = entity.Id,
                Title = entity.Title,
                LocalizedTitle = entity.LocalizedTitle,
                Language = new LanguageModel { Id = entity.Language.Id, Name = entity.Language.Name },
                Clasification = new ClasificationModel { Id = entity.Clasification.Id, Name = entity.Clasification.Name },
                Country = new CountryModel { Id = entity.Country.Id, Name = entity.Country.Name, IsoCode = entity.Country.IsoCode },
                CoCountries = entity.CountryCoProductionBridges.Select(bridge => new CountryModel { Id = bridge.Country.Id, Name = bridge.Country.Name, IsoCode = bridge.Country.IsoCode }).ToArray(),
                Rate = entity.Rate,
                Duration = System.TimeSpan.FromMinutes(entity.Duration),
                Year = entity.Year,
                About = entity.About,
                Argument = entity.Argument,
                PosterImage = new ImageModel { Id = entity.PosterImage.Id, ContentType = entity.PosterImage.ContentType },
                PlacardImage = entity.PlacardImage == null ? null : new ImageModel { Id = entity.PlacardImage.Id, ContentType = entity.PlacardImage.ContentType },
                Genres = entity.ProductionGenreBridges.Select(bridge => new GenreModel { Id = bridge.Genre.Id, Name = bridge.Genre.Name }).ToArray(),
                Directors = entity.Participations.Where(item => item.Role == ProductionRole.Direction).Select(item => item.Personality.ToModel()).ToArray(),
                Interpreters = entity.Participations.Where(item => item.Role == ProductionRole.Interpretation).Select(item => item.Personality.ToModel()).ToArray(),
                AwardWinnings = entity.AwardWinnings.Select(item => new AwardWinningModel { Id = item.Id, Type = (int)item.Type, Edition = item.Edition, Category = item.Category, Award = new AwardModel { Id = item.Award.Id, Name = item.Award.Name } }).ToArray(),
                TrailerVideo = entity.TrailerVideo == null ? null : new VideoModel { Id = entity.TrailerVideo.Id, Extension = entity.TrailerVideo.Extension },
                Slug = Cinema.Web.Common.Providers.SlugHelperProvider.Instance.Get().GenerateSlug(entity.Title),
                Category = category            
            };

            if (entity.AudioVisual == null)
            {
                var avFolderPath = System.IO.Path.Combine(mediaStoreOptions.Value.AudioVisualsPath, category.StorageRelativePath, model.Slug);

                logger.LogInformation("XXXXXXXXXXXXXXXXXXXXX avFolderPath: {0}", avFolderPath);

                var fileNames = System.IO.Directory.EnumerateFiles(avFolderPath, string.Format("{0}.*", model.Slug));

                logger.LogInformation("XXXXXXXXXXXXXXXXXXXXX fileNames: {0}", string.Join(", ", fileNames));

                var avFileName = fileNames.First(item => mediaStoreOptions.Value.SupportVideoFileExtensions.Any(ext => item.EndsWith(ext, true, System.Globalization.CultureInfo.InvariantCulture)));

                var subtitleFileName = fileNames.FirstOrDefault(item => mediaStoreOptions.Value.SupportSubtitleFileExtensions.Any(ext => item.EndsWith(ext, true, System.Globalization.CultureInfo.InvariantCulture)));

                var avFileInfo = new System.IO.FileInfo(avFileName);

                var getMetadataTask = new MediaToolkit.Tasks.FfTaskGetMetadata(avFileName);

                var getMetadataResult = await mediaToolkitService.ExecuteAsync(getMetadataTask);

                var videoMediaStream = getMetadataResult.Metadata.Streams.First(stream => stream.CodecType == "video");

                entity.AudioVisual = new ProductionAudioVisual
                {
                    ProductionId = entity.Id,
                    Production = entity,
                    HasSubtitle = subtitleFileName != null,
                    FileSize = avFileInfo.Length,
                    Format = avFileInfo.Extension.Substring(1).ToUpper(),
                    Resolution = string.Format("{0}x{1}", videoMediaStream.Width, videoMediaStream.Height),
                    Quality = AudioVisualQuality.LD
                };

                if (videoMediaStream.Height >= 360)
                {
                    entity.AudioVisual.Quality = AudioVisualQuality.SD;
                }

                if (videoMediaStream.Width >= 1280 || videoMediaStream.Height >= 720)
                {
                    entity.AudioVisual.Quality = AudioVisualQuality.HD;
                }

                if (videoMediaStream.Width >= 1920 || videoMediaStream.Height >= 1080)
                {
                    entity.AudioVisual.Quality = AudioVisualQuality.FHD;
                }

                if (videoMediaStream.Height >= 2160)
                {
                    entity.AudioVisual.Quality = AudioVisualQuality.UHD;
                }

                if (videoMediaStream.Height >= 4320)
                {
                    entity.AudioVisual.Quality = AudioVisualQuality.K8;
                }
            }

            model.HasSubtitle = entity.AudioVisual.HasSubtitle;
            model.FileSize = entity.AudioVisual.FileSize;
            model.Format = entity.AudioVisual.Format;
            model.Resolution = entity.AudioVisual.Resolution;
            model.Quality = entity.AudioVisual.Quality;

            return model;
        }

        private CategoryModel CategoryIdToModel(System.Guid id)
        {
            var entity = categoryRepository.Get(id);

            return new CategoryModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Parent = entity.ParentId == null ? null : CategoryIdToModel(entity.ParentId.Value)
            };
        }

        private string CategoryToStorageRelativePath(CategoryModel model)
        {
            if (model.Parent == null)
            {
                return Cinema.Web.Common.Providers.SlugHelperProvider.Instance.Get().GenerateSlug(model.Name);
            }
            else
            {
                return string.Format("{0}\\{1}", CategoryToStorageRelativePath(model.Parent), Cinema.Web.Common.Providers.SlugHelperProvider.Instance.Get().GenerateSlug(model.Name));
            }
        }

        private System.Collections.Generic.List<System.Guid> GetCategoryDepthIds(Category entity)
        {
            var result = new System.Collections.Generic.List<System.Guid>();

            if (entity.Children == null || entity.Children.Count <= 0)
            {
                return result;
            }

            foreach (var item in entity.Children)
            {
                result.Add(item.Id);

                var child = categoryRepository.Get(item.Id);

                result.AddRange(GetCategoryDepthIds(child));
            }

            return result;
        }
    }
}
