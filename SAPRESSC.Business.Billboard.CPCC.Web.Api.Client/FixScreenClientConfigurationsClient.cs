using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.Api;
using System.Net.Http;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Client
{
    public static class FixScreenClientConfigurationsClient
    {
        public static FixScreenClientConfigurationApiModel Get(System.Uri apiServerBaseUri, System.Guid key)
        {
            var task = GetAsyncTask(apiServerBaseUri, key);

            task.Wait();

            return task.Result;
        }

        private static async System.Threading.Tasks.Task<FixScreenClientConfigurationApiModel> GetAsyncTask(System.Uri baseAddress, System.Guid key)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;

                client.Timeout = System.TimeSpan.FromSeconds(30);

                var response = await client.GetAsync("api/cpccbillboard/fixscreenclientconfigurations/" + key);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return Newtonsoft.Json.JsonConvert.DeserializeObject<FixScreenClientConfigurationApiModel>(responseContent);
                }
            }

            return null;
        }
    }
}
