using System.Collections.Generic;

namespace DNS_Cacher {
    public class DNSObject {
        public IList<string> IPv4 { get; set; }
        public IList<string> IPv6 { get; set; }
        public string hostname { get; set; }
    }
}