using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;

namespace C2Sim.Lib
{
    class HttpImplantSimulator
    {
        public string domain { get; set; } = "getbobspizza.com";
        public string[] headers { get; set; }
        public string[] uris { get; set; }
        public static int sleep { get; set; } = 2;
        public int port { get; set; } = 80;
        public static Lib.Logger logger { get; set; }
        public static bool https { get; set; } = false;

        public HttpImplantSimulator()
        {
            logger = new Lib.Logger(AppDomain.CurrentDomain.BaseDirectory + "beaconsim.log");
            logger.SimulationHeader("HTTP");
        }

        public void WebRequest(string url)
        {
            var client = new HttpClient();
            try
            {
                logger.TimestampInfo(String.Format("Performing GET request to {0}", url));
                var response = client.GetAsync(url);
                if (response.Result.IsSuccessStatusCode)
                {

                    logger.TimestampInfo(String.Format("I was able to perform request to {0}", url));

                }
                else
                {
                    logger.TimestampInfo(String.Format("Received {0} when requesting {1}", response.Result.StatusCode, url));
                    
                }
            }
            catch
            {
                logger.TimestampInfo("Error with HTTP request");
            }

        }
        public void run()
        {
            while (true)
            {
                Random rnd = new Random();
                Thread.Sleep(sleep * 1000);
                WebRequest("http://www.cmu.edu.pe/aaaaaaaaaaaa");
            }
        }
    }
}
