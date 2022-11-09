using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Managers
{
    public class SubscriptionManager : Facade.ISubscriptionManagerFacade
    {
        private const string TARGET_API_BASE_QUERY = "mediastore/subscriptions";

        private SubscriptionModel authenticatedSubscription;

        public async System.Threading.Tasks.Task AuthenticateAsync(string id, string password)
        {
            authenticatedSubscription = await Common.Helpers.HttpClientHelper.Instance.GetAsync<SubscriptionModel>(string.Format("{0}/{1}/{2}", TARGET_API_BASE_QUERY, id, password));
        }

        public SubscriptionModel GetAuthenticatedSubscrition()
        {
            return authenticatedSubscription;
        }

        public async System.Threading.Tasks.Task SubscriptionCompleteAsync(GenreModel[] genres, ProducerHouseModel[] producerHouses, TagModel[] tags)
        {
            authenticatedSubscription.PreferedGenres = genres;
            authenticatedSubscription.PreferedProducerHouses = producerHouses;
            authenticatedSubscription.PreferedTags = tags;

            await Common.Helpers.HttpClientHelper.Instance.PutAsync(TARGET_API_BASE_QUERY, authenticatedSubscription);

            authenticatedSubscription.LastSignInAt = System.DateTime.Now;
        }
    }
}
