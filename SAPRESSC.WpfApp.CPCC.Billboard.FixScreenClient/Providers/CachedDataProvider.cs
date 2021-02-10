using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Client;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Providers
{
    public class CachedDataProvider
    {
        private static CachedDataProvider singleInstance;

        public static CachedDataProvider GetSingleInstance()
        {
            if (singleInstance == null) singleInstance = new CachedDataProvider();

            return singleInstance;
        }

        public FixScreenClientConfigurationModel Configuration { get; private set; }

        public List<AdviseModel> Advises { get; private set; }
        public List<EventModel> Events { get; private set; }
        public List<CinemaBillboardModel> CinemaBillboards { get; private set; }

        public bool AdviseSyncronized { get; private set; }

        protected CachedDataProvider()
        {
            AdviseSyncronized = false;

            Advises = new List<AdviseModel>();
            Events = new List<EventModel>();
            CinemaBillboards = new List<CinemaBillboardModel>();
        }

        private System.Uri ApiServerBaseUri
        {
            get
            {
                var protocol = ConfigurationManager.AppSettings.Get("ApiSeverProtocol");
                var host = ConfigurationManager.AppSettings.Get("ApiSeverName");
                var port = int.Parse(ConfigurationManager.AppSettings.Get("ApiServerPort"));

                var url = protocol + "://" + host;

                if (!(("http" == protocol && port == 80) || ("https" == protocol && port == 443)))
                {
                    url += ":" + port;
                }

                return new System.Uri(url);
            }
        }

        private System.Guid FixScreenClientID
        {
            get
            {
                return System.Guid.Parse(ConfigurationManager.AppSettings.Get("FixScreenClientID"));
            }
        }

        public void UpdateConfiguration()
        {
            Configuration = FixScreenClientConfigurationsClient.Get(ApiServerBaseUri, FixScreenClientID);
        }

        public void UpdateAdvises()
        {
            AdviseSyncronized = false;

            Advises = AdvisesApiClient.Get(ApiServerBaseUri).ToList();

            AdviseSyncronized = true;
        }

        public void UpdateEvents()
        {
            if (Configuration == null || Configuration.ShowEvents)
            {
                Events = EventsApiClient.Get(ApiServerBaseUri, true).ToList();
            }
        }

        public void UpdateCinemaBillboards()
        {
            if (Configuration == null || Configuration.ShowPresentations)
            {
                var rnd = new System.Random();

                CinemaBillboards = Business.Billboard.Cinema.Web.Api.Client.CinemaBillboardsApiClient.Get(ApiServerBaseUri, true).ToList();

                if (CinemaBillboards.Count > 1)
                {
                    CinemaBillboards = CinemaBillboards.OrderBy(x => (rnd.Next())).ToList();
                }
            }
        }
    }
}
