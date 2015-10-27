using System;
using System.Collections.Generic;
using System.Linq;

namespace MVPStream.Models
{
    public class MvpViewModel:PagedViewModel
    {
        public string PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public IEnumerable<Entry> Publicaciones { get; set; }
    }
}