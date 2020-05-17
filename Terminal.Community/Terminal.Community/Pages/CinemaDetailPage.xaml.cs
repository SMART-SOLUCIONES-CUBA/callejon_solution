namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class CinemaDetailPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        public CinemaDetailPage(Models.View.CinemaViewModel model)
        {
            InitializeComponent();

            BindingContext = model;
        }

        private void RoomActionButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            var targetRoom = (sender as Xamarin.Forms.BindableObject).BindingContext as Models.View.RoomViewModel;

            Navigation.PushAsync(new RoomBillboardPage(targetRoom));
        }

        private void PhoneContactActionButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            var targetRoom = (sender as Xamarin.Forms.BindableObject).BindingContext as Models.View.PhoneContactViewModel;

            Xamarin.Essentials.Launcher.OpenAsync(new System.Uri(string.Format("tel://{0}", targetRoom.PhoneNumber)));
        }

        private void EmailContactActionButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            var targetRoom = (sender as Xamarin.Forms.BindableObject).BindingContext as Models.View.EmailContactViewModel;

            Xamarin.Essentials.Launcher.OpenAsync(new System.Uri(string.Format("mailto://{0}", targetRoom.EmailAddress)));
        }

        private void LocationActionButton_Clicked(object sender, System.EventArgs e)
        {
            var model = BindingContext as Models.View.CinemaViewModel;

            var name = model.Name.Replace("&", "and"); // var name = Uri.EscapeUriString(place.Name);
            var loc = string.Format("{0},{1}", model.LocalizationLatitude.ToString(System.Globalization.CultureInfo.InvariantCulture), model.LocalizationLongitude.ToString(System.Globalization.CultureInfo.InvariantCulture));
            var addr = System.Uri.EscapeUriString(model.Address);

            var launchMapUri = new System.Uri(string.Format("bingmaps:?cp={0}&q={1}", loc, name));

            switch (Xamarin.Forms.Device.RuntimePlatform)
            {
                case Xamarin.Forms.Device.iOS:
                case Xamarin.Forms.Device.macOS:
                    launchMapUri = new System.Uri(string.Format("http://maps.apple.com/maps?q={0}&sll={1}", name.Replace(' ', '+'), loc));

                    break;
                case Xamarin.Forms.Device.Android:
                    launchMapUri = new System.Uri(string.Format("geo:{0}?q={1}({2})", loc, string.IsNullOrWhiteSpace(addr) ? loc : addr, name));

                    break;
            }

            Xamarin.Essentials.Launcher.OpenAsync(launchMapUri);
        }
    }
}