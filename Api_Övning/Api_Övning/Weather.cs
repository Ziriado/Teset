using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Api_Övning
{
    internal class Weather
    {

        public float wind_speed { get; set; }
        public int wind_degrees { get; set; }
        public int temp { get; set; }
        public int humidity { get; set; }
        public int sunset { get; set; }
        public int min_temp { get; set; }
        public int cloud_pct { get; set; }
        public int feels_like { get; set; }
        public int sunrise { get; set; }
        public int max_temp { get; set; }



        public static async Task<Weather> GetWeatherInfoOneCity(string uri)
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.api-ninjas.com/");
            client.DefaultRequestHeaders.Add("X-Api-Key", "Qcxnt4nHksKM6BUvDU82BQ==HY6rAWgyWZVfU7Vz");

            Weather weather = null;



            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                weather = JsonSerializer.Deserialize<Weather>(responseString);
            }

            return weather;
        }
    }
}
