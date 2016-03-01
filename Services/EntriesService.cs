using MVPStream.Models;

namespace MVPStream.Services
{
    public static class EntriesService
    {
        public static BusquedaViewModel GetModel(string query, int page)
        {
            query = System.Uri.UnescapeDataString(query);
            var model = SearchService.SimpleSearch(query, 15, page);
            return new BusquedaViewModel()
            {
                Query=query,
                Page=page,
                Publicaciones = model.Entries,
                Cantidad=model.Count
            };
        }

        public static BusquedaViewModel GetSectionModel(string section, int page)
        {
            var model = SearchService.SectionSearch(section, 24, page);
            return new BusquedaViewModel()
            {
                Publicaciones = model.Entries,
                Cantidad=model.Count,
                Page = page
            };
        }
    }
}