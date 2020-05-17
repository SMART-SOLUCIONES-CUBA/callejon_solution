namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Configurations
{
    public class CommentClassifierEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.CommentClassifier>
    {
        protected override string GetTableName()
        {
            return "commentclassifiers";
        }
    }
}
