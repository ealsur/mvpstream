using System.Collections.Generic;
using MVPStream.Models.Data;

namespace MVPStream.Models
{
    public class SearchResults
    {
        public long Count { get; set; }
        public IEnumerable<Entry> Entries { get; set; }
    }
}