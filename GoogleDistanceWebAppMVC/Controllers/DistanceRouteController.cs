using GoogleDistanceWebAppMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GoogleDistanceWebAppMVC.Controllers
{
    public class DistanceRouteController : Controller
    {
        // GET: DistanceRoute
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(DistanceRoute route)
        {
            if (!ModelState.IsValid)
                return View();

            Task.Run(() => route.FindDistance());

            while (route.Distance == null || route.Duration == null) { }

            return RedirectToAction("DistanceResults", route);
        }

        public ActionResult DistanceResults(DistanceRoute route)
        {
            return View(route);
        }
    }
}