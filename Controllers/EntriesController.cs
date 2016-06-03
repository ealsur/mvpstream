using MVPStream.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVPStream.Controllers
{
    public class EntriesController : Controller
    {
        private readonly ISearchService _searchService;
        public EntriesController(ISearchService searchService){
            _searchService=searchService;
        }
        [Route("busqueda")]
        public IActionResult Busqueda(string q, int page=1)
        {
            if (string.IsNullOrWhiteSpace(q) || page < 1)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = EntriesService.GetModel(_searchService,q, page);
            if(model.Cantidad==0)
            {
                return View("~/Views/Error/Error404.cshtml");
            }
            return View(model);
        }

        [Route("videos")]
        public IActionResult Videos(int page = 1)
        {
            if (page < 1)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(EntriesService.GetSectionModel(_searchService,"Video", page));
        }

        [Route("publicaciones")]
        public IActionResult Publicaciones(int page = 1)
        {
            if (page < 1)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(EntriesService.GetSectionModel(_searchService,"RSS", page));
        }
    }
}