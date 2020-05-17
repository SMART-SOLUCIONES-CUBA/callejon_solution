namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.Page
{
    public class DatabaseStatusPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private string status;
        private bool updated;
        private int cinemaCount;
        private int roomCount;
        private int presentationCount;
        private int eventCount;
        private int personalityCount;
        private int commentCount;
        private int imageCount;

        public string Status { get => status; set => SetProperty(ref status, value); }

        public bool Updated { get => updated; set => SetProperty(ref updated, value); }

        public int CinemaCount { get => cinemaCount; set => SetProperty(ref cinemaCount, value); }

        public int RoomCount { get => roomCount; set => SetProperty(ref roomCount, value); }

        public int PresentationCount { get => presentationCount; set => SetProperty(ref presentationCount, value); }

        public int EventCount { get => eventCount; set => SetProperty(ref eventCount, value); }

        public int PersonalityCount { get => personalityCount; set => SetProperty(ref personalityCount, value); }

        public int CommentCount { get => commentCount; set => SetProperty(ref commentCount, value); }

        public int ImageCount { get => imageCount; set => SetProperty(ref imageCount, value); }
    }
}
