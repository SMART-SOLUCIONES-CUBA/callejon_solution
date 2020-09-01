using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;
using System.Collections.Generic;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.Page
{
    public class SearchPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private string titleTemathic;
        private CategoryModel category;
        private int? year;
        private CountryModel country;
        private ProducerHouseModel producerHouse;
        private List<GenreModel> genres;
        private List<PersonalityModel> personalities;
        private List<TagModel> tags;
        private CategoryModel[] pickableCategories;
        private int[] pickableYears;
        private CountryModel[] pickableCountries;
        private ProducerHouseModel[] pickableProducerHouses;
        private SearchModel[] subscriptorSearches;
        private SearchModel[] otherSubscriptorsSearches;

        public SearchPageModel()
        {
            genres = new List<GenreModel>();
            personalities = new List<PersonalityModel>();
            tags = new List<TagModel>();
        }

        public string TitleTemathic { get => titleTemathic; set => SetProperty(ref titleTemathic, value); }

        public CategoryModel Category { get => category; set => SetProperty(ref category, value); }

        public int? Year { get => year; set => SetProperty(ref year, value); }

        public CountryModel Country { get => country; set => SetProperty(ref country, value); }

        public ProducerHouseModel ProducerHouse { get => producerHouse; set => SetProperty(ref producerHouse, value); }

        public List<GenreModel> Genres { get => genres; set => SetProperty(ref genres, value); }

        public List<PersonalityModel> Personalities { get => personalities; set => SetProperty(ref personalities, value); }

        public List<TagModel> Tags { get => tags; set => SetProperty(ref tags, value); }

        public CategoryModel[] PickableCategories { get => pickableCategories; set => SetProperty(ref pickableCategories, value); }

        public int[] PickableYears { get => pickableYears; set => SetProperty(ref pickableYears, value); }

        public CountryModel[] PickableCountries { get => pickableCountries; set => SetProperty(ref pickableCountries, value); }

        public ProducerHouseModel[] PickableProducerHouses { get => pickableProducerHouses; set => SetProperty(ref pickableProducerHouses, value); }

        public SearchModel[] SubscriptorSearches { get => subscriptorSearches; set => SetProperty(ref subscriptorSearches, value); }

        public SearchModel[] OtherSubscriptorsSearches { get => otherSubscriptorsSearches; set => SetProperty(ref otherSubscriptorsSearches, value); }
    }
}
