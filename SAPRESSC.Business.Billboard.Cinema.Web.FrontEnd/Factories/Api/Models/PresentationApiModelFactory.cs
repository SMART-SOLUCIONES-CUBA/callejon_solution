using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.Api;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Factories.Api.Models
{
    public static class PresentationApiModelFactory
    {
        public static PresentationApiModel BuildFor(Persistence.Entities.Presentation presentation, Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade mediaFileManager, bool cached)
        {
            var model = new PresentationApiModel
            {
                Id = presentation.Id,
                Date = presentation.Date,
                Production = ProductionApiModelFactory.BuildFor(presentation.Production, mediaFileManager, cached)
            };

            return model;
        }
    }
}
