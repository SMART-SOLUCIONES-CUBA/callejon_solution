namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.Page
{
    public class GenresPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel[] items;

        public SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel[] Items { get => items; set => SetProperty(ref items, value); }

        protected override void DisposeUnmanagementResources()
        {
            base.DisposeUnmanagementResources();

            Items = null;
        }
    }
}