using Xamarin.Forms;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Views.Rendered
{
    public class CircularProgressBar : View
    {
        public CircularProgressBar()
        {
        }

        public static readonly BindableProperty IndeterminateProperty =
            BindableProperty.Create(
                nameof(Indeterminate),
                typeof(bool),
                typeof(CircularProgressBar),
                false);

        public bool Indeterminate
        {
            get { return (bool)GetValue(IndeterminateProperty); }
            set { SetValue(IndeterminateProperty, value); }
        }


        public static readonly BindableProperty ProgressProperty =
            BindableProperty.Create(nameof(Progress),
                typeof(int),
                typeof(CircularProgressBar),
                0);

        /// <summary>
        /// Gets or sets the current progress
        /// </summary>
        /// <value>The progress.</value>
        public int Progress
        {
            get { return (int)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        public static readonly BindableProperty SecondaryProgressProperty =
            BindableProperty.Create(nameof(SecondaryProgress),
                typeof(int),
                typeof(CircularProgressBar),
                0);

        public int SecondaryProgress
        {
            get { return (int)GetValue(SecondaryProgressProperty); }
            set { SetValue(SecondaryProgressProperty, value); }
        }

        public static readonly BindableProperty MaxProperty =
            BindableProperty.Create(nameof(Max),
                typeof(int),
                typeof(CircularProgressBar),
                100);

        /// <summary>
        /// Gets or sets the max value
        /// </summary>
        /// <value>The max.</value>
        public int Max
        {
            get { return (int)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }
    }
}
