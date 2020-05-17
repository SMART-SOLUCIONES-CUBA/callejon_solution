using Xamarin.Forms;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Views.Rendered
{
    public class ExtendedEditor : Editor
    {
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(ExtendedEditor), Color.Transparent, propertyChanged: PropertyBorderColorChanged);

        public static readonly BindableProperty FocusBorderColorBindableProperty = BindableProperty.Create(nameof(FocusBorderColor), typeof(Color), typeof(ExtendedEditor), Color.Transparent);

        public static readonly BindableProperty BorderWidthBindableProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(ExtendedEditor), 0.5);

        public static readonly BindableProperty CornerRadiusBindableProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(ExtendedEditor), 5);

        /// <summary>
        /// Border color requires <see cref="BorderWidth"/> to be set
        /// </summary>
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);

            set => SetValue(BorderColorProperty, value);
        }

        /// <summary>
        /// Background color On focus of entry
        /// </summary>
        public Color FocusBorderColor
        {
            get => (Color)GetValue(FocusBorderColorBindableProperty);

            set => SetValue(FocusBorderColorBindableProperty, value);
        }

        /// <summary>
        /// Border width
        /// </summary>
        public double BorderWidth
        {
            get => (double)GetValue(BorderWidthBindableProperty);

            set => SetValue(BorderWidthBindableProperty, value);
        }

        /// <summary>
        /// Corner radius
        /// </summary>
        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusBindableProperty);

            set => SetValue(CornerRadiusBindableProperty, value);
        }
        
        /// <summary>
        /// Properties the border color changed.
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void PropertyBorderColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ExtendedEditor context)
            {
                context.FocusBorderColor = (Color)newValue;
            }
        }
    }
}
