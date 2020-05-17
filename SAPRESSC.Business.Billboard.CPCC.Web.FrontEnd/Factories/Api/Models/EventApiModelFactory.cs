using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Factories.Api.Models;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Factories.Api.Models
{
    public static class EventApiModelFactory
    {
        public static Web.Api.Common.Models.Api.EventApiModel BuildFor(Persistence.Entities.Event @event, Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade mediaFileManager, bool cached)
        {
            var result = new Web.Api.Common.Models.Api.EventApiModel
            {
                Id = @event.Id,
                Header = @event.Header,
                Title = @event.Title,
                Enabled = @event.Enabled,
                Start = @event.Start,
                End = @event.End,
                About = @event.About,
                Description = @event.Description,
                Place = @event.Place,
                Thematic = @event.Thematic,
                Organizer = @event.Organizer,
                PosterImage = ImageApiModelFactory.BuildFor(@event.PosterImage, mediaFileManager, cached)
            };

            if (@event.PromoVideo != null)
            {
                result.PromoVideo = VideoApiModelFactory.BuildFor(@event.PromoVideo, mediaFileManager, cached);
            }

            foreach (var bridge in @event.EventPersonalityBridges)
            {
                if (bridge.IsPresenter)
                {
                    result.Presenters.Add(PersonalityApiModelFactory.BuildFor(bridge.Personality, mediaFileManager, cached));
                }
                else
                {
                    result.WithIns.Add(PersonalityApiModelFactory.BuildFor(bridge.Personality, mediaFileManager, cached));
                }
            }

            return result;
        }
    }
}
