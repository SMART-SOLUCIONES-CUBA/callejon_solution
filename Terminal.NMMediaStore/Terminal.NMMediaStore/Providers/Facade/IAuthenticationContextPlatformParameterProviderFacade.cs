namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers.Facade
{
    public interface IAuthenticationContextPlatformParameterProviderFacade
    {
        Microsoft.IdentityModel.Clients.ActiveDirectory.PlatformParameters GetPlatformParameters();
    }
}
