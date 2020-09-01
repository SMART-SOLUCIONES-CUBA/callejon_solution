namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Extensions
{
    public static class FormattedStringExtensions
    {
        public static void AddSepparatorIfRequired(this Xamarin.Forms.FormattedString target, Xamarin.Forms.Color color)
        {
            if (target.Spans.Count <= 0) return;

            target.Spans.Add(new Xamarin.Forms.Span { Text = " / ", TextColor = color });
        }
    }
}