using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("mediastore")]
    public class AdvertisementsController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly Core.Persistence.Repositories.IEntityRepository<Persistence.Entities.Advertisement, System.Guid> repository;

        public AdvertisementsController(Core.Persistence.Repositories.IEntityRepository<Persistence.Entities.Advertisement, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<AdvertisementsController> localizer, ILogger<AdvertisementsController> logger) : base(localizer, logger)
        {
            this.repository = repository;
        }

        [HttpGet]
        public virtual IActionResult Get()
        {
            return Ok(repository.GetAll().Select(entity => new Web.Api.Common.Models.AdvertisementModel
            {
                Id = entity.Id,
                Title = entity.Title,
                About = entity.About,
                Action = entity.Action,
                PlacardImage = new Web.Api.Common.Models.ImageModel { Id = entity.PlacardImage.Id, ContentType = entity.PlacardImage.ContentType }
            }).ToArray());
        }
    }
}
