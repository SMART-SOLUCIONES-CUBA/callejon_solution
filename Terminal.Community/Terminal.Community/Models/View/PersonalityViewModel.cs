namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.View
{
    public class PersonalityViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public bool IsMale { get; set; }

        public System.DateTime BornDate { get; set; }

        public string About { get; set; }

        public System.Guid ImageId { get; set; }

        public string FullName => ToString();

        public override string ToString()
        {
            return string.Format("{0} {1}", Name.Trim(), LastName.Trim());
        }
    }
}
