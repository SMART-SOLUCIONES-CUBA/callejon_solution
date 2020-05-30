namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.Data
{
    public class TerminalDataModel
    {
		public string Model { get; set; }

		public string Manufacturer { get; set; }

		public string Name { get; set; }

		public string VersionString { get; set; }

		public System.Version Version { get; set; }

		public Xamarin.Essentials.DevicePlatform Platform { get; set; }

		public Xamarin.Essentials.DeviceIdiom Idiom { get; set; }

		public Xamarin.Essentials.DeviceType DeviceType { get; set; }
	}
}
