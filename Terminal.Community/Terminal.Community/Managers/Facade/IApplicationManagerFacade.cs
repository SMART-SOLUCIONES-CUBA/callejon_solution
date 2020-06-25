namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Managers.Facade
{
    public interface IApplicationManagerFacade
    {
        void Close();

        void ClearDatabase();

        long GetDatabaseSize();

        void LaunchSystemWiFiSetting();
    }
}
