using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Net;

namespace DNS_Cacher {
    public class Cache {
        public string cacheName;
        public List<DNSEntry> cachedEntries = new List<DNSEntry>();

        public Cache(string _cacheName) {
            cacheName = _cacheName;

            if (!File.Exists($"./{cacheName}.json")) {
                File.Create($"./{cacheName}.json").Close();
                File.WriteAllText(
                    Path.Combine(Directory.GetCurrentDirectory(), _cacheName + ".json"), 
                    "{\n\t\"cached\": []\n}"
                );
            }
        }

        public CacheObject readCache(string cachePath) {
            string contents = File.ReadAllText($@"{cachePath}", Encoding.UTF8);

            try {
                CacheObject decoded = JsonSerializer.Deserialize<CacheObject>(contents);

                return decoded;
            } catch (Exception err) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"There was a problem reading the cache from {cachePath}.");
                Console.WriteLine(err.Message);
                Console.ForegroundColor = ConsoleColor.White;

                return null;
            }         
        }

        public bool cache(DNSEntry entry) {
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), cacheName + ".json"))) {
                CacheObject cacheContents = readCache(Path.Combine(Directory.GetCurrentDirectory(), cacheName + ".json"));

                List<string> IPv4 = new List<string>();
                foreach (IPAddress ip in entry.IPv4) {
                    IPv4.Add(ip.ToString());
                }
                
                List<string> IPv6 = new List<string>();
                foreach (IPAddress ip in entry.IPv6) {
                    IPv6.Add(ip.ToString());
                }

                DNSObject dnsEntry = new DNSObject {
                    IPv4 = new List<string>(IPv4),
                    IPv6 = new List<string>(IPv6),
                    hostname = entry.hostname
                };

                cacheContents.cached.Add(dnsEntry);
                string newContents = JsonSerializer.Serialize(cacheContents);

                if (newContents != null) {
                    File.WriteAllText(
                        Path.Combine(Directory.GetCurrentDirectory(), cacheName + ".json"),
                        newContents
                    );

                    return true;
                } else { return false; }
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Cannot find cache file {cacheName}");
                Console.Write("To create this cache, run the command ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"create-cache {cacheName}");
                Console.ForegroundColor = ConsoleColor.White;

                return false;
            }
        }
    }
}