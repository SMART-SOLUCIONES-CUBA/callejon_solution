namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Factories.Api.Models
{
    public static class AdviseApiModelFactory
    {
        public static Web.Api.Common.Models.Api.AdviseApiModel BuildFor(Persistence.Entities.Advise advise)
        {
            return new Web.Api.Common.Models.Api.AdviseApiModel { Message = advise.Message };
        }
    }
}
