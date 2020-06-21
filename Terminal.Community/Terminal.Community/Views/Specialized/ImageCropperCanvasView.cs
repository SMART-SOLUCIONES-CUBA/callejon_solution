using System.Collections.Generic;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Views.Specialized
{
    public class ImageCropperCanvasView : SKCanvasView
    {
        const int CORNER = 50;      // pixel length of cropper corner
        const int RADIUS = 100;     // pixel radius of touch hit-test

        private double rotationAngle;
        private float? aspectRatio;
        private SKBitmap originBitmap;
        private SKBitmap workBitmap;
        private CroppingRectangle croppingRect;
        private SKMatrix inverseBitmapMatrix;

        // Touch tracking 
        private Effects.TouchEffect touchEffect;

        private Dictionary<long, TouchPoint> touchPoints;

        // Drawing objects
        private SKPaint cornerStroke;
        private SKPaint edgeStroke;
        private SKPaint maskFillPaint;

        public ImageCropperCanvasView(SKBitmap originBitmap, float? aspectRatio = null)
        {
            touchEffect = new Effects.TouchEffect();

            touchPoints = new Dictionary<long, TouchPoint>();

            cornerStroke = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.White,
                StrokeWidth = 10
            };

            edgeStroke = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.White,
                StrokeWidth = 2
            };

            maskFillPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Black.WithAlpha(84)
            };

            this.originBitmap = originBitmap;
            this.aspectRatio = aspectRatio;

            workBitmap = originBitmap;

            SKRect bitmapRect = new SKRect(0, 0, workBitmap.Width, workBitmap.Height);

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
                    canvas.DrawBitmap(workBitmap, source, dest);
                }

                return croppedBitmap;
            }
        }

        public void RotateToRight()
        {
            rotationAngle += 90;

            if (rotationAngle >= 360)
            {
                rotationAngle = 0;
            }

            PerformRotation();
        }

        public void RotateToLeft()
        {
            rotationAngle -= 90;

            if (rotationAngle < 0)
            {
                rotationAngle = 360 - System.Math.Abs(rotationAngle);
            }

            PerformRotation();
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
            float scale = System.Math.Min((float)info.Width / workBitmap.Width, (float)info.Height / workBitmap.Height);

            var inverseScaleMatrix = SKMatrix.MakeIdentity();

            inverseScaleMatrix.ScaleX = scale;
            inverseScaleMatrix.ScaleY = scale;

            float x = (info.Width - scale * workBitmap.Width) / 2;
            float y = (info.Height - scale * workBitmap.Height) / 2;

            var bitmapRect = new SKRect(x, y, x + scale * workBitmap.Width, y + scale * workBitmap.Height);

            canvas.DrawBitmap(workBitmap, bitmapRect);

            // Calculate a matrix transform for displaying the cropping rectangle
            var cropRectMatrix = SKMatrix.MakeIdentity();

            cropRectMatrix.ScaleX = scale;
            cropRectMatrix.ScaleY = scale;
            cropRectMatrix.TransX = x;
            cropRectMatrix.TransY = y;

            // Display rectangle
            var scaledCropRect = cropRectMatrix.MapRect(croppingRect.Rect);

            canvas.Save();

            using (var maskPath = new SKPath())
            {
                maskPath.AddRect(bitmapRect);

                canvas.ClipPath(maskPath);

                using (var inverseMaskPath = new SKPath())
                {
                    inverseMaskPath.AddRect(scaledCropRect);

                    canvas.ClipPath(inverseMaskPath, SKClipOperation.Difference);

                    canvas.DrawPaint(maskFillPaint);
                }
            }

            canvas.Restore();

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
            cropRectMatrix.TryInvert(out inverseBitmapMatrix);
        }

        private void PerformRotation()
        {
            double radians = System.Math.PI * rotationAngle / 180;

            float sine = (float)System.Math.Abs(System.Math.Sin(radians));
            float cosine = (float)System.Math.Abs(System.Math.Cos(radians));

            int rotatedWidth = (int)(cosine * originBitmap.Width + sine * originBitmap.Height);
            int rotatedHeight = (int)(cosine * originBitmap.Height + sine * originBitmap.Width);

            workBitmap = new SKBitmap(rotatedWidth, rotatedHeight);

            using (SKCanvas canvas = new SKCanvas(workBitmap))
            {
                canvas.Clear(SKColors.Gray);

                canvas.Translate(rotatedWidth / 2, rotatedHeight / 2);

                canvas.RotateDegrees((float)rotationAngle);

                canvas.Translate(-originBitmap.Width / 2, -originBitmap.Height / 2);

                canvas.DrawBitmap(originBitmap, new SKPoint());
            }

            SKRect bitmapRect = new SKRect(0, 0, workBitmap.Width, workBitmap.Height);

            croppingRect = new CroppingRectangle(bitmapRect, aspectRatio);

            InvalidateSurface();
        }

        private void OnTouchEffectTouchAction(object sender, Delegates.TouchActionEventArgs args)
        {
            SKPoint pixelLocationPoint = ConvertToPixel(args.Location);
            SKPoint bitmapLocationPoint = inverseBitmapMatrix.MapPoint(pixelLocationPoint);

            switch (args.Kind)
            {
                case Enumerations.TouchActionKind.Pressed:
                    // Convert radius to bitmap/cropping scale
                    float radius = inverseBitmapMatrix.ScaleX * RADIUS;

                    if (!touchPoints.ContainsKey(args.Id) && croppingRect.IsHitTouchPoint(bitmapLocationPoint))
                    {
                        // Find corner that the finger is touching
                        int cornerIndex = croppingRect.GetHitTouchPointCornerIndex(bitmapLocationPoint, radius);

                        var touchPoint = new TouchPoint
                        {
                            CornerIndex = cornerIndex,
                            Offset = cornerIndex != -1 ? bitmapLocationPoint - croppingRect.Corners[cornerIndex] : bitmapLocationPoint
                        };

                        touchPoints.Add(args.Id, touchPoint);
                    }

                    break;

                case Enumerations.TouchActionKind.Moved:
                    if (touchPoints.ContainsKey(args.Id))
                    {
                        var touchPoint = touchPoints[args.Id];

                        if (touchPoint.CornerIndex != -1)
                        {
                            croppingRect.MoveCorner(touchPoint.CornerIndex, bitmapLocationPoint - touchPoint.Offset);
                        }
                        else
                        {
                            croppingRect.Move(bitmapLocationPoint - touchPoint.Offset);
                        }

                        InvalidateSurface();
                    }

                    break;

                case Enumerations.TouchActionKind.Released:
                case Enumerations.TouchActionKind.Cancelled:
                    if (touchPoints.ContainsKey(args.Id))
                    {
                        touchPoints.Remove(args.Id);

                        croppingRect.ResetTranslation();
                    }

                    break;
            }
        }

        private SKPoint ConvertToPixel(Xamarin.Forms.Point pt)
        {
            return new SKPoint((float)(CanvasSize.Width * pt.X / Width), (float)(CanvasSize.Height * pt.Y / Height));
        }

        #region Private Type

        private struct TouchPoint
        {
            public int CornerIndex { set; get; }
            public SKPoint Offset { set; get; }
        }

        private class CroppingRectangle
        {
            private float minSize;
            private SKPoint translation;
            private SKRect maxRect;             // generally the size of the bitmap
            private float? aspectRatio;

            public CroppingRectangle(SKRect maxRect, float? aspectRatio = null)
            {
                minSize = (float)(System.Math.Min(maxRect.Width, maxRect.Height) * 0.2);

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

            public int GetHitTouchPointCornerIndex(SKPoint point, float radius)
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

            public bool IsHitTouchPoint(SKPoint point)
            {
                return Rect.Contains(point);
            }

            public void Move(SKPoint point)
            {
                SKRect rect = Rect;

                var beforeTranslation = translation;

                var variation = point - beforeTranslation;

                translation += variation;

                rect.Offset(variation);

                var verticalOverflow = rect.Top < 0 ? rect.Top : 0;

                if (verticalOverflow == 0)
                {
                    verticalOverflow = rect.Bottom > maxRect.Bottom ? rect.Bottom - maxRect.Bottom  : 0;
                }

                var horizontalOverflow = rect.Left < 0 ? rect.Left : 0;

                if (horizontalOverflow == 0)
                {
                    horizontalOverflow = rect.Right > maxRect.Right ? rect.Right - maxRect.Right : 0;
                }

                if (verticalOverflow != 0 || horizontalOverflow != 0)
                {
                    var relocationPoint = new SKPoint(horizontalOverflow * -1, verticalOverflow * -1);

                    rect.Offset(relocationPoint);
                }

                Rect = rect;
            }

            public void ResetTranslation()
            {
                translation = SKPoint.Empty;
            }

            public void MoveCorner(int index, SKPoint point)
            {
                SKRect rect = Rect;

                switch (index)
                {
                    case 0: // upper-left
                        rect.Left = System.Math.Min(System.Math.Max(point.X, maxRect.Left), rect.Right - minSize);
                        rect.Top = System.Math.Min(System.Math.Max(point.Y, maxRect.Top), rect.Bottom - minSize);
                        break;

                    case 1: // upper-right
                        rect.Right = System.Math.Max(System.Math.Min(point.X, maxRect.Right), rect.Left + minSize);
                        rect.Top = System.Math.Min(System.Math.Max(point.Y, maxRect.Top), rect.Bottom - minSize);
                        break;

                    case 2: // lower-right
                        rect.Right = System.Math.Max(System.Math.Min(point.X, maxRect.Right), rect.Left + minSize);
                        rect.Bottom = System.Math.Max(System.Math.Min(point.Y, maxRect.Bottom), rect.Top + minSize);
                        break;

                    case 3: // lower-left
                        rect.Left = System.Math.Min(System.Math.Max(point.X, maxRect.Left), rect.Right - minSize);
                        rect.Bottom = System.Math.Max(System.Math.Min(point.Y, maxRect.Bottom), rect.Top + minSize);
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

        #endregion
    }
}
