using Microsoft.AspNetCore.Mvc;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Components
{
    public class PersonalityRelationshipsDetail : ViewComponent
    {
        private readonly IEntityRepository<Event, System.Guid> eventRepository;
        private readonly IEntityRepository<Production, long> productionRepository;

        public PersonalityRelationshipsDetail(IEntityRepository<Event, System.Guid> eventRepository, IEntityRepository<Production, long> productionRepository)
        {
            this.eventRepository = eventRepository;
            this.productionRepository = productionRepository;
        }

        public IViewComponentResult Invoke(long key)
        {
            var productionMemberships = productionRepository.GetAll().Select(entity => entity.Participations.Where(bridge => bridge.Personality.Id == key)).ToArray();
            var eventMemberships = eventRepository.GetAll().Select(entity => entity.EventPersonalityBridges.Where(bridge => bridge.PersonalityId == key)).ToArray();

            var model = new Models.View.PersonalityRelationshipsDetailViewModel
            {
                ProductionRelationshipsCount = productionMemberships.LongCount(),
                EventRelationshipsCount = eventMemberships.LongCount()
            };

            return View(model);
        }
    }
}
