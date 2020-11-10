namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.Page
{
    public class PickGenresPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private View.GenreChoiseViewModel[] choises;

        public View.GenreChoiseViewModel[] Choises { get => choises; set => SetProperty(ref choises, value); }

        protected override void DisposeUnmanagementResources()
        {
            base.DisposeUnmanagementResources();

            Choises = null;
        }
    }
}
