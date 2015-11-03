using MVPStream.Services;
using Microsoft.AspNet.Mvc;

namespace MVPStream.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(Duration = 43200)]
        public IActionResult Index()
        {
            return View(HomeService.GetModel());
        }
    }
}