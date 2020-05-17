using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.Api
{
    public class ClientApiModel
    {
        [Required]
        public System.Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public bool IsMale { get; set; }

        [RegularExpression("^5[0-9]{7}$")]
        public string PhoneNumber { get; set; }

        [RegularExpression("^.*@.*$")]
        public string EmailAddress { get; set; }

        public System.DateTime BornDate { get; set; }

        [StringLength(255)]
        public string About { get; set; }

        public System.Guid ImageId { get; set; }
    }
}
