using SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.View;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.Page
{
    public class MasterPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private ProfileViewModel profile;

        public MasterPageModel()
        {
            NavigationSectionGroups = new System.Collections.Generic.List<View.NavigationSectionGroupViewModel>();
        }

        public System.Collections.Generic.List<View.NavigationSectionGroupViewModel> NavigationSectionGroups { get; set; }

        public ProfileViewModel Profile { get => profile; set => SetProperty(ref profile, value); }
    }
}
