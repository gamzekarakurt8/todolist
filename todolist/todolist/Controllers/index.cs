using Microsoft.AspNetCore.Mvc;

namespace todolist.Controllers
{
    public class index : Controller
    {
        public ActionResult Giris()
        {
            return View();
        }
    }
}
