using System;
using System.Collections.Generic;  
using System.Linq;  
using System.Text;  
using System.Net;  

namespace DNS_Cacher {
    public class Lookup {
        public DNSEntry find(string hostname, string cache) {
            Console.WriteLine($"DNS lookup for hostname {hostname}...");
            DNSEntry entry;

            try {
                IPAddress[] addressList = Dns.GetHostAddresses(hostname);

                if (addressList.Length > 0) {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Found DNS recor{(addressList.Length > 1 ? "ds" : "d")} for {hostname}. ({addressList.Length}) IPs Found");
                    Console.ForegroundColor = ConsoleColor.White;

                    entry = new DNSEntry(addressList, hostname);

                    return entry; 
                }
            } catch (Exception err) {
                if (err != null) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Invalid hostname. ({hostname})");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                return null;
            }

            return null;
        }
    }
}