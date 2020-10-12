using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConsoleApp1
{
    class Program
    {

        private static string timeSeries = "?function=TIME_SERIES_DAILY&symbol=";
        private static string symbol = "MSFT&apikey=";
        private static string apiKey = "ILEIEU1EGZRFK01R";
        private static string endpoint = "https://www.alphavantage.co/query";

        static void Main(string[] args)
        {
            HttpClient myClient = new HttpClient();
            myClient.BaseAddress = new Uri(endpoint);
            myClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            string requiredParams = timeSeries + symbol + apiKey;
            HttpResponseMessage response = myClient.GetAsync(requiredParams).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content.ReadAsStringAsync().Result;
                JObject myJson = JObject.Parse(dataObjects);
                string convertedJson = myJson.ToString();
                var newArray = convertedJson.Split("{");

                averageFunction(newArray);
            }
            else
                Console.WriteLine("error");


            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }

        public static void averageFunction(string[] newArray)
        {

            int avgCount = 0;
            //go through volumes to create average
            for (int i = 4; i < 11; i++)
            {
                var temp = newArray[i].Split(" \"5. volume\": \"")[1].Split("\"\r\n")[0];
                avgCount += int.Parse(temp);
            }

            avgCount /= 7;
            Console.WriteLine("MSFT 7 day volume average: " + avgCount);
            return;
        }

    }
}
