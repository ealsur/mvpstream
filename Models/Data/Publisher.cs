using System.Collections.Generic;

namespace MVPStream.Models.Data
{
    public class Publisher:Document
    {
        public Publisher() : base("publisher")
        {
           
        }
        public string id { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public List<string> Especialidades { get; set; }
        public string Email { get; set; }
        public string Pais { get; set; }
        public string Site { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string LinkedIn { get; set; }
        public string Lenguajes { get; set; }
        public List<Source> Sources { get; set; }
        
    }
}
