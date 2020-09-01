using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.Page
{
    public class LibraryPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private CategoryModel[] categories;
        private ProducerHouseModel[] relevantProducerHouses;
        private GenreModel[] relevantGenres;
        private int[] relevantYears;
        private PersonalityModel[] relevantPerformancePersonalities;
        private PersonalityModel[] relevantDirectionPersonalities;

        public CategoryModel[] Categories { get => categories; set => SetProperty(ref categories, value); }

        public ProducerHouseModel[] RelevantProducerHouses { get => relevantProducerHouses; set => SetProperty(ref relevantProducerHouses, value); }
        
        public GenreModel[] RelevantGenres { get => relevantGenres; set => SetProperty(ref relevantGenres, value); }

        public int[] RelevantYears { get => relevantYears; set => SetProperty(ref relevantYears, value); }

        public PersonalityModel[] RelevantPerformancePersonalities { get => relevantPerformancePersonalities; set => SetProperty(ref relevantPerformancePersonalities, value); }

        public PersonalityModel[] RelevantDirectionPersonalities { get => relevantDirectionPersonalities; set => SetProperty(ref relevantDirectionPersonalities, value); }
    }
}
