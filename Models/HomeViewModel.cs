using System.Collections.Generic;
using MVPStream.Models.Data;

namespace MVPStream.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Entry> UltimosVideos { get; set; }
        public IEnumerable<Entry> UltimasPublicaciones { get; set; }
        public IEnumerable<Entry> UltimosEventos { get; set; }
    }
}