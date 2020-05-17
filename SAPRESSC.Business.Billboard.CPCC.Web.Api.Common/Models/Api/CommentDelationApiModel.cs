namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.Api
{
    public class CommentDelationApiModel
    {
        public System.Guid Id { get; set; }

        public System.DateTime Created { get; set; }

        public string Note { get; set; }

        public System.Guid ClientId { get; set; }

        public System.Guid CommentId { get; set; }
    }
}
