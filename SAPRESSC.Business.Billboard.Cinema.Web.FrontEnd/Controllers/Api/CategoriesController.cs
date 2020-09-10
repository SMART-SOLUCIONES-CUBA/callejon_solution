using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("mediastore")]
    public class CategoriesController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly Core.Persistence.Repositories.IEntityRepository<Persistence.Entities.Category, System.Guid> repository;

        public CategoriesController(Core.Persistence.Repositories.IEntityRepository<Persistence.Entities.Category, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<CategoriesController> localizer, ILogger<CategoriesController> logger) : base(localizer, logger)
        {
            this.repository = repository;
        }

        [HttpGet("children")]
        [HttpGet("children/{key}")]
        public virtual IActionResult GetChildren(System.Guid key = default)
        {
            if (key == System.Guid.Empty)
            {
                return Ok(repository.GetAll().Select(entity => new Web.Api.Common.Models.CategoryModel
                {
                    Id = entity.Id,
                    Name = entity.Name
                }).ToArray());
            }

            return Ok(repository.FindBy(entity => entity.ParentId == key).Select(entity => new Web.Api.Common.Models.CategoryModel
            {
                Id = entity.Id,
                Name = entity.Name
            }).ToArray());
        }
    }
}
