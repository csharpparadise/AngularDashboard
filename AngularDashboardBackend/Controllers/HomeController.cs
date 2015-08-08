using System.Threading.Tasks;
using System.Web.Mvc;

namespace AngularDashboardBackend.Controllers
{
    public class HomeController : Controller
    {
        private static Task DashboardTask;

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult StartDelivery()
        {
            return View();
        }

        private Task CreateDeliveryTask()
        {
            return new Task(() =>
            {

            });
        }


    }
}
