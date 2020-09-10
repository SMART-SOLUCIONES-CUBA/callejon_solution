namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class MockCategoryProvider : Facade.ICategoryProviderFacade
    {
        public async System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel[]> GetByParentAsync(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel parent)
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            if (parent == null)
            {
                return new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel[]
                {
                    new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel
                    {
                        Id = System.Guid.NewGuid(),
                        Name = "Películas"
                    },
                    new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel
                    {
                        Id = System.Guid.NewGuid(),
                        Name = "Series"
                    },
                    new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel
                    {
                        Id = System.Guid.NewGuid(),
                        Name = "Video Clips"
                    },
                    new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel
                    {
                        Id = System.Guid.NewGuid(),
                        Name = "Cortos"
                    }
                };
            }

            return new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel[]
            {
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel
                {
                    Id = System.Guid.NewGuid(),
                    Name = "Cubanas"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel
                {
                    Id = System.Guid.NewGuid(),
                    Name = "Premiadas"
                }
            };
        }
    }
}
