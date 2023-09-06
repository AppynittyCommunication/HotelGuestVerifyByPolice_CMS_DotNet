using Microsoft.AspNetCore.Mvc;

namespace HotelGuestVerifyByPolice_CMS.Areas.Department.Controllers
{
    public class DepartmentHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
