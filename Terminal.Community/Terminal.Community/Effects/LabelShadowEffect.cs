namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Effects
{
    public class LabelShadowEffect : Xamarin.Forms.RoutingEffect
	{
		public LabelShadowEffect() : base("XFToolKit.LabelShadowEffect")
		{
		}

		public float Radius { get; set; }

		public Xamarin.Forms.Color Color { get; set; }

		public float DistanceX { get; set; }

		public float DistanceY { get; set; }
	}
}
