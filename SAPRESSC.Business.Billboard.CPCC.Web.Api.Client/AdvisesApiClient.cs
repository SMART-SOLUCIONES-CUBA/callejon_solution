using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Client
{
    public static class AdvisesApiClient
    {
        public static ICollection<AdviseModel> Get(System.Uri apiServerBaseUri)
        {
            var task = GetAsyncTask(apiServerBaseUri);

            task.Wait();

            return task.Result;
        }

        private static async System.Threading.Tasks.Task<ICollection<AdviseModel>> GetAsyncTask(System.Uri baseAddress)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;

                client.Timeout = System.TimeSpan.FromSeconds(30);

                var response = await client.GetAsync("api/cpccbillboard/advises");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return Newtonsoft.Json.JsonConvert.DeserializeObject<ICollection<AdviseModel>>(responseContent);
                }
            }

            return null;
        }
    }
}
