namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models
{
    public class EventCommentModel : CommentModel
    {
        public System.Guid EventId { get; set; }

        public System.Guid ReferenceId { get; set; }
    }
}
