namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Factories.Api.Models
{
    public static class PersonalityApiModelFactory
    {
        public static Web.Api.Common.Models.Api.PersonalityApiModel BuildFor(Persistence.Entities.Personality personality, Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade mediaFileManager, bool cached)
        {
            var result = new Web.Api.Common.Models.Api.PersonalityApiModel
            {
                Id = personality.Id,
                Name = personality.Name,
                LastName = personality.LastName,
                IsMale = personality.Sex == Middleware.Identity.Persistence.Enumerations.Sex.Male,
                BornDate = personality.BornDate,
                About = personality.About
            };

            if (personality.Image != null)
            {
                result.Image = ImageApiModelFactory.BuildFor(personality.Image, mediaFileManager, cached);
            }

            return result;
        }
    }
}
