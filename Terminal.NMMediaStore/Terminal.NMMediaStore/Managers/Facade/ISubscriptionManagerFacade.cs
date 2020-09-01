namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Managers.Facade
{
    public interface ISubscriptionManagerFacade
    {
        Models.SubscriptorModel GetAuthenticatedSubscritor();

        System.Threading.Tasks.Task AuthenticateAsync(string id, string password);

        System.Threading.Tasks.Task PersistSubscriptionAsync(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SubscriptionModel model);
    }
}
