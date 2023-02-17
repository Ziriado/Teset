using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api_Övning
{

    internal class CurrencyRate
    {
        public string currency_pair { get; set; }
        public float exchange_rate { get; set; }

        public static async Task<CurrencyRate> GetCurrenctValue(string uri)

        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.api-ninjas.com/");
            client.DefaultRequestHeaders.Add("X-Api-Key", "Qcxnt4nHksKM6BUvDU82BQ==HY6rAWgyWZVfU7Vz");
            CurrencyRate currency = null;

            //string baseAdress = "https://api.api-ninjas.com/";
            //client.BaseAddress= new Uri(baseAdress);

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                currency = JsonSerializer.Deserialize<CurrencyRate>(responseString);
            }

            return currency;
        }
    }
}
