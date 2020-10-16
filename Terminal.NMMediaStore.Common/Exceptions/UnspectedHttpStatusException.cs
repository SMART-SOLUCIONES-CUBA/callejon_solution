namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Common.Exceptions
{
    public class UnspectedHttpStatusException : System.Exception
    {
        public System.Net.HttpStatusCode HttpStatusCode { get; set; }
    }
}
