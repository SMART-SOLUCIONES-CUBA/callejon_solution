namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Common
{
    public class InvalidModelException : System.Exception
    {
        public InvalidModelException(string field, string message) : base(message)
        {
            Field = field;
        }

        public string Field { get; private set; }
    }
}
