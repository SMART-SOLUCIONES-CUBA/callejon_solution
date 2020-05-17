namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.View
{
    public class GenreViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
