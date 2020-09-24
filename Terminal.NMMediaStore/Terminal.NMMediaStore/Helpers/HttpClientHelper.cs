using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Helpers
{
    public class HttpClientHelper
    {
        private static HttpClientHelper instance;

        public static HttpClientHelper Instance 
        { 
            get 
            {
                if (instance == null)
                {
                    instance = new HttpClientHelper("http://sgc.elcallejon.net/api/");
                    //instance = new HttpClientHelper("http://localhost:5000/api/");
                }

                return instance;
            }
        }

        private const string API_KEY_HEADER_PROPERTY = "X-Api-Key";
        private const string API_KEY_VALUE = "terminal-mediastore-v0.1.0-rc-eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ0";
        private const string CONTENT_TYPE = "APPLICATION/JSON";

        private readonly HttpClient client;

        public HttpClientHelper(string baseUrl)
        {
            client = new HttpClient { BaseAddress = new System.Uri(baseUrl), Timeout = System.TimeSpan.FromSeconds(30) };

            client.DefaultRequestHeaders.Add(API_KEY_HEADER_PROPERTY, API_KEY_VALUE);
        }

        public async Task DeleteAsync(string url)
        {
            var response = await client.DeleteAsync(url);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exceptions.UnspectedHttpStatusException { HttpStatusCode = response.StatusCode };
            }
        }

        public async Task<TResponse> GetAsync<TResponse>(string url)
        {
            var response = await client.GetAsync(url);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exceptions.UnspectedHttpStatusException { HttpStatusCode = response.StatusCode };
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(responseContent);
        }

        public async Task PostAsync<TRequest>(string urlOrResource, TRequest request)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, CONTENT_TYPE);

            var response = await client.PostAsync(urlOrResource, content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exceptions.UnspectedHttpStatusException { HttpStatusCode = response.StatusCode };
            }
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string urlOrResource, TRequest request)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, CONTENT_TYPE);

            var response = await client.PostAsync(urlOrResource, content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exceptions.UnspectedHttpStatusException { HttpStatusCode = response.StatusCode };
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(responseContent);
        }

        public async Task PostFormAsync(string urlOrResource, MultipartFormDataContent multipartFormDataContent)
        {
            var response = await client.PostAsync(urlOrResource, multipartFormDataContent);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exceptions.UnspectedHttpStatusException { HttpStatusCode = response.StatusCode };
            }
        }

        public async Task PutFormAsync(string urlOrResource, MultipartFormDataContent multipartFormDataContent)
        {
            var response = await client.PutAsync(urlOrResource, multipartFormDataContent);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exceptions.UnspectedHttpStatusException { HttpStatusCode = response.StatusCode };
            }
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(string urlOrResource, TRequest request)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, CONTENT_TYPE);

            var response = await client.PutAsync(urlOrResource, content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exceptions.UnspectedHttpStatusException { HttpStatusCode = response.StatusCode };
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(responseContent);
        }

        public async Task PutAsync<TRequest>(string urlOrResource, TRequest request)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, CONTENT_TYPE);

            var response = await client.PutAsync(urlOrResource, content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exceptions.UnspectedHttpStatusException { HttpStatusCode = response.StatusCode };
            }
        }

        public async Task PutAsync(string urlOrResource)
        {
            var content = new StringContent(string.Empty, System.Text.Encoding.UTF8, CONTENT_TYPE);

            var response = await client.PutAsync(urlOrResource, content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exceptions.UnspectedHttpStatusException { HttpStatusCode = response.StatusCode };
            }
        }
    }
}
