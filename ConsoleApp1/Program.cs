using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConsoleApp1
{
    class Program
    {


        static void Main(string[] args)
        {
            string[] stockSymbols = { "AAPL", "BA" };
            Console.WriteLine("MSFT 7 Day Average");
            averageFunction();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("AAPL 6 Month Maximum");
            appleMax();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("BA Daily 1 Month Difference");
            baDifference();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Stock Symbol Larger ROI");
            largestReturn(stockSymbols);

            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }

        public static void averageFunction()
        {
        string timeSeries = "?function=TIME_SERIES_DAILY&symbol=";
        string symbol = "MSFT&apikey=";
        string apiKey = "ILEIEU1EGZRFK01R";
        string endpoint = "https://www.alphavantage.co/query";

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
                int avgCount = 0;
                //go through volumes to create average
                for (int i = 4; i < 11; i++)
                {
                    var temp = newArray[i].Split(" \"5. volume\": \"")[1].Split("\"\r\n")[0];
                    avgCount += int.Parse(temp);
                }

                avgCount /= 7;
                Console.WriteLine("MSFT 7 day volume average: " + avgCount);

            }
            else
                Console.WriteLine("error");

            return;
        }

        public static void appleMax()
        {
            string timeSeries = "?function=TIME_SERIES_MONTHLY&symbol=";
            string symbol = "AAPL&apikey=";
            string apiKey = "ILEIEU1EGZRFK01R";
            string endpoint = "https://www.alphavantage.co/query";

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
                decimal max = 0;
                //go through volumes to create average
                for (int i = 4; i < 10; i++)
                {
                    var temp = decimal.Parse(newArray[i].Split(" \"2. high\": \"")[1].Split("\",\r\n")[0]);
                    if (max < temp)
                        max = temp;
                }
                
                Console.WriteLine("AAPL 6 month maximum: " + max);

            }
            else
                Console.WriteLine("error");

            return;
        }

        public static void baDifference()
        {
            string timeSeries = "?function=TIME_SERIES_DAILY&symbol=";
            string symbol = "BA&apikey=";
            string apiKey = "ILEIEU1EGZRFK01R";
            string endpoint = "https://www.alphavantage.co/query";

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
                decimal max = 0;
                //go through volumes to create average
                for (int i = 4; i < 34; i++)
                {
                    var open = decimal.Parse(newArray[i].Split(" \"1. open\": \"")[1].Split("\",\r\n")[0]);
                    var close = decimal.Parse(newArray[i].Split(" \"4. close\": \"")[1].Split("\",\r\n")[0]);
                    var difference = open - close;
                    Console.WriteLine("BA Open Close Differential: " + difference);
                }
            }
            else
                Console.WriteLine("error");

            return;
        }

        public static void largestReturn(string[] stockSymbols)
        {
            string timeSeries = "?function=TIME_SERIES_DAILY&symbol=";
            string symbol = "&apikey=";
            string apiKey = "ILEIEU1EGZRFK01R";
            string endpoint = "https://www.alphavantage.co/query";
            string largestSymbol = "";
            decimal largestResult = 0;

            HttpClient myClient = new HttpClient();
            myClient.BaseAddress = new Uri(endpoint);
            myClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            foreach(string s in stockSymbols)
            {
                string requiredParams = timeSeries + s + symbol + apiKey;
                HttpResponseMessage response = myClient.GetAsync(requiredParams).Result;
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsStringAsync().Result;
                    JObject myJson = JObject.Parse(dataObjects);
                    string convertedJson = myJson.ToString();
                    var newArray = convertedJson.Split("{");
                    //go through volumes to create average
                    var open = decimal.Parse(newArray[4].Split(" \"1. open\": \"")[1].Split("\",\r\n")[0]);
                    var close = decimal.Parse(newArray[34].Split(" \"4. close\": \"")[1].Split("\",\r\n")[0]);
                    var difference = close - open;
                    if (difference > largestResult) {
                        largestSymbol = s;
                        largestResult = difference;
                    }
                }
                else
                    Console.WriteLine("error");
            }
            Console.WriteLine("Largest Return Was: " + largestResult.ToString() + "\nMade By: " + largestSymbol);
            return;
        }
    }
}
