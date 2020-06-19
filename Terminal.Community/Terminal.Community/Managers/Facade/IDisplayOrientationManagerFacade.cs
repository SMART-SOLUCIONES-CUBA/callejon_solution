namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Managers.Facade
{
    public interface IDisplayOrientationManagerFacade
    {
        void Unlock();

        void Lock(Xamarin.Essentials.DisplayOrientation orientation);
    }
}
