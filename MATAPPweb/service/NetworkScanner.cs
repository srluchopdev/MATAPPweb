using System.Net.NetworkInformation;
using System.Net;

namespace YourNamespace.Services
{
    public class NetworkScanner
    {
        public List<string> ScanNetworkForSlimeVRDevices()
        {
            List<string> ipAddresses = new List<string>();
            string localIP = GetLocalIPAddress();
            string subnet = localIP.Substring(0, localIP.LastIndexOf('.'));

            for (int i = 1; i < 255; i++)
            {
                string ip = $"{subnet}.{i}";
                Ping ping = new Ping();
                PingReply reply = ping.Send(ip, 100);

                if (reply.Status == IPStatus.Success)
                {
                    ipAddresses.Add(ip);
                }
            }

            return ipAddresses;
        }

        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}

