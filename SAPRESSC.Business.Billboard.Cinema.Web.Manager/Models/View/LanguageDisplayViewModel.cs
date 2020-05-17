using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class LanguageDisplayViewModel :Core.Web.Management.Models.View.IEntityDisplayViewModel<Language, int>
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public void Import(Language entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}
