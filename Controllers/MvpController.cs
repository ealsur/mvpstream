using MVPStream.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVPStream.Controllers
{
    public class MvpController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly IDocumentDB _documentDB;
        public MvpController(ISearchService searchService, IDocumentDB documentDB){
            _searchService=searchService;
            _documentDB=documentDB;
        }
        [Route("mvp/{id}")]
        public IActionResult Index(string id, int page = 1)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View(MvpService.GetModel(_searchService, _documentDB,id, page));
        }        
    }
}