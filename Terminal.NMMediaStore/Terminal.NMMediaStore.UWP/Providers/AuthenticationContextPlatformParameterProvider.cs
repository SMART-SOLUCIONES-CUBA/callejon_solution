[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Providers.AuthenticationContextPlatformParameterProvider))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Providers
{
    public class AuthenticationContextPlatformParameterProvider : NMMediaStore.Providers.Facade.IAuthenticationContextPlatformParameterProviderFacade
    {
        public Microsoft.IdentityModel.Clients.ActiveDirectory.PlatformParameters GetPlatformParameters()
        {
            return new Microsoft.IdentityModel.Clients.ActiveDirectory.PlatformParameters(Microsoft.IdentityModel.Clients.ActiveDirectory.PromptBehavior.Auto, false);
        }
    }
}
