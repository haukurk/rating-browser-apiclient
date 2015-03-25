using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using samskip.rating_browser.Models;

namespace samskip.rating_browser.Clients
{
    /// <summary>
    /// This class is an API Client specially made for Windows Forms.
    /// </summary>
    internal class APIClient
    {

        internal static HttpClient GetHttpClient()
        {
            var client = new HttpClient {BaseAddress = new Uri(Settings.Default.ITAPIURL)};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        /// <summary>
        /// Make a GET request to a simple API.
        /// We assume all data is in the json object "data"
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="uri">Uri of the REST API resource.</param>
        /// <returns></returns>
        public async Task<T> Get<T>(string uri)
        {
            using (var client = GetHttpClient())
            {
                try
                {
                    using (var response = await client.GetAsync(uri).ConfigureAwait(false))
                    {
                        response.EnsureSuccessStatusCode(); // Throw if not a success code.
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonOutput = await response.Content.ReadAsStringAsync();
                            try
                            {
                                return JsonConvert.DeserializeObject<T>(jsonOutput);
                            }
                            catch(Exception e)
                            {
                                Debug.WriteLine("Error in deserialization of the JSON object. \n Message: "+e.Message + " \n\nStack: "+e.StackTrace);
                                throw new APIException("Deserialization error...", e);
                            }
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    Debug.WriteLine("Error in HTTP request. \n Message: " + e.Message + " \n\nStack: " + e.StackTrace);
                    throw new APIException("Webservice HTTP Request Error!", e);
                }

            }
            return default(T);
        }
    }
}
