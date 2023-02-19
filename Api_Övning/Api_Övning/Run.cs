using Api_Övning.Metohds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq.Expressions;

namespace Api_Övning
{
    internal class Run
    {
        public static async Task running()
        {
            var fullListCurrency = FileManagment.CreateListFromFile(FileManagment.path + "currency.txt");
            var shortListCurrency = FileManagment.CreateSplitedList(FileManagment.path + "currency.txt");
            var regionList = FileManagment.CreateListFromFile(FileManagment.path + "continents.txt");
            Task<Weather> weather = Weather.GetWeatherInfoOneCity("v1/weather?city=Nyköping,Sverige");
            var ip = GetYourIp.GetIPAddress();
            string ipstring = (await ip).ToString();
            Task<IpCheck> MyOwnIp = IpCheck.GetIp($"v1/iplookup?address={ipstring}");


            Console.WriteLine("Vilken stad vill du se temperaturen i?");
            string cityInput = Console.ReadLine();
            Console.WriteLine("Vilket land ligger staden i?");
            string countryInput = Console.ReadLine();
            string cityCountryInput = cityInput + "," + countryInput;
            Task<Weather> weatherwithInputs = Weather.GetWeatherInfoOneCity($"v1/weather?city={cityCountryInput}");

            string country = (await MyOwnIp).country;
            string city = (await MyOwnIp).city;
            string countryCity = city + "," + country;
            Task<Weather> weather1 = Weather.GetWeatherInfoOneCity($"v1/weather?city={countryCity}");

            Console.WriteLine("Första valutan som du vill titta på");
            var currency1 = await Select_Currency.GetOneCurrency(fullListCurrency, shortListCurrency);
            Console.WriteLine("Andra valutan");
            var currency2 = await Select_Currency.GetOneCurrency(fullListCurrency, shortListCurrency);
            string finalCurrency = currency1 + "_" + currency2;

            Task<CurrencyRate> currency = CurrencyRate.GetCurrenctValue($"v1/exchangerate?pair={currency1}_{currency2}");

            Console.WriteLine("Vilken region vill du se tiden i?");
            var regions = await SomeMethods.GetOneList(regionList, "region");

            Console.WriteLine("Vilken stad vill du se tiden i?");
            string cityfortime = Console.ReadLine();
            string timeRegionAndCity = "timezone/" + regions + "/" + cityfortime;

            Task<Times> time1 = Times.GetTimeForACity(timeRegionAndCity);


            Task.WaitAll(time1);
            var time2 = (await time1).datetime.ToString().Split('T', '.');
            var time3 = time2[1];


            Console.WriteLine();
            Console.WriteLine("Skriver ut valutorna");
            Task.WaitAll(currency);
            await currency;
            Console.WriteLine($"En {currency1} ger dig " + Math.Round(currency.Result.exchange_rate, 2) + $" {currency2}");
            Console.WriteLine();
            Task.WaitAll(MyOwnIp);
            await MyOwnIp;
            Console.WriteLine("Din ip är " + MyOwnIp.Result.address);
            Console.WriteLine();

            Task.WaitAll(weather);
            await weather;
            Console.WriteLine("Hård kodade tempen");
            Console.WriteLine(weather.Result.temp + " °C varmt i staden " + "Nyköping");
            Task.WaitAll(weatherwithInputs);
            await weatherwithInputs;
            Console.WriteLine();
            Console.WriteLine("Utvald stad");
            Console.WriteLine(weatherwithInputs.Result.temp + " °C varmt i staden " + cityInput + " som ligger i landet " + countryInput);
            Console.WriteLine();
            Task.WaitAll(weather1);
            await weather1;
            Console.WriteLine(weather1.Result.temp + " °C grader varmt i staden " + city + " i landet " + country +
                " som är baserat på din ip adess information.");

            Console.WriteLine();
            Console.WriteLine(time3 + " är tiden i staden " + cityfortime + " som ligger i värdsdelen " + regions);

        }
    }
}
