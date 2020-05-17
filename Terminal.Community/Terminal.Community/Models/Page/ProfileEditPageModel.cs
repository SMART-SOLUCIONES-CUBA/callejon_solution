namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.Page
{
    public class ProfileEditPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private View.ProfileViewModel profile;

        public View.ProfileViewModel Profile { get => profile; set => SetProperty(ref profile, value); }
    }
}
