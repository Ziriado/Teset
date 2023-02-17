using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Api_Övning
{
    internal class FileManagment
    {
        public static string path = "../../../Files/";

        public static string[] values = new string[] {"Euro,EUR","Pound,GBP","Krona,SEK","Rubble,RUB","Dollar,USD" };

        public static void CreateFile(string textFileName)
        {
            File.WriteAllText(path + $"{textFileName}.txt", "");
        }
        public static void FillCurrencyFile(string[] content,string nameOfFile)
        {
            string currencyPath = path + nameOfFile+".txt";

            using (StreamWriter writer =new StreamWriter(currencyPath))
            {
                for(int i = 0; i < content.Length; i++)
                {
                    writer.WriteLine(content[i]);
                }
            }
        }
        public static async Task <List<string>> CreateListFromFile(string filepath)
        {
            List<string> list = new List<string>();
            using (StreamReader reader=new StreamReader(filepath))
            {
                string fileContent=reader.ReadLine();
                while (fileContent != null)
                {
                    list.Add(fileContent);
                    fileContent = reader.ReadLine();
                }
            }
                return list;
        }

        public static async Task<List<string>>CreateSplitedList(string filepath)
        {
            List<string> list = new List<string>();

            using (StreamReader reader = new StreamReader(filepath))
            {
                string fileContent =reader.ReadLine();
                while (fileContent != null)
                {
                    string[] arr = fileContent.Split(',');

                    list.Add(arr[1]);
                    fileContent = reader.ReadLine();
                }
                return list;
            }
        }
    }
}
