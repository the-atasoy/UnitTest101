using FakeNetworkUtility.DNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace FakeNetworkUtility.Ping
{
    public class NetworkService
    {
        private readonly IDNS _dns;

        public NetworkService(IDNS dns)
        {
            _dns = dns;
        }
        public string SendPing()
        {
            var dnsSuccess = _dns.SendDns();
            if (dnsSuccess) return "Success: Ping Sent";
            else return "Failed: Ping not sent";
        }

        public int PingTimeout(int a, int b)
        {
            return a + b;
        }

        public DateTime LastPingDate()
        {
            return DateTime.Now;
        }

        public PingOptions GetPingOptions()
        {
            return new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };
        }

        public IEnumerable<PingOptions> MostRecentPings()
        {
            IEnumerable<PingOptions> pingOptions = new[]
            {
                new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            },
                new PingOptions()
            {
                DontFragment = false,
                Ttl = 2
            },
                new PingOptions()
            {
                DontFragment = true,
                Ttl = 7
            },
                new PingOptions()
            {
                DontFragment = false,
                Ttl = 55
            }
        };
            return pingOptions;
        }
    }
}
