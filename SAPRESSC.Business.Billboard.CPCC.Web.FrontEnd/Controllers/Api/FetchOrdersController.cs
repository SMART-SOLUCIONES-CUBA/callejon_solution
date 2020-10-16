using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("mediastore")]
    public class FetchOrdersController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly IEntityRepository<Cinema.Persistence.Entities.FetchOrder, long> fetchOrderRepository;
        private readonly IEntityRepository<Cinema.Persistence.Entities.Category, System.Guid> categoryRepository;

        public FetchOrdersController(IEntityRepository<Cinema.Persistence.Entities.Category, System.Guid> categoryRepository, IEntityRepository<Cinema.Persistence.Entities.FetchOrder, long> fetchOrderRepository, Microsoft.Extensions.Localization.IStringLocalizer<FetchOrdersController> localizer, ILogger<FetchOrdersController> logger) : base(localizer, logger)
        {
            this.fetchOrderRepository = fetchOrderRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpGet("{key}")]
        public IActionResult Get(long key)
        {
            // TODO Validate SubscriptorId and send collections !!

            var entity = fetchOrderRepository.Get(key);

            return Ok(new FetchOrderModel
            {
                Id = entity.Id,
                At = entity.At,
                SubscriptorId = entity.SubscriptorId,
                AudioVisuals = entity.ProductionBridges.Select(bridge =>
                {
                    var category = CategoryIdToModel(bridge.Production.CategoryId.Value);

                    category.StorageRelativePath = CategoryToStorageRelativePath(category);

                    return new AudioVisualModel
                    {
                        Id = bridge.ProductionId,
                        Title = bridge.Production.Title,
                        Category = category,
                        Slug = Cinema.Web.Common.Providers.SlugHelperProvider.Instance.Get().GenerateSlug(bridge.Production.Title),
                        PosterImage = new ImageModel { Id = bridge.Production.PosterImage.Id, ContentType = bridge.Production.PosterImage.ContentType, Length = bridge.Production.PosterImage.Length },
                        PlacardImage = bridge.Production.PlacardImage == null ? null : new ImageModel { Id = bridge.Production.PlacardImage.Id, ContentType = bridge.Production.PlacardImage.ContentType, Length = bridge.Production.PlacardImage.Length }
                    };
                }).ToList()
            });
        }

        [HttpGet("count/production/{key}")]
        public IActionResult GetCountByProductionId(long key)
        {
            return Ok(fetchOrderRepository.FindBy(entity => entity.ProductionBridges.Any(item => item.ProductionId == key)).LongCount());
        }

        [HttpPost]
        public IActionResult Create([FromBody] FetchOrderModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = new Cinema.Persistence.Entities.FetchOrder
                    {
                        SubscriptorId = model.SubscriptorId
                    };

                    entity.ProductionBridges.AddRange(model.AudioVisuals.Select(item => new Cinema.Persistence.Entities.FetchOrderProductionBridge
                    {
                        FetchOrder = entity,
                        ProductionId = item.Id
                    }));

                    var result = fetchOrderRepository.Add(entity, User.Identity.Name);

                    return Ok(result.Id);
                }
                catch (System.Exception e)
                {
                    logger.LogError(e.InnerException, e.Message);

                    return Conflict();
                }
            }

            return BadRequest();
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
    }
}
