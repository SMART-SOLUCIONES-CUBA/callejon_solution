[assembly: Xamarin.Forms.ExportRenderer(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Views.Rendered.ResponsiveImageCell), typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Views.Renderer.ResponsiveImageCellRenderer))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Views.Renderer
{
    public class ResponsiveImageCellRenderer : Xamarin.Forms.Platform.Android.ImageCellRenderer
    {
        protected override Android.Views.View GetCellCore(Xamarin.Forms.Cell item, Android.Views.View convertView, Android.Views.ViewGroup parent, Android.Content.Context context)
        {
            var cell = (Android.Widget.LinearLayout)base.GetCellCore(item, convertView, parent, context);
            
            var image = (Android.Widget.ImageView)cell.GetChildAt(0);
            
            image.SetScaleType(Android.Widget.ImageView.ScaleType.Center);
            
            return cell;
        }
    }
}