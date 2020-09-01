namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.View
{
    public class StorageViewModel : XFToolKit.Mvvm.Models.BaseBindableModel
    {
        private string caption;
        private string familyRepresentativeIconGlyph;
        private double currentUsedPercentFactor;
        private double virtualUsedPercentFactor;

        public string Caption { get => caption; set => SetProperty(ref caption, value); }

        public string FamilyRepresentativeIconGlyph { get => familyRepresentativeIconGlyph; set => SetProperty(ref familyRepresentativeIconGlyph, value); }

        public double CurrentUsedPercentFactor { get => currentUsedPercentFactor; set => SetProperty(ref currentUsedPercentFactor, value); }

        public double VirtualUsedPercentFactor { get => virtualUsedPercentFactor; set => SetProperty(ref virtualUsedPercentFactor, value); }
    }
}