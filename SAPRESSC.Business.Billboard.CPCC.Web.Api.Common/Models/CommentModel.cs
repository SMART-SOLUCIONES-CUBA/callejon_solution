namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models
{
    public abstract class CommentModel
    {
        public System.Guid Id { get; set; }

        public System.DateTime Created { get; set; }

        public string Message { get; set; }

        public ClientModel Client { get; set; }
    }
}
