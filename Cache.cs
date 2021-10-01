using System;
using System.Collections.Generic;
using System.IO;

namespace DNS_Cacher {
    public class Cache {
        public string cacheName;

        public Cache(string _cacheName) {
            cacheName = _cacheName;

            File.Create($"./{cacheName}.json");
        }
    }
}