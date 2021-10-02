using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DNS_Cacher {
    public class CacheManager {
        public List<Cache> caches = new List<Cache>();
        public string currCache;

        public CacheManager() {
            string[] cacheFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.json");

            foreach (string cacheFile in cacheFiles) {
                string cacheName = Regex.Replace(cacheFile, $@"({Directory.GetCurrentDirectory().Replace(@"\", @"\\")})|(\.json)|(/)|(\\)", "");

                if (!cacheExists(cacheName) && cacheName != "default-cache") {
                    caches.Add(new Cache(_cacheName: cacheName));
                }
            }
        }

        private bool cacheExists(string cacheName) {
            foreach (Cache c in caches) {
                if (c.cacheName == cacheName) {
                    return true;
                }
            }

            return false;
        }

        public void switchCache(string cacheName) {
            if (cacheExists(cacheName)) {
                string oldCache = currCache;
                currCache = cacheName;

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Switched caches; from {oldCache} to {cacheName}");
                Console.ForegroundColor = ConsoleColor.White;
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Cache by the name {cacheName} does not exist.");
                Console.ForegroundColor = ConsoleColor.White;
            }
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