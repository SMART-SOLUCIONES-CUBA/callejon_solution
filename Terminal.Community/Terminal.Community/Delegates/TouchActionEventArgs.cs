namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Delegates
{
    public class TouchActionEventArgs : System.EventArgs
    {
        public TouchActionEventArgs(long id, Enumerations.TouchActionKind kind, Xamarin.Forms.Point location, bool isInContact)
        {
            Id = id;
            Kind = kind;
            Location = location;
            IsInContact = isInContact;
        }

        public long Id { private set; get; }

        public Enumerations.TouchActionKind Kind { private set; get; }

        public Xamarin.Forms.Point Location { private set; get; }

        public bool IsInContact { private set; get; }
    }
}
