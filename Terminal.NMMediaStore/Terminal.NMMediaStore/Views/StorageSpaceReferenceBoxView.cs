namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Views
{
    public class StorageSpaceReferenceBoxView : Xamarin.Forms.BoxView
    {
        public void InvalidateWidth()
        {
            OnPropertyChanged(WidthProperty.PropertyName);
        }
    }
}
