namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models
{
    public class CategoryModel
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public CategoryModel Parent { get; set; }

        public string StorageRelativePath { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
