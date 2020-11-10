using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.Page
{
    public class LibraryPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private CategoryModel[] categories;
        private ProducerHouseModel[] relevantProducerHouses;
        private bool hasMoreProducerHouses;
        private GenreModel[] relevantGenres;
        private bool hasMoreGenres;
        private int[] relevantYears;
        private bool hasMoreYears;
        private PersonalityModel[] relevantPerformancePersonalities;
        private bool hasMorePerformancePersonalities;
        private PersonalityModel[] relevantDirectionPersonalities;
        private bool hasMoreDirectionPersonalities;

        public CategoryModel[] Categories { get => categories; set => SetProperty(ref categories, value); }

        public ProducerHouseModel[] RelevantProducerHouses { get => relevantProducerHouses; set => SetProperty(ref relevantProducerHouses, value); }

        public bool HasMoreProducerHouses { get => hasMoreProducerHouses; set => SetProperty(ref hasMoreProducerHouses, value); }

        public GenreModel[] RelevantGenres { get => relevantGenres; set => SetProperty(ref relevantGenres, value); }

        public bool HasMoreGenres { get => hasMoreGenres; set => SetProperty(ref hasMoreGenres, value); }

        public int[] RelevantYears { get => relevantYears; set => SetProperty(ref relevantYears, value); }

        public bool HasMoreYears { get => hasMoreYears; set => SetProperty(ref hasMoreYears, value); }

        public PersonalityModel[] RelevantPerformancePersonalities { get => relevantPerformancePersonalities; set => SetProperty(ref relevantPerformancePersonalities, value); }

        public bool HasMorePerformancePersonalities { get => hasMorePerformancePersonalities; set => SetProperty(ref hasMorePerformancePersonalities, value); }

        public PersonalityModel[] RelevantDirectionPersonalities { get => relevantDirectionPersonalities; set => SetProperty(ref relevantDirectionPersonalities, value); }

        public bool HasMoreDirectionPersonalities { get => hasMoreDirectionPersonalities; set => SetProperty(ref hasMoreDirectionPersonalities, value); }

        protected override void DisposeUnmanagementResources()
        {
            base.DisposeUnmanagementResources();

            Categories = null;
            RelevantProducerHouses = null;
            RelevantGenres = null;
            RelevantYears = null;
            RelevantPerformancePersonalities = null;
            RelevantDirectionPersonalities = null;
        }
    }
}
