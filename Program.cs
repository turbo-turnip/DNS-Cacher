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

                DNSEntry entry = DNSLookup.find(hostname: listener.currCommand);

                if (entry != null) {
                    entry.logInfo();
                }
            }
        }
    }
}
