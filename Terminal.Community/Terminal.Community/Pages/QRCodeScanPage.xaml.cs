using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class QRCodeScanPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        private System.Action<string> qrCodeScanSuccessCallbackAction;

        public QRCodeScanPage(System.Action<string> qrCodeScanSuccessCallbackAction)
        {
            InitializeComponent();

            this.qrCodeScanSuccessCallbackAction = qrCodeScanSuccessCallbackAction;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ScannerView.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ScannerView.IsScanning = false;
        }

        protected override bool OnBackButtonPressed()
        {
            qrCodeScanSuccessCallbackAction = null;

            return base.OnBackButtonPressed();
        }

        private void CloseButton_Clicked(object sender, System.EventArgs args)
        {
            this.NavigationPopModalPageAsync();

            qrCodeScanSuccessCallbackAction = null;
        }

        protected void ScannerView_OnScanResult(ZXing.Result result)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {
                if (result.BarcodeFormat != ZXing.BarcodeFormat.QR_CODE) return;

                // Stop analysis until we navigate away so we don't keep reading barcodes
                ScannerView.IsScanning = false;

                await this.NavigationPopModalPageAsync();

                qrCodeScanSuccessCallbackAction?.Invoke(result.Text);

                qrCodeScanSuccessCallbackAction = null;
            });
        }
    }
}
