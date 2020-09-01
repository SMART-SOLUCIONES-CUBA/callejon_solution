using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.Page
{
    public class SubscriptionConfigurationPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private View.GenreChoiseViewModel[] genreChoises;
        private View.ProducerHouseChoiseViewModel[] producerHouseChoises;
        private View.TagChoiseViewModel[] tagChoises;
        private GenreModel[] choisedGenres;
        private ProducerHouseModel[] choisedProducerHouses;
        private TagModel[] choisedTags;

        public SubscriptorModel Subscriptor { get; set; }

        public View.GenreChoiseViewModel[] GenreChoises { get => genreChoises; set => SetProperty(ref genreChoises, value); }

        public View.ProducerHouseChoiseViewModel[] ProducerHouseChoises { get => producerHouseChoises; set => SetProperty(ref producerHouseChoises, value); }

        public View.TagChoiseViewModel[] TagChoises { get => tagChoises; set => SetProperty(ref tagChoises, value); }

        public GenreModel[] ChoisedGenres { get => choisedGenres; set => SetProperty(ref choisedGenres, value); }

        public ProducerHouseModel[] ChoisedProducerHouses { get => choisedProducerHouses; set => SetProperty(ref choisedProducerHouses, value); }

        public TagModel[] ChoisedTags { get => choisedTags; set => SetProperty(ref choisedTags, value); }
    }
}
