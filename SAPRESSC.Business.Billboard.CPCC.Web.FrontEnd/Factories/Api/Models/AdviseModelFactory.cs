namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Factories.Api.Models
{
    public static class AdviseModelFactory
    {
        public static Web.Api.Common.Models.AdviseModel BuildFor(Persistence.Entities.Advise advise)
        {
            return new Web.Api.Common.Models.AdviseModel { Message = advise.Message };
        }
    }
}
