namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class MockAdvertisementProvider : Facade.IAdvertisementProviderFacade
    {
        public async System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AdvertisementModel[]> GetAllAsync()
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            return new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AdvertisementModel[]
            {
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AdvertisementModel
                {
                    Id = System.Guid.NewGuid(),
                    Title = "Titulo del Anuncio",
                    About = "Descripción del anuncio",
                    PlacardImage = new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ImageModel
                    {
                        Id = new System.Guid("8788B019-1210-49CD-967E-4E372AD86495"),
                        ContentType = "image/jpeg"
                    }
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AdvertisementModel
                {
                    Id = System.Guid.NewGuid(),
                    Title = "Titulo del Anuncio",
                    About = "Descripción del anuncio",
                    PlacardImage = new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ImageModel
                    {
                        Id = new System.Guid("8E0EF62A-4177-4456-BE66-869D395DEF5E"),
                        ContentType = "image/jpeg"
                    }
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AdvertisementModel
                {
                    Id = System.Guid.NewGuid(),
                    Title = "Titulo del Anuncio",
                    About = "Descripción del anuncio",
                    PlacardImage = new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ImageModel
                    {
                        Id = new System.Guid("8788B019-1210-49CD-967E-4E372AD86495"),
                        ContentType = "image/jpeg"
                    }
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AdvertisementModel
                {
                    Id = System.Guid.NewGuid(),
                    Title = "Titulo del Anuncio",
                    About = "Descripción del anuncio",
                    PlacardImage = new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ImageModel
                    {
                        Id = new System.Guid("8E0EF62A-4177-4456-BE66-869D395DEF5E"),
                        ContentType = "image/jpeg"
                    }
                }
            };
        }
    }
}
