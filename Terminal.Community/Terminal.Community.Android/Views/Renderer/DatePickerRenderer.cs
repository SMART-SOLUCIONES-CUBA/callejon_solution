using Android.Content;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Xamarin.Forms.DatePicker), typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Views.Renderer.DatePickerRenderer))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Views.Renderer
{
    public class DatePickerRenderer : Xamarin.Forms.Platform.Android.DatePickerRenderer
    {
        public DatePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null) return;

            Control.TextSize = Context.Resources.GetInteger(Resource.Integer.event_default_fontsize_dip);
        }
    }
}