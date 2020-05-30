namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    public partial class ProfileImageCroppingPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        private System.Action<SkiaSharp.SKBitmap> callbackAction;

        private Views.Specialized.ImageCropperCanvasView imageCropperCanvasView;

        public ProfileImageCroppingPage(SkiaSharp.SKBitmap target, System.Action<SkiaSharp.SKBitmap> callbackAction)
        {
            InitializeComponent();

            this.callbackAction = callbackAction;

            imageCropperCanvasView = new Views.Specialized.ImageCropperCanvasView(target, 1f);

            Content = new Xamarin.Forms.Grid();

            (Content as Xamarin.Forms.Grid).Children.Add(imageCropperCanvasView);
        }

        private void ApplyToolbarItem_Activated(object sender, System.EventArgs args)
        {
            callbackAction.Invoke(imageCropperCanvasView.CroppedBitmap);

            callbackAction = null;
        }

        private void CancelToolbarItem_Activated(object sender, System.EventArgs args)
        {
            callbackAction.Invoke(null);

            callbackAction = null;
        }
    }
}
