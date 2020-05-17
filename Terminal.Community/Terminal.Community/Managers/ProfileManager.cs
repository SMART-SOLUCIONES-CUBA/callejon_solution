using Xamarin.Forms;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Managers
{
    public sealed class ProfileManager
    {
        private const string PROFILE_KEY = "profile-key";

        private static ProfileManager instance;

        public static ProfileManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProfileManager();
                }

                return instance;
            }
        }

        private ProfileManager()
        {
        }

        public Models.View.ProfileViewModel GetCurrentProfile()
        {
            if (!Application.Current.Properties.ContainsKey(PROFILE_KEY)) return null;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.View.ProfileViewModel>(Application.Current.Properties[PROFILE_KEY].ToString());

            if (result.Id == System.Guid.Empty)
            {
                result.Id = System.Guid.NewGuid();

                SaveAsCurrentProfileAsync(result).ContinueWith((System.Threading.Tasks.Task task) => { });
            }

            return result;
        }

        internal async System.Threading.Tasks.Task SaveAsCurrentProfileAsync(Models.View.ProfileViewModel model)
        {
            Application.Current.Properties[PROFILE_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(model);

            await Application.Current.SavePropertiesAsync();
        }

        public async System.Threading.Tasks.Task ClearCurrentProfileAsync()
        {
            Application.Current.Properties.Clear();

            await Application.Current.SavePropertiesAsync();
        }
    }
}
