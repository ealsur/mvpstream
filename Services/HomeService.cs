using MVPStream.Models;

namespace MVPStream.Services
{
    public static class HomeService
    {
        public static HomeViewModel GetModel()
        {
            return new HomeViewModel() {
                UltimasPublicaciones = SearchService.Latest("RSS",20).Entries,
                UltimosVideos = SearchService.Latest("Video", 10).Entries
            };
        }
    }
}