using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.Api;
using System.Collections.Generic;
using System.Net.Http;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Client
{
    public static class EventsApiClient
    {
        public static ICollection<EventApiModel> Get(System.Uri apiServerBaseUri, bool cached, string clientId = null, long startDateTicks = 0, long endDateTicks = 0)
        {
            var task = GetAsyncTask(apiServerBaseUri, cached, clientId, startDateTicks, endDateTicks);

            task.Wait();

            return task.Result;
        }

        private static async System.Threading.Tasks.Task<ICollection<EventApiModel>> GetAsyncTask(System.Uri baseAddress, bool cached, string clientId, long startDateTicks, long endDateTicks)
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

                var uri = "api/cpccbillboard/events";

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

                    return Newtonsoft.Json.JsonConvert.DeserializeObject<ICollection<EventApiModel>>(responseContent);
                }
            }

            return null;
        }
    }
}
