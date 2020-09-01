using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Factories.Api.Models
{
    public static class PresentationModelFactory
    {
        public static PresentationModel BuildFor(Persistence.Entities.Presentation presentation, Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade mediaFileManager, bool cached)
        {
            var model = new PresentationModel
            {
                Id = presentation.Id,
                Date = presentation.Date,
                Production = ProductionModelFactory.BuildFor(presentation.Production, mediaFileManager, cached)
            };

            return model;
        }
    }
}
