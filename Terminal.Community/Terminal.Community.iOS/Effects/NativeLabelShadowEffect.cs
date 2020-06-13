using System.Linq;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.ExportEffect(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS.Effects.NativeLabelShadowEffect), "LabelShadowEffect")]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS.Effects
{
    public class NativeLabelShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var effect = (Community.Effects.LabelShadowEffect)Element.Effects.FirstOrDefault(e => e is Community.Effects.LabelShadowEffect);

                if (effect != null)
                {
                    Control.Layer.ShadowRadius = effect.Radius;
                    Control.Layer.ShadowColor = effect.Color.ToCGColor();
                    Control.Layer.ShadowOffset = new CoreGraphics.CGSize(effect.DistanceX, effect.DistanceY);
                    Control.Layer.ShadowOpacity = 1.0f;
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }
    }
}
