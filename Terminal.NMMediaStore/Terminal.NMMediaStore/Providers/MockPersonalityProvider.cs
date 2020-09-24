using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class MockPersonalityProvider : Facade.IPersonalityProviderFacade
    {
        public async System.Threading.Tasks.Task<PaginationModel<PersonalityModel>> GetPaginateAsync(int page, int limit)
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            return new PaginationModel<PersonalityModel>
            {
                Page = page,
                Limit = limit,
                HasNext = true,
                HasPrevious = false,
                MaxPageCount = 1,
                List = new System.Collections.Generic.List<PersonalityModel>(BuildMockList())
            };
        }

        public async System.Threading.Tasks.Task<PersonalityModel[]> GetRelevantActorsAsync()
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            return BuildMockList();
        }

        public async System.Threading.Tasks.Task<PersonalityModel[]> GetRelevantDirectorsAsync()
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            return BuildMockList();
        }

        private PersonalityModel[] BuildMockList()
        {
            return new PersonalityModel[]
            {
                new PersonalityModel
                {
                    Id = 1,
                    Name = "Miguel Angel",
                    LastName = "Torres Pérez",
                    Image = new ImageModel
                    {
                        Id = new System.Guid("485A1093-45B0-49B5-97FA-227ED0435A6A"),
                        ContentType = "image/png"
                    }
                },
                new PersonalityModel
                {
                    Id = 1,
                    Name = "Miguel Angel",
                    LastName = "Torres Pérez",
                    Image = new ImageModel
                    {
                        Id = new System.Guid("485A1093-45B0-49B5-97FA-227ED0435A6A"),
                        ContentType = "image/png"
                    }
                },
                new PersonalityModel
                {
                    Id = 1,
                    Name = "Miguel Angel",
                    LastName = "Torres Pérez",
                    Image = new ImageModel
                    {
                        Id = new System.Guid("485A1093-45B0-49B5-97FA-227ED0435A6A"),
                        ContentType = "image/png"
                    }
                },
                new PersonalityModel
                {
                    Id = 1,
                    Name = "Miguel Angel",
                    LastName = "Torres Pérez",
                    Image = new ImageModel
                    {
                        Id = new System.Guid("485A1093-45B0-49B5-97FA-227ED0435A6A"),
                        ContentType = "image/png"
                    }
                },
                new PersonalityModel
                {
                    Id = 1,
                    Name = "Miguel Angel",
                    LastName = "Torres Pérez",
                    Image = new ImageModel
                    {
                        Id = new System.Guid("485A1093-45B0-49B5-97FA-227ED0435A6A"),
                        ContentType = "image/png"
                    }
                },
                new PersonalityModel
                {
                    Id = 1,
                    Name = "Miguel Angel",
                    LastName = "Torres Pérez",
                    Image = new ImageModel
                    {
                        Id = new System.Guid("485A1093-45B0-49B5-97FA-227ED0435A6A"),
                        ContentType = "image/png"
                    }
                },
                new PersonalityModel
                {
                    Id = 1,
                    Name = "Miguel Angel",
                    LastName = "Torres Pérez",
                    Image = new ImageModel
                    {
                        Id = new System.Guid("485A1093-45B0-49B5-97FA-227ED0435A6A"),
                        ContentType = "image/png"
                    }
                },
                new PersonalityModel
                {
                    Id = 1,
                    Name = "Miguel Angel",
                    LastName = "Torres Pérez",
                    Image = new ImageModel
                    {
                        Id = new System.Guid("485A1093-45B0-49B5-97FA-227ED0435A6A"),
                        ContentType = "image/png"
                    }
                }
            };
        }
    }
}
