namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities
{
    public class PersonalityComment : Comment
    {
        public PersonalityComment()
        {
            Delations = new System.Collections.Generic.List<PersonalityCommentDelation>();
        }

        public Cinema.Persistence.Entities.Personality Personality { get; set; }

        public PersonalityComment Reference { get; set; }

        public System.Collections.Generic.ICollection<PersonalityCommentDelation> Delations { get; set; }
    }
}
