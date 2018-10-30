using NLog;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;

namespace webnudger
{
    class Program
    {
        public static Logger _logger = LogManager.GetCurrentClassLogger();

        private static readonly HttpClient client = new HttpClient();
        
        static void Main(string[] args)
        {

            var random = new Random();
            while (true)
            {
                _logger.Info($"starting...");

                var time = DateTime.Now;
                if(time.Hour > 6 && time.Hour < 23)
                {
                    Nudge();
                }
                var interval = random.Next(240000,300000);

                
                _logger.Info($"sleeping for {Math.Round(TimeSpan.FromMilliseconds(interval).TotalMinutes, 2)} minutes");

                Thread.Sleep(interval);
            }
        }

        public static void Nudge()
        {
            _logger.Info($"making request to riadosa.org");

            var client = new RestClient("http://www.riadosa.org");
            var request = new RestRequest("/", Method.GET);
            var queryResult = client.Execute<dynamic>(request);

            _logger.Info($"Result: {queryResult.StatusCode}");
        }
    }
}
