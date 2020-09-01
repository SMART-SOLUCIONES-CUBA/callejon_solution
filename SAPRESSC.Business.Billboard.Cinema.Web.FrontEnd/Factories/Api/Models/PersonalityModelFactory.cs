namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Factories.Api.Models
{
    public static class PersonalityModelFactory
    {
        public static Web.Api.Common.Models.PersonalityModel BuildFor(Persistence.Entities.Personality personality, Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade mediaFileManager, bool cached)
        {
            var result = new Web.Api.Common.Models.PersonalityModel
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
                result.Image = ImageModelFactory.BuildFor(personality.Image, mediaFileManager, cached);
            }

            return result;
        }
    }
}
