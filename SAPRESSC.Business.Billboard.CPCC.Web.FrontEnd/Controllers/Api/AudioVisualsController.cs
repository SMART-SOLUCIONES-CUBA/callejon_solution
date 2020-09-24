using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Extensions;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("mediastore")]
    public class AudioVisualsController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly Microsoft.Extensions.Options.IOptions<Options.MediaStoreOptions> mediaStoreOptions;
        private readonly Microsoft.Extensions.Options.IOptions<Cinema.Web.Common.Options.FfmpegOptions> ffmpegOptions;
        private readonly IEntityRepository<Production, long> productionRepository;
        private readonly IEntityRepository<Search, long> searchRepository;
        private readonly IEntityRepository<Category, System.Guid> categoryRepository;
        private readonly IEntityRepository<Middleware.Region.Persistence.Entities.Country, int> countryRepository;
        private readonly IEntityRepository<ProducerHouse, long> producerHouseRepository;

        public AudioVisualsController(IEntityRepository<ProducerHouse, long> producerHouseRepository, IEntityRepository<Middleware.Region.Persistence.Entities.Country, int> countryRepository, IEntityRepository<Category, System.Guid> categoryRepository, IEntityRepository<Search, long> searchRepository, IEntityRepository<Production, long> productionRepository, Microsoft.Extensions.Options.IOptions<Cinema.Web.Common.Options.FfmpegOptions> ffmpegOptions, Microsoft.Extensions.Options.IOptions<Options.MediaStoreOptions> mediaStoreOptions, Microsoft.Extensions.Localization.IStringLocalizer<AudioVisualsController> localizer, ILogger<AudioVisualsController> logger) : base(localizer, logger)
        {
            this.mediaStoreOptions = mediaStoreOptions;
            this.ffmpegOptions = ffmpegOptions;
            this.productionRepository = productionRepository;
            this.searchRepository = searchRepository;
            this.categoryRepository = categoryRepository;
            this.countryRepository = countryRepository;
            this.producerHouseRepository = producerHouseRepository;
        }

        [HttpGet("populars")]
        [HttpGet("populars/sheet/{sheet}/limit/{limit}")]
        public virtual IActionResult GetPopulars(int sheet = 1, int limit = 20)
        {
            return BuildPaginationModelActionResult(sheet, limit, PopularOrdering, true);
        }

        [HttpGet("lasts")]
        [HttpGet("lasts/sheet/{sheet}/limit/{limit}")]
        public virtual IActionResult GetLasts(int sheet = 1, int limit = 20)
        {
            return BuildPaginationModelActionResult(sheet, limit, entity => entity.Id, true);
        }

        [HttpGet("recomendeds")]
        [HttpGet("recomendeds/sheet/{sheet}/limit/{limit}")]
        public virtual IActionResult GetRecomendeds(int sheet = 1, int limit = 20)
        {
            return BuildPaginationModelActionResult(sheet, limit, RecomendationOrdering, true);
        }

        [HttpPost("search/sheet/{sheet}")]
        [HttpPost("search/sheet/{sheet}/limit/{limit}")]
        public virtual IActionResult Searh(SearchModel search, int sheet = 1, int limit = 20)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = new Search
                    {
                        Id = search.Id,
                        SubscriptionId = search.SubscriptionProfile.Id,
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

                    searchRepository.Add(entity, User.Identity.Name);

                    return BuildPaginationModelActionResult(sheet, limit, e => e.Title, false, search);
                }
                catch (System.Exception e)
                {
                    logger.LogError(e.InnerException, e.Message);

                    return Conflict();
                }
            }

            return BadRequest();
        }

        protected IActionResult BuildPaginationModelActionResult<TOrderingKey>(int page, int limit, System.Func<Production, TOrderingKey> ordering, bool descending, SearchModel searchModel = null)
        {
            IQueryable<Production> selection = null;

            var filterSelector = searchModel == null ? null : SearchToSelector(searchModel);

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
                    entities = selection.OrderBy(ordering).Skip((page - 1) * limit).Take(limit).ToList();
                }
                else
                {
                    entities = selection.OrderByDescending(ordering).Skip((page - 1) * limit).Take(limit).ToList();
                }

                var engine = new MediaToolkit.Engine(ffmpegOptions.Value.Path);

                var models = entities.Select(entity => EntityToModel(entity, engine)).ToList();

                engine.Dispose();

                paginationModel = new PaginationModel<AudioVisualModel>
                {
                    Limit = limit,
                    Page = page,
                    HasPrevious = page > 1,
                    HasNext = page < maxPageCount,
                    MaxPageCount = maxPageCount,
                    List = models
                };
            }

            return Ok(paginationModel);
        }

        private System.Linq.Expressions.Expression<System.Func<Production, bool>> SearchToSelector(SearchModel model)
        {
            // TODO Implement expected behavior!!
            return entity => true;
        }

        private object PopularOrdering(Production entity)
        {
            // TODO Implement expected behavior!!
            return entity.Rate;
        }

        private object RecomendationOrdering(Production entity)
        {
            // TODO Implement expected behavior!!
            return entity.Rate;
        }

        private AudioVisualModel EntityToModel(Production entity, MediaToolkit.Engine engine)
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

            var avFolderPath = System.IO.Path.Combine(mediaStoreOptions.Value.AudioVisualsPath, category.StorageRelativePath, model.Slug);

            logger.LogDebug("XXXXXXXXXXXXXXXXXXXXX avFolderPath: {0}", avFolderPath);

            var fileNames = System.IO.Directory.EnumerateFiles(avFolderPath, string.Format("{0}.*", model.Slug));

            logger.LogDebug("XXXXXXXXXXXXXXXXXXXXX fileNames: {0}", string.Join(", ", fileNames));

            var avFileName = fileNames.First(item => 
                item.EndsWith(".mpg") || 
                item.EndsWith(".avi") || 
                item.EndsWith(".mp4") || 
                item.EndsWith(".mkv") || 
                item.EndsWith(".avi") || 
                item.EndsWith(".ts") || 
                item.EndsWith(".vod") || 
                item.EndsWith(".data"));

            var subtitleFileName = fileNames.FirstOrDefault(item => item.EndsWith(".srt") || item.EndsWith(".sub"));

            var avFileInfo = new System.IO.FileInfo(avFileName);

            model.HasSubtitle = subtitleFileName != null;
            model.FileSize = avFileInfo.Length;
            model.Format = avFileInfo.Extension.Substring(1);

            var avMediaFile = new MediaToolkit.Model.MediaFile(avFileInfo.FullName);
            
            engine.GetMetadata(avMediaFile);

            model.Resolution = string.IsNullOrEmpty(avMediaFile.Metadata.VideoData.FrameSize) ? "352x288" : avMediaFile.Metadata.VideoData.FrameSize;

            logger.LogDebug("XXXXXXXXXXXXXXXXXXXXX model.Resolution: {0}", model.Resolution);

            var resolutionParts = model.Resolution.Split('x').Select(s => int.Parse(s)).ToArray();

            var height = resolutionParts[1];

            model.Quality = Cinema.Web.Api.Common.Enumerations.AudioVisualQuality.LD;

            if (height >= 360)
            {
                model.Quality = Cinema.Web.Api.Common.Enumerations.AudioVisualQuality.SD;
            }

            if (height >= 720)
            {
                model.Quality = Cinema.Web.Api.Common.Enumerations.AudioVisualQuality.HD;
            }

            if (height >= 1080)
            {
                model.Quality = Cinema.Web.Api.Common.Enumerations.AudioVisualQuality.FHD;
            }

            if (height >= 2160)
            {
                model.Quality = Cinema.Web.Api.Common.Enumerations.AudioVisualQuality.UHD;
            }

            if (height >= 4320)
            {
                model.Quality = Cinema.Web.Api.Common.Enumerations.AudioVisualQuality.K8;
            }

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
                return string.Format("{0}\\{1}", CategoryToStorageRelativePath(model.Parent), model.Name);
            }
        }
    }
}
