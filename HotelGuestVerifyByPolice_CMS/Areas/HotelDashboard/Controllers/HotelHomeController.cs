using HotelGuestVerifyByPolice_CMS.Areas.HotelDashboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelGuestVerifyByPolice_CMS.Areas.HotelDashboard.Controllers
{
    [Area("HotelDashboard")]
    public class HotelHomeController : Controller
    {
        public IActionResult Index()
        {
            Location[] salesLocation = new Location[] {
                new Location
                {
                    ID = 1,
                    Address = "A Road",
                    City = "Pune"
                },
                new Location
                {
                    ID = 2,
                    Address = "B Road",
                    City = "London"
                },
                new Location
                {
                    ID = 3,
                    Address = "K Road",
                    City = "New York"
                },
            };
            return View(salesLocation);
            //return View();
        }
    }
}
