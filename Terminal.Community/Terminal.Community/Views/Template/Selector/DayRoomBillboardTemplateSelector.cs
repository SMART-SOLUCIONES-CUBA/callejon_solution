namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Views.Template.Selector
{
    public class DayRoomBillboardTemplateSelector : Xamarin.Forms.DataTemplateSelector
    {
        public Xamarin.Forms.DataTemplate DayPresentationsDataTemplate { get; set; }

        public Xamarin.Forms.DataTemplate PresentationOveriewDataTemplate { get; set; }

        protected override Xamarin.Forms.DataTemplate OnSelectTemplate(object item, Xamarin.Forms.BindableObject container)
        {
            if (Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Orientation == Xamarin.Essentials.DisplayOrientation.Landscape)
            {
                return DayPresentationsDataTemplate;
            }

            return PresentationOveriewDataTemplate;
        }
    }
}
