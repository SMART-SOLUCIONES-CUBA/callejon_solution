[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Providers.SizeProvider))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Providers
{
    public class SizeProvider : Community.Providers.Facade.ISizeProviderFacade
    {
        public int EventTitleStyleFontSize => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.event_title_fontsize_dip);

        public int EventDefaultStyleFontSize => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.event_default_fontsize_dip);

        public int EventLittleEmphasisStyleFontSize => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.event_little_emphasis_fontsize_dip);

        public int EventPersonalityOverviewContainerSize => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.event_personality_overview_container_size);

        public int EventPersonalityOverviewImageSize => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.event_personality_overview_image_size);

        public int EventPersonalityImageSize => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.event_personality_image_size);

        public int ProfileImageSize => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.profile_image_size);

        public int ProfileOverviewImageSize => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.profile_overview_image_size);

        public int PersonalityCommentImageSize => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.personality_comment_image_size);

        public int EventMiddleEmphasisStyleFontSize => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.event_middle_emphasis_fontsize_dip);

        public int EventSmallStyleFontSize => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.event_small_fontsize_dip);
    }
}