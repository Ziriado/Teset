using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api_Övning
{
    internal class IpCheck
    {
            public bool is_valid { get; set; }
            public string country { get; set; }
            public string country_code { get; set; }
            public string region_code { get; set; }
            public string region { get; set; }
            public string city { get; set; }
            public string zip { get; set; }
            public float lat { get; set; }
            public float lon { get; set; }
            public string timezone { get; set; }
            public string isp { get; set; }
            public string address { get; set; }

        public static async Task<IpCheck> GetIp(string uri)
        {
            IpCheck myIp = null;
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.api-ninjas.com/");
            client.DefaultRequestHeaders.Add("X-Api-Key", "Qcxnt4nHksKM6BUvDU82BQ==HY6rAWgyWZVfU7Vz");

            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                myIp = JsonSerializer.Deserialize<IpCheck>(responseString);
            }
            
            return myIp;
        }


    }
}
