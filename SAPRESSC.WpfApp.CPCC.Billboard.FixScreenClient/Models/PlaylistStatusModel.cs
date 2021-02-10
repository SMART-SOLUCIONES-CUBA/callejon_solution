namespace SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Models
{
    public class PlaylistStatusModel
    {
        public PlaylistItemMetadataModel[] Items { get; private set; }

        public int CurrentIndex { get; private set; }

        public PlaylistStatusModel(PlaylistItemMetadataModel[] items, int currentIndex)
        {
            Items = items;
            CurrentIndex = currentIndex;
        }
    }
}
