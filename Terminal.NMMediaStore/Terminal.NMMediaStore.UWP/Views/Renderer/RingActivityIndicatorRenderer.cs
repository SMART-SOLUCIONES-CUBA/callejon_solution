using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(ActivityIndicator), typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Views.Renderer.RingActivityIndicatorRenderer))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Views.Renderer
{
    public class RingActivityIndicatorRenderer : ViewRenderer<ActivityIndicator, ProgressRing>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ActivityIndicator> eventArgs)
        {
            base.OnElementChanged(eventArgs);

            if (eventArgs.NewElement != null)
            {
                if (Control == null)
                {
                    var control = new ProgressRing
                    {
                        IsActive = Element.IsRunning
                    };

                    SetNativeControl(control);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ActivityIndicator.IsRunningProperty.PropertyName)
            {
                Control.IsActive = Element.IsRunning;
            }
        }
    }
}
