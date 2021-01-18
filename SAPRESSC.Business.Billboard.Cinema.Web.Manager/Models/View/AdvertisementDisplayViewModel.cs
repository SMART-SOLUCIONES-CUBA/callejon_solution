namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class AdvertisementDisplayViewModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<Persistence.Entities.Advertisement, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Title { get; set; }

        public string About { get; set; }

        public System.Guid PlacardImageId { get; set; }

        public string Action { get; set; }

        public void Import(Persistence.Entities.Advertisement entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            About = entity.About;
            PlacardImageId = entity.PlacardImage?.Id ?? System.Guid.Empty;
            Action = entity.Action;
        }
    }
}
