namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class MockTagProvider : Facade.ITagProviderFacade
    {
        public async System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.TagModel[]> GetAllAsync()
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            return new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.TagModel[]
            {
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.TagModel
                {
                    Id = 1,
                    Name = "Etiqueta 1"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.TagModel
                {
                    Id = 2,
                    Name = "Etiqueta 2"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.TagModel
                {
                    Id = 3,
                    Name = "Etiqueta 3"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.TagModel
                {
                    Id = 4,
                    Name = "Etiqueta 4"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.TagModel
                {
                    Id = 5,
                    Name = "Etiqueta 5"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.TagModel
                {
                    Id = 6,
                    Name = "Etiqueta 6"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.TagModel
                {
                    Id = 7,
                    Name = "Etiqueta 7"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.TagModel
                {
                    Id = 8,
                    Name = "Etiqueta 8"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.TagModel
                {
                    Id = 9,
                    Name = "Etiqueta 9"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.TagModel
                {
                    Id = 10,
                    Name = "Etiqueta 10"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.TagModel
                {
                    Id = 11,
                    Name = "Etiqueta 11"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.TagModel
                {
                    Id = 12,
                    Name = "Etiqueta 12"
                }
            };
        }
    }
}
