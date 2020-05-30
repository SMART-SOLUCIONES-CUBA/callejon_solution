namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Views.Template.Selector
{
    public class DayEventTemplateSelector : Xamarin.Forms.DataTemplateSelector
    {
        public Xamarin.Forms.DataTemplate DayEventsDataTemplate { get; set; }

        public Xamarin.Forms.DataTemplate EventOveriewDataTemplate { get; set; }

        protected override Xamarin.Forms.DataTemplate OnSelectTemplate(object item, Xamarin.Forms.BindableObject container)
        {
            if (Xamarin.Forms.Device.Idiom != Xamarin.Forms.TargetIdiom.Phone || Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Orientation == Xamarin.Essentials.DisplayOrientation.Landscape)
            {
                return DayEventsDataTemplate;
            }

            return EventOveriewDataTemplate;
        }
    }
}
