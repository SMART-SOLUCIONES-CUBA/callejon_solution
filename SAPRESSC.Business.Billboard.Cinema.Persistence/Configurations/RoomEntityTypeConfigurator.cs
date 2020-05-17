namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class RoomEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.Room>
    {
        protected override string GetTableName()
        {
            return "rooms";
        }
    }
}
