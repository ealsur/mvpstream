using MVPStream.Models;
using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using MVPStream.Services;
using System.Xml;

namespace MVPStream.Controllers
{
    public class MapController : Controller
    {
        [Route("map")]
        [ResponseCache(Duration = 43200)]
        public IActionResult Index()
        {
            const string baseUrl = "http://mvpstream.azurewebsites.net/";
            var xmlMap = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
            xmlMap.Append("<url><loc>"+baseUrl+"</loc><lastmod>"+DateTime.Now.ToString("yyyy-MM-dd")+"</lastmod><changefreq>daily</changefreq></url>");
            xmlMap.Append("<url><loc>" + baseUrl + "publicaciones</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod><changefreq>daily</changefreq></url>");
            xmlMap.Append("<url><loc>" + baseUrl + "videos</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod><changefreq>daily</changefreq></url>");
            foreach (var especialidad in MVPStream.Models.Especialidades.All)
            {
                
                xmlMap.Append("<url><loc>" + baseUrl + "busqueda?q="+System.Uri.EscapeUriString(especialidad.GetNombre())+"</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod><changefreq>daily</changefreq></url>");
            }
            foreach (var publisher in DocumentDB.GetAllPublishers().Result)
            {
                xmlMap.Append("<url><loc>" + baseUrl + "mvp/" + publisher.id+ "</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod><changefreq>daily</changefreq></url>");
            }
            xmlMap.Append("</urlset>");
            return Content(xmlMap.ToString(), "application/xml");
        }        
        
        [Route("feed")]
        [ResponseCache(Duration = 43200)]
        public IActionResult Feed()
        {
            using(var stream = new System.IO.MemoryStream())
            {
            const string baseUrl = "http://mvpstream.azurewebsites.net/";
            var model = HomeService.GetModel();
            var TextWriter = new XmlTextWriter(stream, Encoding.UTF8);
            TextWriter.WriteStartDocument();
        
            //Below tags are mandatory rss tag
            TextWriter.WriteStartElement("rss");
            TextWriter.WriteAttributeString("version", "2.0");
        
            // Channel tag will contain RSS feed details
            TextWriter.WriteStartElement("channel");
            TextWriter.WriteElementString("title", "MVPStream");
            TextWriter.WriteElementString("description", "MVPStream RSS Feed");
            TextWriter.WriteElementString("link", baseUrl);
            
            foreach(var item in model.UltimasPublicaciones)
            {
                TextWriter.WriteStartElement("item");
                TextWriter.WriteElementString("title", item.Titulo);
                TextWriter.WriteElementString("description", item.Descripcion);
                TextWriter.WriteElementString("link", item.Url);
                foreach(var tag in item.Tags){
                    TextWriter.WriteElementString("category", tag);
                }
                TextWriter.WriteEndElement();
            }
            foreach(var item in model.UltimosVideos)
            {
                TextWriter.WriteStartElement("item");
                TextWriter.WriteElementString("title", item.Titulo);
                TextWriter.WriteElementString("description", item.Descripcion);
                TextWriter.WriteElementString("link", item.Url);
                foreach(var tag in item.Tags){
                    TextWriter.WriteElementString("category", tag);
                }
                TextWriter.WriteEndElement();
            }
            TextWriter.WriteEndElement();
            TextWriter.WriteEndElement();
            TextWriter.WriteEndDocument();
            TextWriter.Flush();
            stream.Position=0;
            using(var streamReader = new System.IO.StreamReader(stream))
            {
                
                return Content(streamReader.ReadToEnd(), "application/rss+xml");    
            }
            }
            
            
        }       
    }
}