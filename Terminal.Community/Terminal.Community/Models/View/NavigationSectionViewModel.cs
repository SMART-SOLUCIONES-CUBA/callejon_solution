namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.View
{
    public class NavigationSectionViewModel
    {
        public string Glyph { get; set; }

        public string Title { get; set; }

        public System.Func<Xamarin.Forms.Page> SelectionFunc { get; set; }
    }
}
