namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Extensions
{
    public static class PersonalityExtensions
    {
        public static Api.Common.Models.PersonalityModel ToModel(this Persistence.Entities.Personality target)
        {
            return new Api.Common.Models.PersonalityModel
            {
                Id = target.Id,
                Name = target.Name,
                LastName = target.LastName,
                BornDate = target.BornDate,
                IsMale = target.Sex == Middleware.Identity.Persistence.Enumerations.Sex.Male,
                About = target.About,
                Image = target.Image == null ? null : new Api.Common.Models.ImageModel { Id = target.Image.Id, ContentType = target.Image.ContentType }
            };
        }
    }
}
