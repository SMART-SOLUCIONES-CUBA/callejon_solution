namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers.Facade
{
    public interface ICategoryProviderFacade
    {
        System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel[]> GetAllAsync();

        System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel[]> GetByParentAsync(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel parent);
    }
}
