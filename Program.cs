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
                    DNSEntry entry = DNSLookup.find(hostname: listener.currCommand.Substring(listener.currCommand.IndexOf(" ") + 1));

                    if (entry != null) {
                        entry.logInfo();
                    }
                } else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Invalid command. ({listener.currCommand})");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}
