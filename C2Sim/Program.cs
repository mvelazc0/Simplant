using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2Sim
{
    class Program
    {
        static void Main(string[] args)
        {
            Lib.DnsImplantSimulator dnsim = new Lib.DnsImplantSimulator();
            dnsim.run();
        }
    }
}
