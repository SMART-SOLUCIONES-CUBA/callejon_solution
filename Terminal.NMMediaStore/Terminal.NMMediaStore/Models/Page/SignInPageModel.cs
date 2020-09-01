namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.Page
{
    public class SignInPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private string subscriptorId;
        private string password;

        public string SubscriptorId { get => subscriptorId; set => SetProperty(ref subscriptorId, value); }

        public string Password { get => password; set => SetProperty(ref password, value); }
    }
}