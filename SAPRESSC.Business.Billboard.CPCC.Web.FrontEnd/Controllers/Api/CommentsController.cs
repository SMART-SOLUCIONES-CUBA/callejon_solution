using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("cpccbillboard")]
    public class CommentsController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly IEntityRepository<Personality, long> personalityRepository;
        private readonly IEntityRepository<Production, long> productionRepository;
        private readonly IEntityRepository<Event, System.Guid> eventRepository;
        private readonly IEntityRepository<Client, System.Guid> clientRepository;
        private readonly IEntityRepository<EventComment, System.Guid> eventCommentsRepository;
        private readonly IEntityRepository<ProductionComment, System.Guid> productionCommentsRepository;
        private readonly IEntityRepository<PersonalityComment, System.Guid> personalityCommentsRepository;
        private readonly IEntityRepository<CommentClassifier, int> commentClassifierRepository;

        public CommentsController(IEntityRepository<CommentClassifier, int> commentClassifierRepository, IEntityRepository<Personality, long> personalityRepository, IEntityRepository<Production, long> productionRepository, IEntityRepository<Event, System.Guid> eventRepository, IEntityRepository<Client, System.Guid> clientRepository, IEntityRepository<PersonalityComment, System.Guid> personalityCommentsRepository, IEntityRepository<ProductionComment, System.Guid> productionCommentsRepository, IEntityRepository<EventComment, System.Guid> eventCommentsRepository, Microsoft.Extensions.Localization.IStringLocalizer<CommentsController> localizer, ILogger<CommentsController> logger) : base(localizer, logger)
        {
            this.personalityRepository = personalityRepository;
            this.productionRepository = productionRepository;
            this.eventRepository = eventRepository;
            this.clientRepository = clientRepository;
            this.eventCommentsRepository = eventCommentsRepository;
            this.productionCommentsRepository = productionCommentsRepository;
            this.personalityCommentsRepository = personalityCommentsRepository;
            this.commentClassifierRepository = commentClassifierRepository;
        }

        [HttpGet("event/{eventId}")]
        public EventCommentModel[] GetAllEventComments(System.Guid eventId)
        {
            var startEdgeDate = System.DateTime.Now;
            var passYearEdgeDate = startEdgeDate.AddMonths(-11);

            return eventCommentsRepository.FindBy(entity => !entity.Banish && entity.Syncronized && entity.Event.Id == eventId && entity.Created <= startEdgeDate && entity.Created >= passYearEdgeDate).OrderBy(entity => entity.Created).Select(entity => new EventCommentModel
            {
                Id = entity.Id,
                Created = entity.Created,
                Message = entity.Message,
                Client = new ClientModel
                {
                    Id = entity.Client.Id,
                    FirstName = entity.Client.FirstName,
                    LastName = entity.Client.LastName,
                    BornDate = entity.Client.BornDate,
                    IsMale = entity.Client.Sex == Middleware.Identity.Persistence.Enumerations.Sex.Male,
                    About = entity.Client.About,
                    EmailAddress = entity.Client.EmailAddress,
                    PhoneNumber = entity.Client.PhoneNumber,
                    ImageId = entity.Client.Image != null ? entity.Client.Image.Id : System.Guid.Empty
                },
                EventId = entity.Event.Id,
                ReferenceId = entity.Reference != null ? entity.Reference.Id : System.Guid.Empty
            }).ToArray();
        }

        [HttpPut("event/{key}")]
        public IActionResult AddEventComment(System.Guid key, [FromBody] CommentInputModel model)
        {
            try
            {
                var @event = eventRepository.Get(key);

                if (@event == null) throw new System.Exception();

                if (ModelState.IsValid)
                {
                    var comment = new EventComment
                    {
                        Id = model.Id,
                        Event = @event,
                        Client = clientRepository.Get(model.ClientId),
                        Message = model.Message,
                        Reference = model.ReferenceId == System.Guid.Empty ? null : eventCommentsRepository.Get(model.ReferenceId),
                        Created = model.Created,
                        Syncronized = true,
                        Banish = commentClassifierRepository.FindBy(entity => model.Message.Contains(entity.Phrase)).Any()
                    };

                    eventCommentsRepository.Add(comment, "api");

                    return Ok();
                }

                return BadRequest();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("production/{productionId}")]
        public ProductionCommentModel[] GetAllProductionComments(long productionId)
        {
            var startEdgeDate = System.DateTime.Now;
            var passYearEdgeDate = startEdgeDate.AddMonths(-11);

            return productionCommentsRepository.FindBy(entity => !entity.Banish && entity.Syncronized && entity.Production.Id == productionId && entity.Created <= startEdgeDate && entity.Created >= passYearEdgeDate).OrderBy(entity => entity.Created).Select(entity => new ProductionCommentModel
            {
                Id = entity.Id,
                Created = entity.Created,
                Message = entity.Message,
                Client = new ClientModel
                {
                    Id = entity.Client.Id,
                    FirstName = entity.Client.FirstName,
                    LastName = entity.Client.LastName,
                    BornDate = entity.Client.BornDate,
                    IsMale = entity.Client.Sex == Middleware.Identity.Persistence.Enumerations.Sex.Male,
                    About = entity.Client.About,
                    EmailAddress = entity.Client.EmailAddress,
                    PhoneNumber = entity.Client.PhoneNumber,
                    ImageId = entity.Client.Image != null ? entity.Client.Image.Id : System.Guid.Empty
                },
                ProductionId = entity.Production.Id,
                ReferenceId = entity.Reference != null ? entity.Reference.Id : System.Guid.Empty
            }).ToArray();
        }

        [HttpPut("production/{key}")]
        public IActionResult AddProductionComment(long key, [FromBody] CommentInputModel model)
        {
            try
            {
                var production = productionRepository.Get(key);

                if (production == null) throw new System.Exception();

                if (ModelState.IsValid)
                {
                    var comment = new ProductionComment
                    {
                        Id = model.Id,
                        Production = production,
                        Client = clientRepository.Get(model.ClientId),
                        Message = model.Message,
                        Reference = model.ReferenceId == System.Guid.Empty ? null : productionCommentsRepository.Get(model.ReferenceId),
                        Created = model.Created,
                        Syncronized = true,
                        Banish = commentClassifierRepository.FindBy(entity => model.Message.Contains(entity.Phrase)).Any()
                    };

                    productionCommentsRepository.Add(comment, "api");

                    return Ok();
                }

                return BadRequest();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("personality/{personalityId}")]
        public PersonalityCommentModel[] GetAllPersonalityComments(long personalityId)
        {
            var startEdgeDate = System.DateTime.Now;
            var passYearEdgeDate = startEdgeDate.AddMonths(-11);

            return personalityCommentsRepository.FindBy(entity => !entity.Banish && entity.Syncronized && entity.Personality.Id == personalityId && entity.Created <= startEdgeDate && entity.Created >= passYearEdgeDate).OrderBy(entity => entity.Created).Select(entity => new PersonalityCommentModel
            {
                Id = entity.Id,
                Created = entity.Created,
                Message = entity.Message,
                Client = new ClientModel
                {
                    Id = entity.Client.Id,
                    FirstName = entity.Client.FirstName,
                    LastName = entity.Client.LastName,
                    BornDate = entity.Client.BornDate,
                    IsMale = entity.Client.Sex == Middleware.Identity.Persistence.Enumerations.Sex.Male,
                    About = entity.Client.About,
                    EmailAddress = entity.Client.EmailAddress,
                    PhoneNumber = entity.Client.PhoneNumber,
                    ImageId = entity.Client.Image != null ? entity.Client.Image.Id : System.Guid.Empty
                },
                PersonalityId = entity.Personality.Id,
                ReferenceId = entity.Reference != null ? entity.Reference.Id : System.Guid.Empty
            }).ToArray();
        }

        [HttpPut("personality/{key}")]
        public IActionResult AddPersonalityComment(long key, [FromBody] CommentInputModel model)
        {
            try
            {
                var personality = personalityRepository.Get(key);

                if (personality == null) throw new System.Exception();

                if (ModelState.IsValid)
                {
                    var comment = new PersonalityComment
                    {
                        Id = model.Id,
                        Personality = personality,
                        Client = clientRepository.Get(model.ClientId),
                        Message = model.Message,
                        Reference = model.ReferenceId == System.Guid.Empty ? null : personalityCommentsRepository.Get(model.ReferenceId),
                        Created = model.Created,
                        Syncronized = true,
                        Banish = commentClassifierRepository.FindBy(entity => model.Message.Contains(entity.Phrase)).Any()
                    };

                    personalityCommentsRepository.Add(comment, "api");

                    return Ok();
                }

                return BadRequest();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
