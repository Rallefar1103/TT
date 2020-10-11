using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        //private static  int maxId = 0;
        //private static int currentId = 0;
        static void Main(string[] args)
        {
            ApiHelper.InitializeClient();

            Task t = Task.Run(() => { LoadJson(1); }).Wait();
               
            t.Wait();

        }

        private static async Task LoadJson(int input)
        {
            JsonModel json = await JsonProcessor.LoadData(input);
            
            Console.WriteLine(json.Id);
            Console.WriteLine(json.UserId);
            Console.WriteLine(json.Title);
            Console.WriteLine(json.Body);


        }
    }
}
