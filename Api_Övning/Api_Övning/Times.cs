using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api_Övning
{
    internal class Times
    {
            public string abbreviation { get; set; }
            public string client_ip { get; set; }
            public DateTime datetime { get; set; }
            public int day_of_week { get; set; }
            public int day_of_year { get; set; }
            public bool dst { get; set; }
            public object dst_from { get; set; }
            public int dst_offset { get; set; }
            public object dst_until { get; set; }
            public int raw_offset { get; set; }
            public string timezone { get; set; }
            public int unixtime { get; set; }
            public DateTime utc_datetime { get; set; }
            public string utc_offset { get; set; }
            public int week_number { get; set; }
        public static async Task<Times> GetTimeForACity(string uri)
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri("http://worldtimeapi.org/api/timezone/");
          

            Times time = null;



            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                time = JsonSerializer.Deserialize<Times>(responseString);
            }

            return time;
        }
    }
}
