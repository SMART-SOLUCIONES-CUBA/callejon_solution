namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.Page
{
    public class ProducerHousesPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProducerHouseModel[] items;

        public SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProducerHouseModel[] Items { get => items; set => SetProperty(ref items, value); }

        protected override void DisposeUnmanagementResources()
        {
            base.DisposeUnmanagementResources();

            Items = null;
        }
    }
}
