namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class ClientViewDisplayModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<Persistence.Entities.Client, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string FullName { get; set; }

        public bool IsMale { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public System.DateTime BornDate { get; set; }

        public string About { get; set; }

        public System.Guid ImageId { get; set; }

        public void Import(Persistence.Entities.Client entity)
        {
            Id = entity.Id;
            FullName = string.Format("{0} {1}", entity.FirstName.Trim(), entity.LastName.Trim());
            IsMale = entity.Sex == Middleware.Identity.Persistence.Enumerations.Sex.Male;
            PhoneNumber = entity.PhoneNumber;
            EmailAddress = entity.EmailAddress;
            BornDate = entity.BornDate;
            About = entity.About;
            ImageId = entity.Image != null ? entity.Image.Id : System.Guid.Empty;
        }
    }
}
