using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVPStream.Models
{
    public class SearchResults
    {
        public long Count { get; set; }
        public IEnumerable<Entry> Entries { get; set; }
    }
}