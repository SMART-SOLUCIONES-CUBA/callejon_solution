using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS.Recognizers
{
    public class TouchRecognizer : UIGestureRecognizer
    {
        Element element;        // Forms element for firing events
        UIView view;            // iOS UIView 
        Community.Effects.TouchEffect touchEffect;
        bool capture;

        static Dictionary<UIView, TouchRecognizer> viewDictionary =
            new Dictionary<UIView, TouchRecognizer>();

        static Dictionary<long, TouchRecognizer> idToTouchDictionary =
            new Dictionary<long, TouchRecognizer>();

        public TouchRecognizer(Element element, UIView view, Community.Effects.TouchEffect touchEffect)
        {
            this.element = element;
            this.view = view;
            this.touchEffect = touchEffect;

            viewDictionary.Add(view, this);
        }

        public void Detach()
        {
            viewDictionary.Remove(view);
        }

        // touches = touches of interest; evt = all touches of type UITouch
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            foreach (UITouch touch in touches.Cast<UITouch>())
            {
                long id = touch.Handle.ToInt64();

                FireEvent(this, id, Enumerations.TouchActionKind.Pressed, touch, true);

                if (!idToTouchDictionary.ContainsKey(id))
                {
                    idToTouchDictionary.Add(id, this);
                }
            }

            // Save the setting of the Capture property
            capture = touchEffect.Capture;
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            foreach (UITouch touch in touches.Cast<UITouch>())
            {
                long id = touch.Handle.ToInt64();

                if (capture)
                {
                    FireEvent(this, id, Enumerations.TouchActionKind.Moved, touch, true);
                }
                else
                {
                    CheckForBoundaryHop(touch);

                    if (idToTouchDictionary[id] != null)
                    {
                        FireEvent(idToTouchDictionary[id], id, Enumerations.TouchActionKind.Moved, touch, true);
                    }
                }
            }
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            foreach (UITouch touch in touches.Cast<UITouch>())
            {
                long id = touch.Handle.ToInt64();

                if (capture)
                {
                    FireEvent(this, id, Enumerations.TouchActionKind.Released, touch, false);
                }
                else
                {
                    CheckForBoundaryHop(touch);

                    if (idToTouchDictionary[id] != null)
                    {
                        FireEvent(idToTouchDictionary[id], id, Enumerations.TouchActionKind.Released, touch, false);
                    }
                }
                idToTouchDictionary.Remove(id);
            }
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            foreach (UITouch touch in touches.Cast<UITouch>())
            {
                long id = touch.Handle.ToInt64();

                if (capture)
                {
                    FireEvent(this, id, Enumerations.TouchActionKind.Cancelled, touch, false);
                }
                else if (idToTouchDictionary[id] != null)
                {
                    FireEvent(idToTouchDictionary[id], id, Enumerations.TouchActionKind.Cancelled, touch, false);
                }
                idToTouchDictionary.Remove(id);
            }
        }

        void CheckForBoundaryHop(UITouch touch)
        {
            long id = touch.Handle.ToInt64();

            // TODO: Might require converting to a List for multiple hits
            TouchRecognizer recognizerHit = null;

            foreach (UIView view in viewDictionary.Keys)
            {
                CGPoint location = touch.LocationInView(view);

                if (new CGRect(new CGPoint(), view.Frame.Size).Contains(location))
                {
                    recognizerHit = viewDictionary[view];
                }
            }
            if (recognizerHit != idToTouchDictionary[id])
            {
                if (idToTouchDictionary[id] != null)
                {
                    FireEvent(idToTouchDictionary[id], id, Enumerations.TouchActionKind.Exited, touch, true);
                }
                if (recognizerHit != null)
                {
                    FireEvent(recognizerHit, id, Enumerations.TouchActionKind.Entered, touch, true);
                }
                idToTouchDictionary[id] = recognizerHit;
            }
        }

        void FireEvent(TouchRecognizer recognizer, long id, Enumerations.TouchActionKind actionType, UITouch touch, bool isInContact)
        {
            // Convert touch location to Xamarin.Forms Point value
            CGPoint cgPoint = touch.LocationInView(recognizer.View);

            Point xfPoint = new Point(cgPoint.X, cgPoint.Y);

            // Get the method to call for firing events
            System.Action<Element, Delegates.TouchActionEventArgs> onTouchAction = recognizer.touchEffect.OnTouchAction;

            // Call that method
            onTouchAction(recognizer.element, new Delegates.TouchActionEventArgs(id, actionType, xfPoint, isInContact));
        }
    }
}
