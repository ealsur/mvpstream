using MVPStream.Models;
using MVPStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace MVPStream.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(Duration = 43200)]
        public IActionResult Index()
        {
            
            /*var f = DocumentDB.Insert<Publisher>("publishers", new Publisher()
            {
                Nombre = "Walter Montes Delgado",
                Lenguajes = "es",
                Site = "http://microsoft.waltermontes.com/",
                Email = "",
                LinkedIn = "",
                Facebook = "",
                Twitter = "",
                Pais = "Costa Rica",
                Especialidades = new List<string>() { "ASP.NET/IIS" },
                Imagen = "http://mvp.microsoft.com/private/publicprofile/photo?mvpid=5001007",
                Sources = new List<Source> { new Source() { Type = SourceType.RSS, Url = "http://microsoft.waltermontes.com/feed/" }}
            });*/
            return View(HomeService.GetModel());
        }

        public IActionResult Nav()
        {
            return PartialView(Especialidades.All);
        }
    }
}