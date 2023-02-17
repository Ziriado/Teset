using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Övning.Metohds
{
    internal class SomeMethods
    {
        public static async Task<string> GetOneList(Task<List<string>> theList,string question)
        {
            string returnList = "";

            int i = 0;
            foreach (var cur in (await theList))
            {
                Console.WriteLine(i + ":" + cur);
                i++;
            }
            Console.Write($"Välj {question}:");
            var firstInput = Console.ReadLine();
            var finalinput = Convert.ToInt32(firstInput);
            var c = (await theList).ToList();
            returnList = c[finalinput];
            return returnList;
        }
    }
}
