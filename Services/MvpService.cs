using MVPStream.Models;

namespace MVPStream.Services
{
    public static class MvpService
    {
        public static MvpViewModel GetModel(ISearchService SearchService, IDocumentDB DocumentDB, string id, int page)
        {
            var model = SearchService.FromMVP(id,15, page);
            return new MvpViewModel()
            {
                Publicaciones = model.Entries,
                Cantidad=model.Count,
                PublisherId = id,
                Page=page,
                Publisher = DocumentDB.GetPublisher(id)                
            };
        }
    }
}