using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EscribirCola.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string info)
        {
            var conn = ConfigurationManager.AppSettings["conexioncola"];
            ManejadorCola.Instancia.CrearCola(conn, "incidencias", 1024, 86400);
            Dictionary<String, String> prop = new Dictionary<string, string>()
            {
                { "incidencia",info},
                { "fecha",DateTime.Now.ToLongTimeString()}
            };
            ManejadorCola.Instancia.EnviarMensaje(conn, "incidencias", prop, "Se ha creado una incidencia");
            return View();
        }
    }
}