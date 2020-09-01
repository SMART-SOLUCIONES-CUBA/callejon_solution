using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models
{
    public class CommentInputModel
    {
        [Required]
        public System.Guid Id { get; set; }

        [Required, StringLength(255)]
        public string Message { get; set; }

        [Required]
        public System.Guid ClientId { get; set; }

        public System.DateTime Created { get; set; }

        [Required]
        public System.Guid ReferenceId { get; set; }
    }
}
