using System;
using System.Collections.Generic;  
using System.Linq;  
using System.Text; 
using System.IO;
using System.Text.Json; 
using System.Net;  

namespace DNS_Cacher {
    public class Lookup {
        public DNSEntry searchCache(string hostname, string cacheName) {
            try {
                string content = File.ReadAllText($@"{Path.Combine(Directory.GetCurrentDirectory(), cacheName + ".json")}");
                CacheObject cachedEntries = JsonSerializer.Deserialize<CacheObject>(content);

                Console.WriteLine("Searching cache...");
                foreach (DNSObject entry in cachedEntries.cached) {
                    if (entry.hostname == hostname) {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"Found cached DNS records for {hostname}!");

                        List<IPAddress> addressList = new List<IPAddress>();
                        IList<string> IPTemp = entry.IPv4;
                        IPTemp.Concat(entry.IPv6);

                        foreach (string ip in IPTemp) {
                            addressList.Add(IPAddress.Parse(ip));
                        }

                        return new DNSEntry(addressList.ToArray(), hostname); 
                    }
                }

                return null;
            } catch (FileNotFoundException err) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Cache by the name of {cacheName} does not exist.");
                Console.ForegroundColor = ConsoleColor.White;

                string errorMessage = err.Message;
                if (errorMessage == null) {
                    errorMessage = err.ToString();
                }

                return null;
            }
        }

        public DNSEntry find(string hostname, string cache, out bool alreadyCached) {
            Console.WriteLine($"DNS lookup for hostname {hostname}...");
            DNSEntry entry;
            DNSEntry cacheEntry = searchCache(hostname, cache);

            if (cacheEntry == null) {
                alreadyCached = false;

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
            } else {
                alreadyCached = true;

                return cacheEntry;
            }
        }
    }
}