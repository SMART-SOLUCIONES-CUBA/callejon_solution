using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Views.Rendered.ExtendedEditor), typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Views.Renderer.ExtendedEditorRenderer))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Views.Renderer
{
    public class ExtendedEditorRenderer : EditorRenderer
    {
        private GradientDrawable backgroundDrawable;

        public ExtendedEditorRenderer(Android.Content.Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var element = e.NewElement as Community.Views.Rendered.ExtendedEditor;

                backgroundDrawable = new GradientDrawable();

                backgroundDrawable.SetShape(ShapeType.Rectangle);
                backgroundDrawable.SetColor(element.BackgroundColor.ToAndroid());
                backgroundDrawable.SetCornerRadius(element.CornerRadius);
                backgroundDrawable.SetStroke((int)element.BorderWidth, element.BorderColor.ToAndroid());

                element.Focused += (sender, evt) =>
                {
                    backgroundDrawable.SetStroke((int)element.BorderWidth, element.FocusBorderColor.ToAndroid());
                };

                element.Unfocused += (sender, evt) =>
                {
                    backgroundDrawable.SetStroke((int)element.BorderWidth, element.BorderColor.ToAndroid());
                };

                Control.Background = backgroundDrawable;

                var controlPadding = Context.Resources.GetInteger(Resource.Integer.event_default_fontsize_dip) * 2;

                Control.SetPadding(controlPadding, controlPadding, controlPadding, controlPadding);
            }
        }
    }
}