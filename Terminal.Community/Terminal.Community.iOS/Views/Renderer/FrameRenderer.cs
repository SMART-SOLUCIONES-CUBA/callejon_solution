using CoreAnimation;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Frame), typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS.Views.Renderer.FrameRenderer))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS.Views.Renderer
{
    public class FrameRenderer : Xamarin.Forms.Platform.iOS.FrameRenderer
    {
        private ShadowView shadowView;

        public override void SetupLayer()
        {
            Element.HasShadow = false;

            base.SetupLayer();

			if (shadowView == null)
			{
				shadowView = new ShadowView(Layer);

                SetNeedsLayout();
			}

			shadowView.UpdateBackgroundColor();
			shadowView.Layer.CornerRadius = Layer.CornerRadius;
			shadowView.Layer.BorderColor = Layer.BorderColor;

			shadowView.Hidden = !Element.IsVisible;
		}

		public override void LayoutSubviews()
		{
			if (shadowView != null)
			{
				if (shadowView.Superview == null && Superview != null)
					Superview.InsertSubviewBelow(shadowView, this);

				shadowView?.SetNeedsLayout();
			}
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				if (shadowView != null)
				{
					shadowView.RemoveFromSuperview();
					shadowView.Dispose();
					shadowView = null;
				}
			}
		}

		private class ShadowView : UIView
		{
			private CALayer shadowee;

			private CGRect previousBounds;
			private CGRect previousFrame;

			public ShadowView(CALayer shadowee)
			{
				this.shadowee = shadowee;

				Layer.ShadowRadius = 1;
				Layer.ShadowColor = UIColor.Gray.CGColor;
				Layer.ShadowOpacity = 0.65f;
				Layer.ShadowOffset = new System.Drawing.SizeF(0, 1);
				Layer.BorderWidth = 1;
				UserInteractionEnabled = false;
			}

			public void UpdateBackgroundColor()
			{
                if (shadowee.BackgroundColor.Alpha < 1)
                    BackgroundColor = UIColor.Clear;
                else
					BackgroundColor = new UIColor(shadowee.BackgroundColor);
			}

			public override void LayoutSubviews()
			{
				if (shadowee.Bounds != previousBounds || shadowee.Frame != previousFrame)
				{
					base.LayoutSubviews();

					SetBounds();
				}
			}

			void SetBounds()
			{
				Layer.Frame = shadowee.Frame;
				Layer.Bounds = shadowee.Bounds;

				previousBounds = shadowee.Bounds;
				previousFrame = shadowee.Frame;
			}
        }
	}
}