using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class ProducerHouse : Middleware.Identity.Persistence.Entities.Organization
    {
        public ICollection<ProducerHouseProductionBridge> ProducerHouseProductionBridges { get; set; }

        public ProducerHouse()
        {
            ProducerHouseProductionBridges = new List<ProducerHouseProductionBridge>();
        }
    }
}
