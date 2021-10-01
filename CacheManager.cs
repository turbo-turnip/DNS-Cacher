using System;
using System.Collections.Generic;
using System.IO;

namespace DNS_Cacher {
    public class CacheManager {
        public List<Cache> caches = new List<Cache>();
        public string currCache;

        private bool cacheExists(string cacheName) {
            foreach (Cache c in caches) {
                if (c.cacheName == cacheName) {
                    return true;
                }
            }

            return false;
        }

        public int register(string cacheName) {
            if (cacheName.IndexOfAny(Path.GetInvalidFileNameChars()) > -1) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Cannot create cache with name of {cacheName}.");
                Console.WriteLine("Use lowercase letters and dashes for cache names.");
                Console.ForegroundColor = ConsoleColor.White;

                return -1;
            }

            if (cacheExists(cacheName)) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Cache with a name of {cacheName} already exists.");
                Console.ForegroundColor = ConsoleColor.White;

                return -1; 
            }

            Cache _newCache = new Cache(cacheName);
            caches.Add(_newCache);

            currCache = cacheName;

            return 0;
        }
    }
}