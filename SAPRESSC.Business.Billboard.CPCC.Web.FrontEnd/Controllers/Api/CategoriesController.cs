using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("mediastore")]
    public class CategoriesController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly Core.Persistence.Repositories.IEntityRepository<Cinema.Persistence.Entities.Category, System.Guid> repository;

        public CategoriesController(Core.Persistence.Repositories.IEntityRepository<Cinema.Persistence.Entities.Category, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<CategoriesController> localizer, ILogger<CategoriesController> logger) : base(localizer, logger)
        {
            this.repository = repository;
        }

        [HttpGet]
        public virtual IActionResult Get()
        {
            // TODO Retire ordering expression on SAPRESSC build-in support released !!
            return Ok(repository.GetAll().OrderBy(repository.GetPagingOrderSelector()).ToArray().Select(entity => new Cinema.Web.Api.Common.Models.CategoryModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Parent = entity.ParentId == null ? null : CategoryIdToModel(entity.ParentId.Value)
            }).ToArray());
        }

        [HttpGet("children")]
        [HttpGet("children/{key}")]
        public virtual IActionResult GetChildren(System.Guid key = default)
        {
            if (key == System.Guid.Empty)
            {
                return Ok(repository.FindBy(entity => entity.ParentId == null).Select(entity => new Cinema.Web.Api.Common.Models.CategoryModel
                {
                    Id = entity.Id,
                    Name = entity.Name
                }).ToArray());
            }

            return Ok(repository.FindBy(entity => entity.ParentId == key).Select(entity => new Cinema.Web.Api.Common.Models.CategoryModel
            {
                Id = entity.Id,
                Name = entity.Name
            }).ToArray());
        }

        private Cinema.Web.Api.Common.Models.CategoryModel CategoryIdToModel(System.Guid id)
        {
            var entity = repository.Get(id);

            return new Cinema.Web.Api.Common.Models.CategoryModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Parent = entity.ParentId == null ? null : CategoryIdToModel(entity.ParentId.Value)
            };
        }
    }
}
