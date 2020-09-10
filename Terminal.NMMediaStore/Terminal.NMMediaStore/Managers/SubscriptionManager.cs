using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Managers
{
    public class SubscriptionManager : Facade.ISubscriptionManagerFacade
    {
        private const string AUTHORITY = "elcallejon.net";

        private Models.SubscriptorModel authenticatedSubscriptor;

        public async System.Threading.Tasks.Task AuthenticateAsync(string id, string password)
        {
            var authContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(AUTHORITY);

            if (authContext.TokenCache.ReadItems().Any())
            {
                authContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
            }

            var platformParams = Xamarin.Forms.DependencyService.Get<Providers.Facade.IAuthenticationContextPlatformParameterProviderFacade>().GetPlatformParameters();
            
            var authResult = await authContext.AcquireTokenAsync("?????", id, null, platformParams);

            authenticatedSubscriptor = new Models.SubscriptorModel
            {
                Name = authResult.UserInfo.GivenName,
                LastName = authResult.UserInfo.FamilyName
            };
        }

        public Models.SubscriptorModel GetAuthenticatedSubscritor()
        {
            throw new System.NotImplementedException();
        }

        public System.Threading.Tasks.Task PersistSubscriptionAsync(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SubscriptionModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
