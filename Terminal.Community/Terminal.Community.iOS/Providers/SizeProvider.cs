[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS.Providers.SizeProvider))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS.Providers
{
    public class SizeProvider : Community.Providers.Facade.ISizeProviderFacade
    {
        public int EventTitleStyleFontSize => 27;

        public int EventMiddleEmphasisStyleFontSize => 19;

        public int EventLittleEmphasisStyleFontSize => 16;

        public int EventDefaultStyleFontSize => 14;

        public int EventSmallStyleFontSize => 11;

        public int EventPersonalityOverviewContainerSize => 100;

        public int EventPersonalityOverviewImageSize => 72;

        public int EventPersonalityImageSize => 144;

        public int ProfileImageSize => 96;

        public int ProfileOverviewImageSize => 96;

        public int PersonalityCommentImageSize => 48;
    }
}