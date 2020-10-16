namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models
{
    public class ProducerHouseModel
    {
        public long Id { get; set; }
        
        public string Name { get; set; }

        public ImageModel Image { get; set; }
        
        public System.DateTime BornDate { get; set; }
        
        public string About { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
