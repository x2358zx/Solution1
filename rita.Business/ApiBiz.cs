using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace rita.Business
{
    public class ApiBiz
    {
        public async Task<T> Post<T>(string url, string api, string id, object data, string header = "application/json")
        {
            try
            {
                if (data == null) data = new object();
                using (var client = new HttpClient())
                {
                    //client.Timeout = TimeSpan.FromSeconds(30);
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, header);
                    HttpResponseMessage response = await client.PostAsync($"{url}/{api}/{id ?? string.Empty}", content);
                    if (response.IsSuccessStatusCode)
                    {
                       return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception ex)
            {
    
            }
            finally { }
            return default(T);
        }
        public async Task<T> Get<T>(string url, string api, string id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //client.Timeout = TimeSpan.FromSeconds(30);
                    client.BaseAddress = new Uri(url);
                    HttpResponseMessage response = await client.GetAsync($"{url}/{api}/{id ?? string.Empty}");
                    if (response.IsSuccessStatusCode)
                    {
                       return JsonConvert.DeserializeObject<T>( response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
            return default(T);
        }
    }
}
