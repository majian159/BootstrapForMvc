using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(new StudentModel
            {
                Id = 1,
                Name = "小明",
                Age = 20,
                Class = "一班",
                Enable = true
            });
        }
    }
}