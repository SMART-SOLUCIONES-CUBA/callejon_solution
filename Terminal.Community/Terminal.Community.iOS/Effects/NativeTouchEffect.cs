using System.Linq;

[assembly: Xamarin.Forms.ExportEffect(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS.Effects.NativeTouchEffect), "TouchEffect")]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS.Effects
{
    public class NativeTouchEffect : Xamarin.Forms.Platform.iOS.PlatformEffect
    {
        UIKit.UIView view;
        Recognizers.TouchRecognizer touchRecognizer;

        protected override void OnAttached()
        {
            // Get the iOS UIView corresponding to the Element that the effect is attached to
            view = Control == null ? Container : Control;

            // Get access to the TouchEffect class in the .NET Standard library
            var effect = (Community.Effects.TouchEffect)Element.Effects.FirstOrDefault(e => e is Community.Effects.TouchEffect);

            if (effect != null && view != null)
            {
                // Create a TouchRecognizer for this UIView
                touchRecognizer = new Recognizers.TouchRecognizer(Element, view, effect);

                view.AddGestureRecognizer(touchRecognizer);
            }
        }

        protected override void OnDetached()
        {
            if (touchRecognizer != null)
            {
                // Clean up the TouchRecognizer object
                touchRecognizer.Detach();

                // Remove the TouchRecognizer from the UIView
                view.RemoveGestureRecognizer(touchRecognizer);
            }
        }
    }
}
