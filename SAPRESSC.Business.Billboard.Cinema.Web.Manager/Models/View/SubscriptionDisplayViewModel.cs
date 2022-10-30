using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Common.Extensions;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class SubscriptionDisplayViewModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<Persistence.Entities.Subscription, string>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string About { get; set; }

        public System.Guid ImageId { get; set; }

        public string ImageExtension { get; set; }

        public bool PasswordChangeRequired { get; set; }

        public System.DateTime? LastSignInAt { get; set; }

        public int FailAuthenticationCount { get; set; }

        public bool Enabled { get; set; }

        public System.DateTime ServiceAvailabilityFrom { get; set; }

        public System.DateTime ServiceAvailabilityTo { get; set; }

        public void Import(Persistence.Entities.Subscription entity)
        {
            Id = entity.Id;
            Name = entity.Identity.Name;
            LastName = entity.Identity.LastName;
            About = entity.Identity.About;
            ImageId = entity.Identity.Image?.Id ?? System.Guid.Empty;
            ImageExtension = entity.Identity.Image?.ContentType.ContentTypeToExtension() ?? null;
            PasswordChangeRequired = entity.PasswordChangeRequired;
            LastSignInAt = entity.LastSignInAt;
            FailAuthenticationCount = entity.FailAuthenticationCount;
            Enabled = entity.Enabled;
            ServiceAvailabilityFrom = entity.ServiceAvailabilityFrom;
            ServiceAvailabilityTo = entity.ServiceAvailabilityTo;
        }
    }
}
