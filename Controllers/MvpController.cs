using MVPStream.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVPStream.Controllers
{
    public class MvpController : Controller
    {
        
        [Route("mvp/{id}")]
        public IActionResult Index(string id, int page = 1)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View(MvpService.GetModel(id, page));
        }        
    }
}