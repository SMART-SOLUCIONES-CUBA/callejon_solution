namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Providers.Facade
{
    public interface ISizeProviderFacade
    {
        int EventTitleStyleFontSize { get; }

        int EventMiddleEmphasisStyleFontSize { get; }

        int EventLittleEmphasisStyleFontSize { get; }

        int EventDefaultStyleFontSize { get; }

        int EventSmallStyleFontSize { get; }

        int EventPersonalityOverviewContainerSize { get; }

        int EventPersonalityOverviewImageSize { get; }

        int EventPersonalityImageSize { get; }

        int ProfileImageSize { get; }

        int ProfileOverviewImageSize { get; }

        int PersonalityCommentImageSize { get; }
    }
}
