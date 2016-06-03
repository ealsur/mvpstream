using MVPStream.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVPStream.Controllers
{
    public class MvpController : Controller
    {
        private readonly ISearchService _searchService;
        public MvpController(ISearchService searchService){
            _searchService=searchService;
        }
        [Route("mvp/{id}")]
        public IActionResult Index(string id, int page = 1)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View(MvpService.GetModel(_searchService,id, page));
        }        
    }
}