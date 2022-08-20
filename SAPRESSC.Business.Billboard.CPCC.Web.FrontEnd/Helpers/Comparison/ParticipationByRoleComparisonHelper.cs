namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Helpers.Comparison
{
    public class ParticipationByRoleComparisonHelper : System.Collections.Generic.IEqualityComparer<Cinema.Persistence.Entities.Participation>
    {
        public bool Equals(Cinema.Persistence.Entities.Participation target, Cinema.Persistence.Entities.Participation reference)
        {
            return target.Id == reference.Id;
        }

        public int GetHashCode(Cinema.Persistence.Entities.Participation target)
        {
            return (int)target.Id;
        }
    }
}
