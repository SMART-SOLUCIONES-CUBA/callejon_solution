using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models
{
    public class CommentDelationInputModel
    {
        public CommentDelationInputModel()
        {
            Revoked = false;
        }

        [Required]
        public System.Guid Id { get; set; }

        public System.DateTime Created { get; set; }

        [Required, StringLength(255)]
        public string Note { get; set; }

        [Required]
        public System.Guid ClientId { get; set; }

        public bool Revoked { get; set; }
    }
}
