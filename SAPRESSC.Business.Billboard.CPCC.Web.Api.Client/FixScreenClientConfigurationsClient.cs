using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models;
using System.Net.Http;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Client
{
    public static class FixScreenClientConfigurationsClient
    {
        public static FixScreenClientConfigurationModel Get(System.Uri apiServerBaseUri, string apiKey, System.Guid key)
        {
            var task = GetAsyncTask(apiServerBaseUri, apiKey, key);

            task.Wait();

            return task.Result;
        }

        private static async System.Threading.Tasks.Task<FixScreenClientConfigurationModel> GetAsyncTask(System.Uri baseAddress, string apiKey, System.Guid key)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;

                client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

                client.Timeout = System.TimeSpan.FromSeconds(30);

                var response = await client.GetAsync("api/cpccbillboard/fixscreenclientconfigurations/" + key);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return Newtonsoft.Json.JsonConvert.DeserializeObject<FixScreenClientConfigurationModel>(responseContent);
                }
            }

            return null;
        }
    }
}
