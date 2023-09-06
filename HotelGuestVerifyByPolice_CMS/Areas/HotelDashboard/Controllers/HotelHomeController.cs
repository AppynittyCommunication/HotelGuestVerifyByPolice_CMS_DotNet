using Microsoft.AspNetCore.Mvc;

namespace HotelGuestVerifyByPolice_CMS.Areas.HotelDashboard.Controllers
{
    public class HotelHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
