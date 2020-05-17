using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.Api;
using System.Collections.Generic;
using System.Net.Http;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Client
{
    public static class CinemaBillboardsApiClient
    {
        public static ICollection<CinemaBillboardApiModel> Get(System.Uri apiServerBaseUri, bool cached, string clientId = null, long startDateTicks = 0, long endDateTicks = 0)
        {
            var task = GetAsyncTask(apiServerBaseUri, cached, clientId, startDateTicks, endDateTicks);

            task.Wait();

            return task.Result;
        }

        private static async System.Threading.Tasks.Task<ICollection<CinemaBillboardApiModel>> GetAsyncTask(System.Uri baseAddress, bool cached, string clientId, long startDateTicks, long endDateTicks)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;

                client.Timeout = System.TimeSpan.FromSeconds(30);

                if (clientId != null)
                {
                    client.DefaultRequestHeaders.Add("x-client", clientId);
                }

                var nowDateTicks = System.DateTime.Today.Ticks;

                var uri = "api/cinemabillboard/cinemabillboards";

                if (cached)
                {
                    uri = string.Format("{0}/cached/{1}", uri, cached);
                }

                if (startDateTicks >= nowDateTicks && endDateTicks >= nowDateTicks && endDateTicks > startDateTicks)
                {
                    uri += string.Format("?start={0}&end={1}", startDateTicks, endDateTicks);
                }

                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return Newtonsoft.Json.JsonConvert.DeserializeObject<ICollection<CinemaBillboardApiModel>>(responseContent);
                }
            }

            return null;
        }
    }
}
