namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models
{
    public class StorageModel
    {
        public string Caption { get; set; }

        public Enumerations.StorageFamiliy Family { get; set; }

        public double UsedSpace { get; set; }

        public double TotalSpace { get; set; }

        public string Path { get; set; }
    }
}
