namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.Page
{
    public class SessionConfigurationPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        public SessionConfigurationPageModel()
        {
            Profile = new View.ProfileViewModel();
        }

        public View.ProfileViewModel Profile { get; private set; }
    }
}
