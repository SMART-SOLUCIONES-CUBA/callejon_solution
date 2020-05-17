using Android.Content;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Xamarin.Forms.Picker), typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Views.Renderer.PickerRenderer))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Views.Renderer
{
    public class PickerRenderer : Xamarin.Forms.Platform.Android.PickerRenderer
    {
        public PickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Picker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null) return;

            Control.TextSize = Context.Resources.GetInteger(Resource.Integer.event_default_fontsize_dip);
        }
    }
}