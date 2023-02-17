using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Övning.Metohds
{
    internal class Select_Currency
    {
        public static async Task <string> GetOneCurrency(Task<List<string>> currencylist,Task<List<string>>currencyListInputs)
        {
            string currency = "";

            int i = 0;
            foreach (var cur in (await currencylist))
            {
                Console.WriteLine(i + ":" + cur);
                i++;
            }
            Console.Write("Välj valuta:");
            var firstInput=Console.ReadLine();
            var finalinput=Convert.ToInt32(firstInput);
            var c = (await currencyListInputs).ToList();
            currency = c[finalinput];
            return currency;
        }
    }
}
