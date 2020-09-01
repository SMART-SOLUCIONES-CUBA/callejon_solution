namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Managers
{
    public class MockSubscriptionManager : Facade.ISubscriptionManagerFacade
    {
        private bool hasSubscriptor;
        private bool hasSubscription;

        public MockSubscriptionManager()
        {
            hasSubscriptor = false;
            hasSubscription = false;
        }

        public async System.Threading.Tasks.Task AuthenticateAsync(string id, string password)
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            hasSubscriptor = true;
        }

        public Models.SubscriptorModel GetAuthenticatedSubscritor()
        {
            return !hasSubscriptor ? null : new Models.SubscriptorModel
            {
                Name = "Dainelvis",
                LastName = "Martínez Basulto",
                Subscription = !hasSubscription ? null : new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SubscriptionModel
                {
                    Id = "dbasulto",
                    Image = new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ImageModel
                    {
                        Id = new System.Guid("485A1093-45B0-49B5-97FA-227ED0435A6A"),
                        ContentType = "image/png"
                    }
                }
            };
        }

        public async System.Threading.Tasks.Task PersistSubscriptionAsync(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SubscriptionModel model)
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            hasSubscription = true;
        }
    }
}
