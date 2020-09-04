namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.View
{
    public class StorageViewModel : XFToolKit.Mvvm.Models.BaseBindableModel
    {
        private StorageModel storage;
        private string familyRepresentativeIconGlyph;
        private double currentUsedPercentFactor;

        public StorageModel Storage { get => storage; set => SetProperty(ref storage, value); }

        public string FamilyRepresentativeIconGlyph { get => familyRepresentativeIconGlyph; set => SetProperty(ref familyRepresentativeIconGlyph, value); }

        public double CurrentUsedPercentFactor { get => currentUsedPercentFactor; set => SetProperty(ref currentUsedPercentFactor, value); }
    }
}