using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ResolutionGroupName("XFToolKit")]
[assembly: Xamarin.Forms.ExportEffect(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Effects.NativeTouchEffect), "TouchEffect")]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Effects
{
    public class NativeTouchEffect : PlatformEffect
    {
        private Android.Views.View view;
        private Xamarin.Forms.Element formsElement;
        private Community.Effects.TouchEffect libTouchEffect;
        private bool capture;
        private System.Func<double, double> fromPixels;
        private int[] twoIntArray = new int[2];

        private static Dictionary<Android.Views.View, NativeTouchEffect> viewDictionary = new Dictionary<Android.Views.View, NativeTouchEffect>();

        private static Dictionary<int, NativeTouchEffect> idToEffectDictionary = new Dictionary<int, NativeTouchEffect>();

        protected override void OnAttached()
        {
            // Get the Android View corresponding to the Element that the effect is attached to
            view = Control == null ? Container : Control;

            // Get access to the TouchEffect class in the .NET Standard library
            var touchEffect = (Community.Effects.TouchEffect)Element.Effects.FirstOrDefault(e => e is Community.Effects.TouchEffect);

            if (touchEffect != null && view != null)
            {
                viewDictionary.Add(view, this);

                formsElement = Element;

                libTouchEffect = touchEffect;

                // Save fromPixels function
                fromPixels = view.Context.FromPixels;

                // Set event handler on View
                view.Touch += OnTouch;
            }
        }

        protected override void OnDetached()
        {
            if (viewDictionary.ContainsKey(view))
            {
                viewDictionary.Remove(view);
                view.Touch -= OnTouch;
            }
        }

        private void OnTouch(object sender, Android.Views.View.TouchEventArgs args)
        {
            // Two object common to all the events
            var senderView = sender as Android.Views.View;
            var motionEvent = args.Event;

            // Get the pointer index
            int pointerIndex = motionEvent.ActionIndex;

            // Get the id that identifies a finger over the course of its progress
            int id = motionEvent.GetPointerId(pointerIndex);


            senderView.GetLocationOnScreen(twoIntArray);

            var screenPointerCoords = new Xamarin.Forms.Point(twoIntArray[0] + motionEvent.GetX(pointerIndex), twoIntArray[1] + motionEvent.GetY(pointerIndex));


            // Use ActionMasked here rather than Action to reduce the number of possibilities
            switch (args.Event.ActionMasked)
            {
                case Android.Views.MotionEventActions.Down:
                case Android.Views.MotionEventActions.PointerDown:
                    FireEvent(this, id, Enumerations.TouchActionKind.Pressed, screenPointerCoords, true);

                    idToEffectDictionary.Add(id, this);

                    capture = libTouchEffect.Capture;

                    break;

                case Android.Views.MotionEventActions.Move:
                    // Multiple Move events are bundled, so handle them in a loop
                    for (pointerIndex = 0; pointerIndex < motionEvent.PointerCount; pointerIndex++)
                    {
                        id = motionEvent.GetPointerId(pointerIndex);

                        if (capture)
                        {
                            senderView.GetLocationOnScreen(twoIntArray);

                            screenPointerCoords = new Xamarin.Forms.Point(twoIntArray[0] + motionEvent.GetX(pointerIndex), twoIntArray[1] + motionEvent.GetY(pointerIndex));

                            FireEvent(this, id, Enumerations.TouchActionKind.Moved, screenPointerCoords, true);
                        }
                        else
                        {
                            CheckForBoundaryHop(id, screenPointerCoords);

                            if (idToEffectDictionary[id] != null)
                            {
                                FireEvent(idToEffectDictionary[id], id, Enumerations.TouchActionKind.Moved, screenPointerCoords, true);
                            }
                        }
                    }
                    break;

                case Android.Views.MotionEventActions.Up:
                case Android.Views.MotionEventActions.Pointer1Up:
                    if (capture)
                    {
                        FireEvent(this, id, Enumerations.TouchActionKind.Released, screenPointerCoords, false);
                    }
                    else
                    {
                        CheckForBoundaryHop(id, screenPointerCoords);

                        if (idToEffectDictionary[id] != null)
                        {
                            FireEvent(idToEffectDictionary[id], id, Enumerations.TouchActionKind.Released, screenPointerCoords, false);
                        }
                    }
                    idToEffectDictionary.Remove(id);
                    break;

                case Android.Views.MotionEventActions.Cancel:
                    if (capture)
                    {
                        FireEvent(this, id, Enumerations.TouchActionKind.Cancelled, screenPointerCoords, false);
                    }
                    else
                    {
                        if (idToEffectDictionary[id] != null)
                        {
                            FireEvent(idToEffectDictionary[id], id, Enumerations.TouchActionKind.Cancelled, screenPointerCoords, false);
                        }
                    }
                    idToEffectDictionary.Remove(id);
                    break;
            }
        }

        private void CheckForBoundaryHop(int id, Xamarin.Forms.Point pointerLocation)
        {
            NativeTouchEffect touchEffectHit = null;

            foreach (Android.Views.View view in viewDictionary.Keys)
            {
                // Get the view rectangle
                try
                {
                    view.GetLocationOnScreen(twoIntArray);
                }
                catch // System.ObjectDisposedException: Cannot access a disposed object.
                {
                    continue;
                }

                var viewRect = new Xamarin.Forms.Rectangle(twoIntArray[0], twoIntArray[1], view.Width, view.Height);

                if (viewRect.Contains(pointerLocation))
                {
                    touchEffectHit = viewDictionary[view];
                }
            }

            if (touchEffectHit != idToEffectDictionary[id])
            {
                if (idToEffectDictionary[id] != null)
                {
                    FireEvent(idToEffectDictionary[id], id, Enumerations.TouchActionKind.Exited, pointerLocation, true);
                }
                if (touchEffectHit != null)
                {
                    FireEvent(touchEffectHit, id, Enumerations.TouchActionKind.Entered, pointerLocation, true);
                }
                idToEffectDictionary[id] = touchEffectHit;
            }
        }

        private void FireEvent(NativeTouchEffect touchEffect, int id, Enumerations.TouchActionKind actionType, Xamarin.Forms.Point pointerLocation, bool isInContact)
        {
            // Get the method to call for firing events
            System.Action<Xamarin.Forms.Element, Delegates.TouchActionEventArgs> onTouchAction = touchEffect.libTouchEffect.OnTouchAction;

            // Get the location of the pointer within the view
            touchEffect.view.GetLocationOnScreen(twoIntArray);

            double x = pointerLocation.X - twoIntArray[0];
            double y = pointerLocation.Y - twoIntArray[1];

            var point = new Xamarin.Forms.Point(fromPixels(x), fromPixels(y));

            // Call the method
            onTouchAction(touchEffect.formsElement, new Delegates.TouchActionEventArgs(id, actionType, point, isInContact));
        }
    }
}
