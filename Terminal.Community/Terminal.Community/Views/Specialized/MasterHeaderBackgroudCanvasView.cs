using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Views.Specialized
{
    public class MasterHeaderBackgroudCanvasView : SKCanvasView
    {
        private SKBitmap bitmap;

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs eventArgs)
        {
            var randomizer = new System.Random();

            using (System.IO.Stream stream = GetType().Assembly.GetManifestResourceStream(string.Format("SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Resources.Images.{0}.jpg", randomizer.Next(1, 5))))
            {
                bitmap = SKBitmap.Decode(stream);
            }

            var info = eventArgs.Info;
            var surface = eventArgs.Surface;

            surface.Canvas.Clear();

            var path = SKPath.ParseSvgPathData(string.Format("M 0 0 H {0} V {1} L 0 {2} z", bitmap.Width, bitmap.Height, (int)(bitmap.Height * 0.75)));

            path.GetBounds(out SKRect bounds);

            surface.Canvas.Scale(info.Width / bounds.Width);
            surface.Canvas.ClipPath(path);
            surface.Canvas.ResetMatrix();

            surface.Canvas.DrawBitmap(bitmap, info.Rect);


            var overlay = new SKPaint()
            {
                Style = SKPaintStyle.Fill,
                Color = ((Xamarin.Forms.Color)Xamarin.Forms.Application.Current.Resources["PrimaryBrandColor"]).ToSKColor().WithAlpha(200)
            };

            surface.Canvas.DrawPaint(overlay);
        }

        // TODO Remove if no exception throwed!!
        //private static void OnPropertyChanged(Xamarin.Forms.BindableObject bindable, object oldVal, object newVal)
        //{
        //    var self = bindable as MasterHeaderBackgroudCanvasView;

        //    self?.InvalidateSurface();
        //}
    }
}
