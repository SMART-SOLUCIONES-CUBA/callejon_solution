using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.Api;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("cpccbillboard")]
    public class ClientsController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly IEntityRepository<Client, System.Guid> repository;
        private readonly IEntityRepository<Image, System.Guid> imageRepository;

        public ClientsController(IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<Client, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ClientsController> localizer, ILogger<ClientsController> logger) : base(localizer, logger)
        {
            this.repository = repository;
            this.imageRepository = imageRepository;
        }

        [HttpGet("{key}")]
        public IActionResult Get(System.Guid key)
        {
            try
            {
                var entity = repository.Get(key);

                var model = new ClientApiModel
                {
                    Id = entity.Id,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    IsMale = entity.Sex == Middleware.Identity.Persistence.Enumerations.Sex.Male,
                    BornDate = entity.BornDate,
                    PhoneNumber = entity.PhoneNumber,
                    EmailAddress = entity.EmailAddress,
                    About = entity.About,
                    ImageId = entity.Image == null ? System.Guid.Empty : entity.Image.Id
                };

                return Ok(model);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] ClientApiModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Client
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Sex = model.IsMale ? Middleware.Identity.Persistence.Enumerations.Sex.Male : Middleware.Identity.Persistence.Enumerations.Sex.Famale,
                    Image = model.ImageId == System.Guid.Empty ? null : imageRepository.Get(model.ImageId),
                    PhoneNumber = model.PhoneNumber,
                    EmailAddress = model.EmailAddress,
                    BornDate = model.BornDate,
                    About = model.About
                };

                repository.Add(entity, "api");

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("{key}")]
        public IActionResult Update(System.Guid key, [FromBody] ClientApiModel model)
        {
            try
            {
                var entity = repository.Get(key);

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Sex = model.IsMale ? Middleware.Identity.Persistence.Enumerations.Sex.Male : Middleware.Identity.Persistence.Enumerations.Sex.Famale;
                entity.Image = model.ImageId == System.Guid.Empty ? null : imageRepository.Get(model.ImageId);
                entity.PhoneNumber = model.PhoneNumber;
                entity.EmailAddress = model.EmailAddress;
                entity.BornDate = model.BornDate;
                entity.About = model.About;

                repository.Update(entity, "api");

                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
