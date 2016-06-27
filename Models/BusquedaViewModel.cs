using System.Collections.Generic;
using MVPStream.Models.Data;

namespace MVPStream.Models
{
    public class BusquedaViewModel:PagedViewModel
    {
        public string Query { get; set; }
        public IEnumerable<Entry> Publicaciones { get; set; }
    }
}