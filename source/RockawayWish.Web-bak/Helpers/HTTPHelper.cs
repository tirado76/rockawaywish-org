using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;



namespace RockawayWish.Web.Helpers
{
    internal class HTTPHelper
    {
        internal async Task<T> SendRequestAsync<T>(Uri url, HttpMethod httpMethod = null,
                                                           IDictionary<string, string> headers = null,
                                                           object requestData = null)
        {
            var result = default(T);
            var method = httpMethod ?? HttpMethod.Get;


            var request = new HttpRequestMessage(method, url);

            // Serialize our request data
            var data = requestData == null ? null : JsonConvert.SerializeObject(requestData);
            if (data != null)
            {

                // Add the serialized request data to our request object
                request.Content = new FormUrlEncodedContent((Dictionary<string, string>)requestData);
                //request.Content = new StringContent(data, Encoding.UTF8, "application/json");
            }
            // Add each of the specified headers to our request
            if (headers != null)
            {
                foreach (var h in headers)
                {
                    request.Headers.Add(h.Key, h.Value);
                }
            }
            // Get a response from our Web Service
            var handler = new HttpClientHandler();
            var client = new HttpClient(handler);
            try
            {
                var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);

                if (response.IsSuccessStatusCode && response.Content != null)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<T>(content);
                }
            }
            catch (Exception ex)
            {
                string err = ex.ToString();
            }
            return result;
        }
    }
}