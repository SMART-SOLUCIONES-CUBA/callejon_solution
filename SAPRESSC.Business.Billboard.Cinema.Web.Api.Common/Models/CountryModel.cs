namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models
{
    public class CountryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IsoCode { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
