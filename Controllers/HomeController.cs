using MVPStream.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVPStream.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISearchService _searchService;
        public HomeController(ISearchService searchService){
            _searchService=searchService;
        }
        [ResponseCache(Duration = 43200)]
        public IActionResult Index()
        {
            return View(HomeService.GetModel(_searchService));
        }
    }
}