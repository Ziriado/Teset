using Api_Övning.Metohds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine("Vilken stad vill du se temperaturen i?");
            string cityInput = Console.ReadLine();
            Console.WriteLine("Vilket land ligger staden i?");
            string countryInput = Console.ReadLine();
            string cityCountryInput = cityInput + "," + countryInput;
            string ipstring = (await ip).ToString();
            Task<IpCheck> MyOwnIp = IpCheck.GetIp($"v1/iplookup?address={ipstring}");
            Task<Weather> weatherwithInputs = Weather.GetWeatherInfoOneCity($"v1/weather?city={cityCountryInput}");

            Console.WriteLine("Första valutan som du vill titta på");
            var currency1 = await Select_Currency.GetOneCurrency(fullListCurrency, shortListCurrency);
            Console.WriteLine("Andra valutan");
            var currency2 = await Select_Currency.GetOneCurrency(fullListCurrency, shortListCurrency);
            string finalCurrency = currency1 + "_" + currency2;

            Task<CurrencyRate> currency = CurrencyRate.GetCurrenctValue($"v1/exchangerate?pair={finalCurrency}");

            string country = (await MyOwnIp).country;
            string city = (await MyOwnIp).city;
            string countryCity = city + "," + country;

            Console.WriteLine("Vilken region vill du se tiden i?");
            var regions = await SomeMethods.GetOneList(regionList, "region");

            Console.WriteLine("Vilken stad vill du se tiden i?");
            string cityfortime = Console.ReadLine();
            string timeRegionAndCity = regions + "/" + cityfortime;

            Task<Times> time1 = Times.GetTimeForACity(timeRegionAndCity);

            Task<Weather> weather1 = Weather.GetWeatherInfoOneCity($"v1/weather?city={countryCity}");

            Console.WriteLine((await MyOwnIp).address + " är din Ip adress!");



            var weather99 = await weather1;
            Console.WriteLine();
            Console.WriteLine((await weather).temp + " är det hårkodade platsen temperatur!");



            Console.WriteLine();
            Console.WriteLine("Nu skriver vi ut med hjälp av 2 metoder");
            Console.WriteLine("Det är " + (weather99).temp + " °C i " + city);



            Console.WriteLine();
            Console.WriteLine("Nu skriver vi med hjälp utav inputs!");
            Console.WriteLine((await weatherwithInputs).temp + " °C i staden " + cityInput + " som ligger i landet " + countryInput);




            Console.WriteLine();
            Console.WriteLine("Nu pratar vi valutor!");
            Console.WriteLine("En " + currency1 + " ger dig " + Math.Round((await currency).exchange_rate, 2) + " " + currency2);

            Console.WriteLine();
            Console.WriteLine("Klockan är " + (await time1).utc_datetime + " och beffiner sig i tidzonen " + (await time1).timezone);
        }
    }
}
