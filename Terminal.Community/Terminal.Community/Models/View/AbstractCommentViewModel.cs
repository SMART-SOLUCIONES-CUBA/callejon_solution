namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.View
{
    public abstract class AbstractCommentViewModel
    {
        public System.Guid Id { get; set; }

        public string ClientName { get; set; }

        public Xamarin.Forms.ImageSource ClientImageSource { get; set;}

        public System.DateTime Created { get; set; }

        public string Message { get; set; }

        public AbstractCommentViewModel Reference { get; set; }

        public CommentDelationViewModel LastDelation { get; set; }
    }
}
