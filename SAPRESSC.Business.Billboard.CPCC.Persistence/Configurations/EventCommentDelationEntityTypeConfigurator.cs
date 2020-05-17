namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Configurations
{
    public class EventCommentDelationEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.EventCommentDelation>
    {
        protected override string GetTableName()
        {
            return "eventcommentdelations";
        }
    }
}
