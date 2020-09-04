namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models
{
    public class StorageModel
    {
        public string Caption { get; set; }

        public Enumerations.StorageFamiliy Family { get; set; }

        public long UsedSpace { get; set; }

        public long TotalSpace { get; set; }

        public string Path { get; set; }
    }
}
