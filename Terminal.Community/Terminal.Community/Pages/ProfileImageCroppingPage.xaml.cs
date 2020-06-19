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

            CropCanvasWrapper.Children.Add(imageCropperCanvasView);
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

        private void RotateToLeftButton_Clicked(object sender, System.EventArgs args)
        {
            imageCropperCanvasView.RotateToLeft();
        }

        private void RotateToRightButton_Clicked(object sender, System.EventArgs args)
        {
            imageCropperCanvasView.RotateToRight();
        }

        protected override void DisposeUnmanagementResources()
        {
            base.DisposeUnmanagementResources();

            imageCropperCanvasView = null;
        }
    }
}
