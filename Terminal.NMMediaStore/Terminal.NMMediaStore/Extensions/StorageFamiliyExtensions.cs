namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Extensions
{
    public static class StorageFamiliyExtensions
    {
        public static string ToFamilyRepresentativeIconGlyph(this Enumerations.StorageFamiliy target)
        {
            switch (target)
            {
                case Enumerations.StorageFamiliy.SDCard:
                    return "\uf479";
                case Enumerations.StorageFamiliy.MicroSDCard:
                    return "\uf7db";
                case Enumerations.StorageFamiliy.UsbFlashDrive:
                    return "\uf553";
                case Enumerations.StorageFamiliy.UsbHDDrive:
                    return "\uf2ca";
                case Enumerations.StorageFamiliy.HostDevice:
                    return "\uf11c";
            }

            return "\uf553";
        }
    }
}
