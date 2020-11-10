namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.Page
{
    public class YearsPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private int[] items;

        public int[] Items { get => items; set => SetProperty(ref items, value); }

        protected override void DisposeUnmanagementResources()
        {
            base.DisposeUnmanagementResources();

            Items = null;
        }
    }
}
