namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.Api
{
    public abstract class CommentApiModel
    {
        public System.Guid Id { get; set; }

        public System.DateTime Created { get; set; }

        public string Message { get; set; }

        public ClientApiModel Client { get; set; }
    }
}
