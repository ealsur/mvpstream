using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using MVPStream.Models;
using System.Linq;
using System;

namespace MVPStream.Services
{
    public static class SearchService
    {

        static readonly SearchServiceClient client;
        static readonly SearchIndexClient indexClient;
        static SearchService()
        {
            client = new SearchServiceClient(AzureEndpoints.SearchAccount, new SearchCredentials(AzureEndpoints.SearchKey));
            indexClient = client.Indexes.GetClient("entries");
        }

        public static SearchResults Latest(string tipo, int cantidad)
        {
            return SearchDocuments(string.Empty, string.Format("Tipo eq '{0}'", tipo), "Fecha desc", 1, cantidad);
        }

        public static SearchResults FromMVP(string id, int cantidad, int page)
        {
            return SearchDocuments(string.Empty, string.Format("PublisherId eq '{0}'", id), "Fecha desc", page, cantidad);
        }

        public static SearchResults SectionSearch(string section, int cantidad, int page)
        {
            return SearchDocuments(string.Empty, string.Format("Tipo eq '{0}'", section), "Fecha desc", page, cantidad);
        }

        public static SearchResults SimpleSearch(string query, int cantidad, int page)
        {
            return SearchDocuments(query, string.Empty, string.Empty, page, cantidad);
        }

        private static SearchResults SearchDocuments(string searchText, string filter, string orderBy, int page = 1, int pageSize = 10)
        {
            
            var sp = new SearchParameters();

            if (!string.IsNullOrEmpty(filter))
            {
                sp.Filter = filter;
            }
            sp.Top = pageSize;
            sp.Skip = (page - 1) * pageSize;
            if (!string.IsNullOrEmpty(orderBy))
            {
                sp.OrderBy = orderBy.Split(',');
            }
            sp.IncludeTotalResultCount = true;
            var results = indexClient.Documents.Search(searchText, sp);
            var model = new SearchResults() {  };
            
            model.Entries = results.Results.Select(x=>new Entry(){
                 id = (string)x.Document["id"],
                    Descripcion = (string)x.Document["Descripcion"],
                    Fecha = ((DateTimeOffset)x.Document["Fecha"]).DateTime,
                    Imagen = (string)x.Document["Imagen"],
                    Lenguajes = (string)x.Document["Lenguajes"],
                    PublisherId = (string)x.Document["PublisherId"],
                    PublisherImagen = (string)x.Document["PublisherImagen"],
                    PublisherNombre = (string)x.Document["PublisherNombre"],
                    Tags = ((string[])x.Document["Tags"]).ToList(),
                    Tipo = (string)x.Document["Tipo"],
                    Titulo = (string)x.Document["Titulo"],
                    Url = (string)x.Document["Url"]
            }).ToList();
            model.Count = results.Count.Value;
            return model;
        }
    }
}