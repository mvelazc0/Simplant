using DnsClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace C2Sim.Lib
{

    class DnsImplantSimulator
    {

        public string domain { get; set; } = "getbobspizza.com";
        public string[] subdomains { get; set; } = { "c2", "pizza", "deepdish", "resolver" };
        public static int sleep { get; set; } = 2;
        public int port { get; set; } = 53;
        public static Lib.Logger logger { get; set; }

        public DnsImplantSimulator()
        {
            logger = new Lib.Logger(AppDomain.CurrentDomain.BaseDirectory + "beaconsim.log");
            logger.SimulationHeader("DNS");
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string rstring= new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            return rstring;
            //var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(rstring);
            //return System.Convert.ToBase64String(plainTextBytes);

        }

        public static void TxtResolution(string fqdn, int type = 3)
        {
            QueryType querytype;

            switch (type)
            {
                case 1:
                    querytype = QueryType.TXT;
                    break;

                case 2:
                    querytype = QueryType.A;
                    break;

                case 3:
                    querytype = QueryType.AAAA;
                    break;

                default:
                    querytype = QueryType.TXT;
                    break;

            }
                 
            logger.TimestampInfo(String.Format("Resolving {0} record for {1}", querytype.ToString(), fqdn));
            var lookup = new LookupClient();

            try
            {
                var result = lookup.Query(fqdn, QueryType.TXT);
                if (result.Answers.Count > 1)
                {
                    foreach (var TxtRecord in result.Answers.TxtRecords())
                    {
                        logger.TimestampInfo(TxtRecord.ToString());
                    }
                }
                else 
                {
                    logger.TimestampInfo("No result found");
                }
            }
            catch 
            {
                logger.TimestampInfo("Error with DNS request");

            }

        }

        public void run()
        {
            while (true)
            {
                Random rnd = new Random();
                Thread.Sleep(sleep * 1000);
                TxtResolution(RandomString(63)+"."+ subdomains[rnd.Next(1, 4)] + "."+domain);
            }
        }

    }



}
