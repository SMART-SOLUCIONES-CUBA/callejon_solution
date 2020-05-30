using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.ExportRenderer(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Views.Rendered.ExtendedEditor), typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS.Views.Renderer.ExtendedEditorRenderer))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS.Views.Renderer
{
    public class ExtendedEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Editor> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var element = e.NewElement as Community.Views.Rendered.ExtendedEditor;

                Control.Layer.CornerRadius = element.CornerRadius;
                Control.Layer.BorderColor = element.BorderColor.ToCGColor();
                Control.Layer.BorderWidth = System.Convert.ToInt32(element.BorderWidth);
            }
        }
    }
}
