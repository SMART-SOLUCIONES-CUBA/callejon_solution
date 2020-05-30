namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Effects
{
    public class TouchEffect : Xamarin.Forms.RoutingEffect
    {
        public event Delegates.TouchActionEventHandler TouchAction;

        public TouchEffect() : base("XFToolKit.TouchEffect")
        {
        }

        public bool Capture { set; get; }

        public void OnTouchAction(Xamarin.Forms.Element element, Delegates.TouchActionEventArgs args)
        {
            TouchAction?.Invoke(element, args);
        }
    }
}
