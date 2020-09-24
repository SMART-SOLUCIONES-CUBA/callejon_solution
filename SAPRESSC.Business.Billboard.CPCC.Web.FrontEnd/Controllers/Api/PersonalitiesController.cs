using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("mediastore")]
    public class PersonalitiesController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly Core.Persistence.Repositories.IEntityRepository<Cinema.Persistence.Entities.Personality, long> repository;

        public PersonalitiesController(Core.Persistence.Repositories.IEntityRepository<Cinema.Persistence.Entities.Personality, long> repository, Microsoft.Extensions.Localization.IStringLocalizer<PersonalitiesController> localizer, ILogger<PersonalitiesController> logger) : base(localizer, logger)
        {
            this.repository = repository;
        }

        [HttpGet("relevant/actors")]
        [HttpGet("relevant/actors/{limit}")]
        public virtual IActionResult GetRelevantActors(int limit = 10)
        {
            return Ok(repository.GetAll().Select(entity => entity.ToModel()).Take(limit).ToArray());
        }

        [HttpGet("relevant/directors")]
        [HttpGet("relevant/actors/{limit}")]
        public virtual IActionResult GetRelevantDirectors(int limit = 10)
        {
            return Ok(repository.GetAll().Select(entity => entity.ToModel()).Take(limit).ToArray());
        }

        [HttpGet]
        [Route("page/{sheet}")]
        [Route("page/{sheet}/limit/{limit}")]
        public virtual IActionResult GetPaginate(int sheet = 1, int limit = 25)
        {
            var selection = repository.GetAll();

            var maxCount = selection.Count();

            var paginationModel = new Cinema.Web.Api.Common.Models.PaginationModel<Cinema.Web.Api.Common.Models.PersonalityModel>
            {
                Limit = limit,
                Page = 0,
                HasNext = false,
                HasPrevious = false,
                List = new System.Collections.Generic.List<Cinema.Web.Api.Common.Models.PersonalityModel>()
            };

            if (maxCount > 0)
            {
                var rest = maxCount % limit;

                var maxPageCount = maxCount / limit;

                if (rest > 0)
                    maxPageCount += 1;

                if (sheet < 0 || sheet > maxPageCount)
                {
                    sheet = 1;
                }

                var entities = new System.Collections.Generic.List<Cinema.Persistence.Entities.Personality>();

                if (!repository.IsPagingOrderDescending())
                {
                    entities = selection.Skip((sheet - 1) * limit).Take(limit).ToList();
                }
                else
                {
                    entities = selection.Skip((sheet - 1) * limit).Take(limit).ToList();
                }

                var models = entities.Select(entity => entity.ToModel()).ToList();

                paginationModel = new Cinema.Web.Api.Common.Models.PaginationModel<Cinema.Web.Api.Common.Models.PersonalityModel>
                {
                    Limit = limit,
                    Page = sheet,
                    HasPrevious = sheet > 1,
                    HasNext = sheet < maxPageCount,
                    MaxPageCount = maxPageCount,
                    List = models
                };
            }

            return Ok(paginationModel);
        }
    }
}
