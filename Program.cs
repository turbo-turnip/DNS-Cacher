using System;

namespace DNS_Cacher {
    class Program {
        static void Welcome() {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Welcome to DNS Cacher!\nEnter a hostname to start.\n\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void Main(string[] args) {
            Welcome();

            Listener listener = new Listener();
            Lookup DNSLookup = new Lookup();
            CacheManager cache = new CacheManager();

            cache.register("default-cache");

            while (true) {
                listener.listen(cache.currCache);

                if (listener.currCommand.StartsWith("lookup")) {
                    DNSEntry entry = DNSLookup.find(hostname: listener.currCommand.Substring(listener.currCommand.IndexOf(" ") + 1), cache: cache.currCache);

                    if (entry != null) {
                        entry.logInfo();

                        cache.cacheEntry(entry);
                    }
                } else if (listener.currCommand.StartsWith("current-cache")) { 
                    Console.Write("* ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(cache.currCache);
                    Console.ForegroundColor = ConsoleColor.White;

                    foreach (Cache c in cache.caches) {
                        if (c.cacheName != cache.currCache) {
                            Console.WriteLine($"  {c.cacheName}");
                        }
                    } 
                } else if (listener.currCommand.StartsWith("create-cache")) {
                    string newCacheName = listener.currCommand.Substring(listener.currCommand.IndexOf(" ") + 1);

                    if (cache.register(newCacheName) == 0) {
                        Console.WriteLine($"Successfully registered new cache {newCacheName}!");

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Switched to cache {newCacheName}");
                        Console.ForegroundColor = ConsoleColor.White;
                    } else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Failed to register cache {newCacheName}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                } else if (listener.currCommand.StartsWith("switch-cache")) {
                    string newCache = listener.currCommand.Substring(listener.currCommand.IndexOf(" ") + 1);

                    cache.switchCache(cacheName: newCache);
                } else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Invalid command. ({listener.currCommand})");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}
