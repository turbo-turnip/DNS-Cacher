using System;
using System.Collections.Generic;

namespace DNS_Cacher {
    public class CacheObject {
        public IList<DNSObject> cached { get; set; }
    }
}