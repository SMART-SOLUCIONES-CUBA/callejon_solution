using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models
{
    public class SubscriptionModel
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        public string About { get; set; }

        public bool PasswordChangeRequired { get; set; }

        public System.Guid ImageId { get; set; }

        public System.DateTime ServiceAvailabilityFrom { get; set; }

        public System.DateTime ServiceAvailabilityTo { get; set; }

        public System.DateTime? LastSignInAt { get; set; }

        public ProducerHouseModel[] PreferedProducerHouses { get; set; }

        public GenreModel[] PreferedGenres { get; set; }

        public TagModel[] PreferedTags { get; set; }
    }
}
