namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models
{
    public class PersonalityModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public bool IsMale { get; set; }

        public System.DateTime BornDate { get; set; }

        public string About { get; set; }

        public ImageModel Image { get; set; }

        public string FullName
        {
            get => Name.Trim() + " " + LastName.Trim();
        }

        public override string ToString()
        {
            return FullName; 
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(PersonalityModel)) return false;

            return Id == (obj as PersonalityModel).Id;
        }

        public override int GetHashCode()
        {
            int hashCode = -2083761535;

            hashCode = hashCode * -1521134295 + Id.GetHashCode();

            return hashCode;
        }
    }
}
