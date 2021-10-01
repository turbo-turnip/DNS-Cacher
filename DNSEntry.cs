using System;
using System.Collections.Generic;  
using System.Linq;  
using System.Text;  
using System.Net;  

namespace DNS_Cacher {
    public class DNSEntry {
        public List<IPAddress> IPv4 = new List<IPAddress>();
        public List<IPAddress> IPv6 = new List<IPAddress>();
        public string hostname;

        public DNSEntry(IPAddress[] addressList, string _hostname) {
            hostname = _hostname;
            foreach (IPAddress ip in addressList) {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) {
                    IPv4.Add(ip);
                } else if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6) {
                    IPv6.Add(ip);
                }
            }
        }

        public void logInfo() {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nDNS entry for {hostname}\n");

            if (IPv4.Count > 0) {
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine($"IPv4 ({IPv4.Count})");
                Console.ForegroundColor = ConsoleColor.White;
                foreach (IPAddress ip in IPv4) {
                    Console.WriteLine(ip);
                }
                Console.WriteLine("");
            }
            
            if (IPv6.Count > 0) {
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine($"IPv6 ({IPv6.Count})");
                Console.ForegroundColor = ConsoleColor.White;
                foreach (IPAddress ip in IPv6) {
                    Console.WriteLine(ip);
                }
                Console.WriteLine("");
            }
        }
    }
}