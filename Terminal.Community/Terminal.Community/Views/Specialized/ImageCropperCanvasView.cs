using System.Collections.Generic;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Views.Specialized
{
    public class ImageCropperCanvasView : SKCanvasView
    {
        const int CORNER = 50;      // pixel length of cropper corner
        const int RADIUS = 100;     // pixel radius of touch hit-test

        SKBitmap bitmap;
        CroppingRectangle croppingRect;
        SKMatrix inverseBitmapMatrix;

        // Touch tracking 
        Effects.TouchEffect touchEffect = new Effects.TouchEffect();

        struct TouchPoint
        {
            public int CornerIndex { set; get; }
            public SKPoint Offset { set; get; }
        }

        Dictionary<long, TouchPoint> touchPoints = new Dictionary<long, TouchPoint>();

        // Drawing objects
        SKPaint cornerStroke = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.White,
            StrokeWidth = 10
        };

        SKPaint edgeStroke = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.White,
            StrokeWidth = 2
        };

        public ImageCropperCanvasView(SKBitmap bitmap, float? aspectRatio = null)
        {
            this.bitmap = bitmap;

            SKRect bitmapRect = new SKRect(0, 0, bitmap.Width, bitmap.Height);
            croppingRect = new CroppingRectangle(bitmapRect, aspectRatio);

            touchEffect.TouchAction += OnTouchEffectTouchAction;
        }

        public SKBitmap CroppedBitmap
        {
            get
            {
                SKRect cropRect = croppingRect.Rect;
                SKBitmap croppedBitmap = new SKBitmap((int)cropRect.Width,
                                                      (int)cropRect.Height);
                SKRect dest = new SKRect(0, 0, cropRect.Width, cropRect.Height);
                SKRect source = new SKRect(cropRect.Left, cropRect.Top,
                                           cropRect.Right, cropRect.Bottom);

                using (SKCanvas canvas = new SKCanvas(croppedBitmap))
                {
                    canvas.DrawBitmap(bitmap, source, dest);
                }

                return croppedBitmap;
            }
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            // Attach TouchEffect to parent view
            Parent.Effects.Add(touchEffect);
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs args)
        {
            base.OnPaintSurface(args);

            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(SKColors.Gray);

            // Calculate rectangle for displaying bitmap 
            float scale = System.Math.Min((float)info.Width / bitmap.Width, (float)info.Height / bitmap.Height);
            float x = (info.Width - scale * bitmap.Width) / 2;
            float y = (info.Height - scale * bitmap.Height) / 2;
            SKRect bitmapRect = new SKRect(x, y, x + scale * bitmap.Width, y + scale * bitmap.Height);
            canvas.DrawBitmap(bitmap, bitmapRect);

            // Calculate a matrix transform for displaying the cropping rectangle
            SKMatrix bitmapScaleMatrix = SKMatrix.MakeIdentity();

            bitmapScaleMatrix.SetScaleTranslate(scale, scale, x, y);

            // Display rectangle
            SKRect scaledCropRect = bitmapScaleMatrix.MapRect(croppingRect.Rect);
            canvas.DrawRect(scaledCropRect, edgeStroke);

            // Display heavier corners
            using (SKPath path = new SKPath())
            {
                path.MoveTo(scaledCropRect.Left, scaledCropRect.Top + CORNER);
                path.LineTo(scaledCropRect.Left, scaledCropRect.Top);
                path.LineTo(scaledCropRect.Left + CORNER, scaledCropRect.Top);

                path.MoveTo(scaledCropRect.Right - CORNER, scaledCropRect.Top);
                path.LineTo(scaledCropRect.Right, scaledCropRect.Top);
                path.LineTo(scaledCropRect.Right, scaledCropRect.Top + CORNER);

                path.MoveTo(scaledCropRect.Right, scaledCropRect.Bottom - CORNER);
                path.LineTo(scaledCropRect.Right, scaledCropRect.Bottom);
                path.LineTo(scaledCropRect.Right - CORNER, scaledCropRect.Bottom);

                path.MoveTo(scaledCropRect.Left + CORNER, scaledCropRect.Bottom);
                path.LineTo(scaledCropRect.Left, scaledCropRect.Bottom);
                path.LineTo(scaledCropRect.Left, scaledCropRect.Bottom - CORNER);

                canvas.DrawPath(path, cornerStroke);
            }

            // Invert the transform for touch tracking
            bitmapScaleMatrix.TryInvert(out inverseBitmapMatrix);
        }

        void OnTouchEffectTouchAction(object sender, Delegates.TouchActionEventArgs args)
        {
            SKPoint pixelLocation = ConvertToPixel(args.Location);
            SKPoint bitmapLocation = inverseBitmapMatrix.MapPoint(pixelLocation);

            switch (args.Kind)
            {
                case Enumerations.TouchActionKind.Pressed:
                    // Convert radius to bitmap/cropping scale
                    float radius = inverseBitmapMatrix.ScaleX * RADIUS;

                    // Find corner that the finger is touching
                    int cornerIndex = croppingRect.HitTest(bitmapLocation, radius);

                    if (cornerIndex != -1 && !touchPoints.ContainsKey(args.Id))
                    {
                        TouchPoint touchPoint = new TouchPoint
                        {
                            CornerIndex = cornerIndex,
                            Offset = bitmapLocation - croppingRect.Corners[cornerIndex]
                        };

                        touchPoints.Add(args.Id, touchPoint);
                    }
                    break;

                case Enumerations.TouchActionKind.Moved:
                    if (touchPoints.ContainsKey(args.Id))
                    {
                        TouchPoint touchPoint = touchPoints[args.Id];
                        croppingRect.MoveCorner(touchPoint.CornerIndex,
                                                bitmapLocation - touchPoint.Offset);
                        InvalidateSurface();
                    }
                    break;

                case Enumerations.TouchActionKind.Released:
                case Enumerations.TouchActionKind.Cancelled:
                    if (touchPoints.ContainsKey(args.Id))
                    {
                        touchPoints.Remove(args.Id);
                    }
                    break;
            }
        }

        SKPoint ConvertToPixel(Xamarin.Forms.Point pt)
        {
            return new SKPoint((float)(CanvasSize.Width * pt.X / Width),
                                (float)(CanvasSize.Height * pt.Y / Height));
        }

        private class CroppingRectangle
        {
            const float MINIMUM = 10;   // pixels width or height

            SKRect maxRect;             // generally the size of the bitmap
            float? aspectRatio;

            public CroppingRectangle(SKRect maxRect, float? aspectRatio = null)
            {
                this.maxRect = maxRect;
                this.aspectRatio = aspectRatio;

                // Set initial cropping rectangle
                Rect = new SKRect(0.9f * maxRect.Left + 0.1f * maxRect.Right,
                                  0.9f * maxRect.Top + 0.1f * maxRect.Bottom,
                                  0.1f * maxRect.Left + 0.9f * maxRect.Right,
                                  0.1f * maxRect.Top + 0.9f * maxRect.Bottom);

                // Adjust for aspect ratio
                if (aspectRatio.HasValue)
                {
                    SKRect rect = Rect;
                    float aspect = aspectRatio.Value;

                    if (rect.Width > aspect * rect.Height)
                    {
                        float width = aspect * rect.Height;
                        rect.Left = (maxRect.Width - width) / 2;
                        rect.Right = rect.Left + width;
                    }
                    else
                    {
                        float height = rect.Width / aspect;
                        rect.Top = (maxRect.Height - height) / 2;
                        rect.Bottom = rect.Top + height;
                    }

                    Rect = rect;
                }
            }

            public SKRect Rect { set; get; }

            public SKPoint[] Corners
            {
                get
                {
                    return new SKPoint[]
                    {
                    new SKPoint(Rect.Left, Rect.Top),
                    new SKPoint(Rect.Right, Rect.Top),
                    new SKPoint(Rect.Right, Rect.Bottom),
                    new SKPoint(Rect.Left, Rect.Bottom)
                    };
                }
            }

            public int HitTest(SKPoint point, float radius)
            {
                SKPoint[] corners = Corners;

                for (int index = 0; index < corners.Length; index++)
                {
                    SKPoint diff = point - corners[index];

                    if ((float)System.Math.Sqrt(diff.X * diff.X + diff.Y * diff.Y) < radius)
                    {
                        return index;
                    }
                }

                return -1;
            }

            public void MoveCorner(int index, SKPoint point)
            {
                SKRect rect = Rect;

                switch (index)
                {
                    case 0: // upper-left
                        rect.Left = System.Math.Min(System.Math.Max(point.X, maxRect.Left), rect.Right - MINIMUM);
                        rect.Top = System.Math.Min(System.Math.Max(point.Y, maxRect.Top), rect.Bottom - MINIMUM);
                        break;

                    case 1: // upper-right
                        rect.Right = System.Math.Max(System.Math.Min(point.X, maxRect.Right), rect.Left + MINIMUM);
                        rect.Top = System.Math.Min(System.Math.Max(point.Y, maxRect.Top), rect.Bottom - MINIMUM);
                        break;

                    case 2: // lower-right
                        rect.Right = System.Math.Max(System.Math.Min(point.X, maxRect.Right), rect.Left + MINIMUM);
                        rect.Bottom = System.Math.Max(System.Math.Min(point.Y, maxRect.Bottom), rect.Top + MINIMUM);
                        break;

                    case 3: // lower-left
                        rect.Left = System.Math.Min(System.Math.Max(point.X, maxRect.Left), rect.Right - MINIMUM);
                        rect.Bottom = System.Math.Max(System.Math.Min(point.Y, maxRect.Bottom), rect.Top + MINIMUM);
                        break;
                }

                // Adjust for aspect ratio
                if (aspectRatio.HasValue)
                {
                    float aspect = aspectRatio.Value;

                    if (rect.Width > aspect * rect.Height)
                    {
                        float width = aspect * rect.Height;

                        switch (index)
                        {
                            case 0:
                            case 3: rect.Left = rect.Right - width; break;
                            case 1:
                            case 2: rect.Right = rect.Left + width; break;
                        }
                    }
                    else
                    {
                        float height = rect.Width / aspect;

                        switch (index)
                        {
                            case 0:
                            case 1: rect.Top = rect.Bottom - height; break;
                            case 2:
                            case 3: rect.Bottom = rect.Top + height; break;
                        }
                    }
                }

                Rect = rect;
            }
        }
    }
}
