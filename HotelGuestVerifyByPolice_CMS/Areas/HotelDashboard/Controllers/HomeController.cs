using Microsoft.AspNetCore.Mvc;

namespace HotelGuestVerifyByPolice_CMS.Areas.HotelDashboard.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
