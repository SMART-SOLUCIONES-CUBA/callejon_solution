namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.Api
{
    public class PersonalityApiModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public bool IsMale { get; set; }

        public System.DateTime BornDate { get; set; }

        public string About { get; set; }

        public ImageApiModel Image { get; set; }

        public string FullName
        {
            get => Name.Trim() + " " + LastName.Trim();
        }
    }
}
