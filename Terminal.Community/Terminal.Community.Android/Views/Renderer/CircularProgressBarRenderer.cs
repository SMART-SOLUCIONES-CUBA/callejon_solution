using Android.App;
using Android.Content;
using SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Views.Rendered;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(CircularProgressBar), typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Views.Renderer.CircularProgressBarRenderer))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Views.Renderer
{
    public class CircularProgressBarRenderer : ViewRenderer<CircularProgressBar, Android.Widget.ProgressBar>
    {
        public CircularProgressBarRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CircularProgressBar> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null) return;

            var viewLayoutInflater = Android.Views.LayoutInflater.From(Application.Context);

            Application.Context.GetSystemService(Context.LayoutInflaterService);

            Android.Widget.ProgressBar nativeControl;

            if (Element.Indeterminate)
            {
                nativeControl = (Android.Widget.ProgressBar)viewLayoutInflater.Inflate(Resource.Layout.IndeterminateCircleProgressBar, null);
            }
            else
            {
                nativeControl = (Android.Widget.ProgressBar)viewLayoutInflater.Inflate(Resource.Layout.DeterminateCircleProgressBar, null);

                nativeControl.Max = Element.Max;
                nativeControl.Progress = Element.Progress;
                nativeControl.SecondaryProgress = Element.SecondaryProgress;
            }

            nativeControl.Indeterminate = Element.Indeterminate;

            SetNativeControl(nativeControl);
        }


        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null || Element == null)
                return;

            if (e.PropertyName == CircularProgressBar.MaxProperty.PropertyName)
            {
                Control.Max = Element.Max;
            }
            else if (e.PropertyName == CircularProgressBar.ProgressProperty.PropertyName)
            {
                Control.Progress = Element.Progress;
            }
            else if (e.PropertyName == CircularProgressBar.SecondaryProgressProperty.PropertyName)
            {
                Control.SecondaryProgress = Element.SecondaryProgress;
            }
            else if (e.PropertyName == CircularProgressBar.IndeterminateProperty.PropertyName)
            {
                Control.Indeterminate = Element.Indeterminate;
            }
        }
    }
}