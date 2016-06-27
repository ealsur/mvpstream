using System.Collections.Generic;
using MVPStream.Models.Data;

namespace MVPStream.Models
{
    public class MvpViewModel:PagedViewModel
    {
        public string PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public IEnumerable<Entry> Publicaciones { get; set; }
    }
}