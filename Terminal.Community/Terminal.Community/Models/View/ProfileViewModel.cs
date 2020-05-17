namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.View
{
    public class ProfileViewModel : XFToolKit.Mvvm.Models.BaseBindableModel
    {
        public System.Guid Id { get; set; }

        private string firstName;
        private string lastName;
        private string phone;

        public string FirstName { get => firstName; set => SetProperty(ref firstName, value); }

        public string LastName { get => lastName; set => SetProperty(ref lastName, value); }

        public string Phone { get => phone; set => SetProperty(ref phone, value); }

        public string Email { get; set; }

        public string About { get; set; }

        public System.DateTime BornDate { get; set; }

        public bool IsMale { get; set; }

        public ProfileViewModel()
        {
            Id = System.Guid.NewGuid();
            Email = string.Empty;
            About = string.Empty;
            IsMale = true;
            BornDate = System.DateTime.MinValue;
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName);
        }
    }
}
