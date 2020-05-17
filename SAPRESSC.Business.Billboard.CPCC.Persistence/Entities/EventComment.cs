namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities
{
    public class EventComment : Comment
    {
        public EventComment()
        {
            Delations = new System.Collections.Generic.List<EventCommentDelation>();
        }

        public Event Event { get; set; }

        public EventComment Reference { get; set; }

        public System.Collections.Generic.ICollection<EventCommentDelation> Delations { get; set; }
    }
}
