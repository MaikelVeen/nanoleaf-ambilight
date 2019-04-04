using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using NanoleafAmbilight.Color;
using Newtonsoft.Json.Linq;

namespace NanoleafAmbilight.Nanoleaf
{
    public class NanoleafClient : IDisposable
    {
        public NanoleafClient(string ipAddress)
        {
            // TODO create method to find ip address in network using SSDP
            BaseAddress = ipAddress;
            HttpClient = new HttpClient();
            AuthToken = GetAuthToken();
        }


        private string AuthToken { get; set; }
        private string BaseAddress { get; set; }
        private HttpClient HttpClient { get; set; }

        /// <summary>
        /// Get the access token for the Nanoleaf Lights. User has to hold the
        /// power button for five seconds and call this method in the thirty
        /// seconds after lights start blinking
        /// </summary>
        /// <param name="ipAddress">The Ip Address of the Nanoleaf Light</param>
        /// <returns></returns>
        public async Task<string> GetAuthTokenAsync()
        {
            
            try
            {
                HttpResponseMessage response = await HttpClient.PostAsync($"{BaseAddress}/api/v1/new", null);
                response.EnsureSuccessStatusCode();

                string body = await response.Content.ReadAsStringAsync();
                JObject jsonObject = JObject.Parse(body);

                return jsonObject["auth_token"].ToString();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Return the authentication token
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public string GetAuthToken()
        {
            return GetAuthTokenAsync().Result;
        }

        public async Task SetColorAsync(HSBColor color)
        {
            try
            {
                HttpContent content = new StringContent("{\"hue\": {\"value\":" + color.Hue.ToString() +
                                                        "}, \"sat\": {\"value\":" + color.Saturation.ToString() +
                                                        "},  \"brightness\": {\"value\":" +
                                                        color.Brightness.ToString() + "}}");
                HttpResponseMessage response =
                    await HttpClient.PutAsync($"{BaseAddress}/api/v1/{AuthToken}/state", content);

                response.EnsureSuccessStatusCode();
            }

            catch (HttpRequestException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Start()
        {
            while (true)
            {
                SetColor(new HSBColor());
            }
        }

        public void SetColor(HSBColor color)
        {
            SetColorAsync(color).GetAwaiter().GetResult();
        }

        public void Dispose()
        {
            HttpClient?.Dispose();
        }
    }
}