using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class ProfileOverviewPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        public ProfileOverviewPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var currentProfile = Managers.ProfileManager.Instance.GetCurrentProfile();

            BindingContext = new Models.View.ProfileViewModel
            {
                Id = currentProfile.Id,
                FirstName = currentProfile.FirstName,
                LastName = currentProfile.LastName,
                Phone = currentProfile.Phone,
                Email = currentProfile.Email,
                IsMale = currentProfile.IsMale,
                BornDate = currentProfile.BornDate,
                About = currentProfile.About
            };
        }

        private void EditToolbarItem_Activated(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPushPageAsync(new ProfileEditPage());
        }
    }
}