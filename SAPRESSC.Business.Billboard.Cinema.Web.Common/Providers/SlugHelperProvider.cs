namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Common.Providers
{
    public class SlugHelperProvider
    {
        private static SlugHelperProvider instance;

        public static SlugHelperProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SlugHelperProvider();
                }

                return instance;
            }
        }

        private Slugify.SlugHelper slugHelper;

        public SlugHelperProvider()
        {
            slugHelper = new Slugify.SlugHelper();
        }

        public Slugify.SlugHelper Get()
        {
            return slugHelper;
        }
    }
}
