[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS.Managers.ApplicationManager))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS.Managers
{
    public sealed class ApplicationManager : Community.Managers.Facade.IApplicationManagerFacade
    {
        public void ClearDatabase()
        {
            throw new System.NotImplementedException();
        }

        public void Close()
        {
            System.Threading.Thread.CurrentThread.Abort();
        }

        public long GetDatabaseSize()
        {
            throw new System.NotImplementedException();
        }
    }
}