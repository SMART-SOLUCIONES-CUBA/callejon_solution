using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Managers.Facade
{
    public interface ISubscriptionManagerFacade
    {
        SubscriptionModel GetAuthenticatedSubscrition();

        System.Threading.Tasks.Task AuthenticateAsync(string id, string password);

        System.Threading.Tasks.Task SubscriptionCompleteAsync(GenreModel[] genres, ProducerHouseModel[] producerHouses, TagModel[] tags);
    }
}
