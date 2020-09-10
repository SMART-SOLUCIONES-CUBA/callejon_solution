namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Exceptions
{
    public class UnspectedHttpStatusException : System.Exception
    {
        public System.Net.HttpStatusCode HttpStatusCode { get; set; }
    }
}
