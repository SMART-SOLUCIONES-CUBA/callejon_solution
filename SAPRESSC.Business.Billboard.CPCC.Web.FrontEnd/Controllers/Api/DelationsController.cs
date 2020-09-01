using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("cpccbillboard")]
    public class DelationsController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly IEntityRepository<Client, System.Guid> clientRepository;
        private readonly IEntityRepository<EventComment, System.Guid> eventCommentsRepository;
        private readonly IEntityRepository<ProductionComment, System.Guid> productionCommentsRepository;
        private readonly IEntityRepository<PersonalityComment, System.Guid> personalityCommentsRepository;
        private readonly IEntityRepository<EventCommentDelation, System.Guid> eventCommentDelationRepository;
        private readonly IEntityRepository<ProductionCommentDelation, System.Guid> productionCommentDelationRepository;
        private readonly IEntityRepository<PersonalityCommentDelation, System.Guid> personalityCommentDelationRepository;

        public DelationsController(IEntityRepository<EventCommentDelation, System.Guid> eventCommentDelationRepository, IEntityRepository<ProductionCommentDelation, System.Guid> productionCommentDelationRepository, IEntityRepository<PersonalityCommentDelation, System.Guid> personalityCommentDelationRepository, IEntityRepository<Client, System.Guid> clientRepository, IEntityRepository<PersonalityComment, System.Guid> personalityCommentsRepository, IEntityRepository<ProductionComment, System.Guid> productionCommentsRepository, IEntityRepository<EventComment, System.Guid> eventCommentsRepository, Microsoft.Extensions.Localization.IStringLocalizer<DelationsController> localizer, ILogger<DelationsController> logger) : base(localizer, logger)
        {
            this.clientRepository = clientRepository;
            this.eventCommentsRepository = eventCommentsRepository;
            this.productionCommentsRepository = productionCommentsRepository;
            this.personalityCommentsRepository = personalityCommentsRepository;
            this.eventCommentDelationRepository = eventCommentDelationRepository;
            this.productionCommentDelationRepository = productionCommentDelationRepository;
            this.personalityCommentDelationRepository = personalityCommentDelationRepository;
        }

        [HttpGet("eventcomment/client/{clientId}")]
        public CommentDelationModel[] GetAllClientEventCommentDelations(System.Guid clientId)
        {
            return eventCommentDelationRepository.FindBy(entity => entity.Client.Id == clientId && !entity.Revoked).Select(entity => new CommentDelationModel
            {
                Id = entity.Id,
                Created = entity.Created,
                Note = entity.Note,
                ClientId = entity.Client.Id,
                CommentId = entity.Comment.Id
            }).ToArray();
        }



        [HttpGet("productioncomment/client/{clientId}")]
        public CommentDelationModel[] GetAllClientProductionCommentDelations(System.Guid clientId)
        {
            return productionCommentDelationRepository.FindBy(entity => entity.Client.Id == clientId && !entity.Revoked).Select(entity => new CommentDelationModel
            {
                Id = entity.Id,
                Created = entity.Created,
                Note = entity.Note,
                ClientId = entity.Client.Id,
                CommentId = entity.Comment.Id
            }).ToArray();
        }



        [HttpGet("personalitycomment/client/{clientId}")]
        public CommentDelationModel[] GetAllClientPerosnalityCommentDelations(System.Guid clientId)
        {
            return personalityCommentDelationRepository.FindBy(entity => entity.Client.Id == clientId && !entity.Revoked).Select(entity => new CommentDelationModel
            {
                Id = entity.Id,
                Created = entity.Created,
                Note = entity.Note,
                ClientId = entity.Client.Id,
                CommentId = entity.Comment.Id
            }).ToArray();
        }

        [HttpPut("eventcomment/{key}")]
        public IActionResult AddEventCommentDelation(System.Guid key, [FromBody] CommentDelationInputModel model)
        {
            try
            {
                var eventComment = eventCommentsRepository.Get(key);

                if (eventComment == null) throw new System.Exception();

                if (ModelState.IsValid)
                {
                    var commentDelation = eventCommentDelationRepository.FindOneBy(entity => entity.Id == model.Id);

                    if (commentDelation != null)
                    {
                        if (!commentDelation.Revoked && model.Revoked)
                        {
                            commentDelation.Revoked = true;

                            eventCommentDelationRepository.Update(commentDelation, "api");
                        }
                    }
                    else
                    {
                        commentDelation = new EventCommentDelation
                        {
                            Id = model.Id,
                            Created = model.Created,
                            Comment = eventComment,
                            Client = clientRepository.Get(model.ClientId),
                            Note = model.Note
                        };

                        eventCommentDelationRepository.Add(commentDelation, "api");
                    }

                    return Ok();
                }

                return BadRequest();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("personalitycomment/{key}")]
        public IActionResult AddPersonalityCommentDelation(System.Guid key, [FromBody] CommentDelationInputModel model)
        {
            try
            {
                var personalityComment = personalityCommentsRepository.Get(key);

                if (personalityComment == null) throw new System.Exception();

                if (ModelState.IsValid)
                {
                    var commentDelation = personalityCommentDelationRepository.FindOneBy(entity => entity.Id == model.Id);

                    if (commentDelation != null)
                    {
                        if (!commentDelation.Revoked && model.Revoked)
                        {
                            commentDelation.Revoked = true;

                            personalityCommentDelationRepository.Update(commentDelation, "api");
                        }
                    }
                    else
                    {
                        commentDelation = new PersonalityCommentDelation
                        {
                            Id = model.Id,
                            Created = model.Created,
                            Comment = personalityComment,
                            Client = clientRepository.Get(model.ClientId),
                            Note = model.Note
                        };

                        personalityCommentDelationRepository.Add(commentDelation, "api");
                    }

                    return Ok();
                }

                return BadRequest();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("productioncomment/{key}")]
        public IActionResult AddProductionCommentDelation(System.Guid key, [FromBody] CommentDelationInputModel model)
        {
            try
            {
                var productionComment = productionCommentsRepository.Get(key);

                if (productionComment == null) throw new System.Exception();

                if (ModelState.IsValid)
                {
                    var commentDelation = productionCommentDelationRepository.FindOneBy(entity => entity.Id == model.Id);

                    if (commentDelation != null)
                    {
                        if (!commentDelation.Revoked && model.Revoked)
                        {
                            commentDelation.Revoked = true;

                            productionCommentDelationRepository.Update(commentDelation, "api");
                        }
                    }
                    else
                    {
                        commentDelation = new ProductionCommentDelation
                        {
                            Id = model.Id,
                            Created = model.Created,
                            Comment = productionComment,
                            Client = clientRepository.Get(model.ClientId),
                            Note = model.Note
                        };

                        productionCommentDelationRepository.Add(commentDelation, "api");
                    }

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
