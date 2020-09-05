namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Views
{
    public class VirtualStorageSpaceUseSpan : Xamarin.Forms.Span
    {
        public void InvalidateText()
        {
            var converter = new Converters.MasterPageModelToVirtualStorageUsedSpaceConverter();

            Text = converter.Convert(BindingContext, typeof(string), null, System.Globalization.CultureInfo.CurrentCulture).ToString();
        }
    }
}