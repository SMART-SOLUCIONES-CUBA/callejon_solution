namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class MockProducerHouseProvider : Facade.IProducerHouseProviderFacade
    {
        public async System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProducerHouseModel[]> GetRelevantsAsync()
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            return BuildMockList();
        }

        public async System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProducerHouseModel[]> GetAllAsync()
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            return BuildMockList();
        }

        private SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProducerHouseModel[] BuildMockList()
        {
            return new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProducerHouseModel[]
            {
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProducerHouseModel
                {
                    Id = 1,
                    Name = "Instituto Cubano del Arte e Industria Cinematográficos",
                    BornDate = new System.DateTime(1962, 10, 23),
                    About = "",
                    Image = new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ImageModel
                    {
                        Id = new System.Guid("976FA809-8D9E-4BE5-BA39-E94411D2D6D2"),
                        ContentType = "image/jpeg"
                    }
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProducerHouseModel
                {
                    Id = 2,
                    Name = "CineSoft",
                    BornDate = new System.DateTime(1962, 10, 23),
                    About = "",
                    Image = new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ImageModel
                    {
                        Id = new System.Guid("5EAF7064-C478-4B63-80DD-B4F253AD820A"),
                        ContentType = "image/jpeg"
                    }
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProducerHouseModel
                {
                    Id = 3,
                    Name = "Walt Disney Pictures",
                    BornDate = new System.DateTime(1962, 10, 23),
                    About = "",
                    Image = new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ImageModel
                    {
                        Id = new System.Guid("7D2509E8-577A-4FA1-9ABE-05734ABE7EA0"),
                        ContentType = "image/jpeg"
                    }
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProducerHouseModel
                {
                    Id = 3,
                    Name = "MARVEL Studios",
                    BornDate = new System.DateTime(1962, 10, 23),
                    About = "",
                    Image = new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ImageModel
                    {
                        Id = new System.Guid("593B3C8C-285E-42CD-8B6B-3B678190E688"),
                        ContentType = "image/png"
                    }
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProducerHouseModel
                {
                    Id = 3,
                    Name = "UNIVERSAL",
                    BornDate = new System.DateTime(1962, 10, 23),
                    About = "",
                    Image = new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ImageModel
                    {
                        Id = new System.Guid("01F7F824-95FC-4ACC-8BC4-63E1DE4B5479"),
                        ContentType = "image/png"
                    }
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProducerHouseModel
                {
                    Id = 3,
                    Name = "Warner Bros. Pictures",
                    BornDate = new System.DateTime(1962, 10, 23),
                    About = "",
                    Image = new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ImageModel
                    {
                        Id = new System.Guid("F3299FD1-1F73-49DB-82C6-D8F48CBDB258"),
                        ContentType = "image/jpeg"
                    }
                }
            };
        }
    }
}
