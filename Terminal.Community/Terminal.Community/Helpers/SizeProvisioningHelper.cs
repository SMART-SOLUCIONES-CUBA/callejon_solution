using SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Providers.Facade;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Helpers
{
    internal sealed class SizeProvisioningHelper
    {
        private const int LARGE_SCREEN_MASTER_PAGE_WIDTH = 320;

        private const int STANDARD_SCREEN_MASTER_PAGE_WIDTH = 240;

        private static SizeProvisioningHelper instance;

        public static SizeProvisioningHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SizeProvisioningHelper();
                }

                return instance;
            }
        }

        private SizeProvisioningHelper() { }

        public float PresentationOverviewHeight => 240;

        public float EventOverviewHeight => 240;

        public float PresentationDetailPosterWrapperHeight => GetScreenWidthDPI();

        public float EventDetailPosterWrapperHeight => GetScreenWidthDPI();

        public int EventTitleStyleFontSize => Xamarin.Forms.DependencyService.Get<ISizeProviderFacade>().EventTitleStyleFontSize;

        public int EventDefaultStyleFontSize => Xamarin.Forms.DependencyService.Get<ISizeProviderFacade>().EventDefaultStyleFontSize;

        public int EventSmallStyleFontSize => Xamarin.Forms.DependencyService.Get<ISizeProviderFacade>().EventSmallStyleFontSize;

        public int EventLittleEmphasisStyleFontSize => Xamarin.Forms.DependencyService.Get<ISizeProviderFacade>().EventLittleEmphasisStyleFontSize;

        public int EventMiddleEmphasisStyleFontSize => Xamarin.Forms.DependencyService.Get<ISizeProviderFacade>().EventMiddleEmphasisStyleFontSize;

        public int EventPersonalityOverviewContainerSize => Xamarin.Forms.DependencyService.Get<ISizeProviderFacade>().EventPersonalityOverviewContainerSize;

        public int EventPersonalityOverviewImageSize => Xamarin.Forms.DependencyService.Get<ISizeProviderFacade>().EventPersonalityOverviewImageSize;

        public int EventPersonalityImageSize => Xamarin.Forms.DependencyService.Get<ISizeProviderFacade>().EventPersonalityImageSize;

        public int ProfileImageSize => Xamarin.Forms.DependencyService.Get<ISizeProviderFacade>().ProfileImageSize;

        public int ProfileOverviewImageSize => Xamarin.Forms.DependencyService.Get<ISizeProviderFacade>().ProfileOverviewImageSize;

        public int PersonalityCommentImageSize => Xamarin.Forms.DependencyService.Get<ISizeProviderFacade>().PersonalityCommentImageSize;

        public double MasterPageDefaultWidth
        {
            get
            {
                var screenWith = GetScreenWidthDPI();

                switch (Xamarin.Forms.Device.RuntimePlatform)
                {
                    case Xamarin.Forms.Device.Android:
                        if (screenWith < STANDARD_SCREEN_MASTER_PAGE_WIDTH)
                        {
                            return screenWith;
                        }

                        if (screenWith > LARGE_SCREEN_MASTER_PAGE_WIDTH)
                        {
                            return LARGE_SCREEN_MASTER_PAGE_WIDTH;
                        }

                        return STANDARD_SCREEN_MASTER_PAGE_WIDTH;

                    case Xamarin.Forms.Device.iOS:
                        return screenWith * 0.8;

                    default:
                        throw new System.InvalidOperationException();
                }
            }
        }

        private int GetScreenWidthDPI()
        {
            return System.Convert.ToInt32(Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width / Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density);
        }
    }
}
