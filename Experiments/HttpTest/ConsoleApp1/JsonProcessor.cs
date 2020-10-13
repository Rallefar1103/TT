using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class JsonProcessor
    {
        public static async Task<JsonModel> LoadData(int input = 0)
        {
            string url = "";

            if (input > 0)
            {
                url = $"https://jsonplaceholder.typicode.com/posts/{ input }";
            }
            else
            {
                url = "https://jsonplaceholder.typicode.com/posts/1";
            }
            
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url)) 
            {
                if (response.IsSuccessStatusCode)
                {
                    JsonModel json = await response.Content.ReadAsAsync<JsonModel>();

                    return json;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
