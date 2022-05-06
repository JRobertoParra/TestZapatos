using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApi_Zapateria.Controllers
{
    public class ZapateriaController : Controller
    {
        // GET: Zapateria
        public ActionResult Index()
        {
            return View();
        }
    }
}