using HotelGuestVerifyByPolice_CMS.Areas.HotelDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace HotelGuestVerifyByPolice_CMS.Areas.HotelDashboard.Controllers
{
    [Area("HotelDashboard")]
    public class HotelHomeController : Controller
    {
        private readonly IHttpContextAccessor _contx;
        private readonly string _myApi;
        private readonly HttpClient _httpClient;
        private object msg;

        public HotelHomeController(IConfiguration configuration, IHttpContextAccessor contx)
        {
            _httpClient = new HttpClient();
            _myApi = configuration["MyApi:API"];
            Uri baseUri = new Uri(_myApi);
            _httpClient.BaseAddress = baseUri;
            _contx = contx;
        }
       
        public IActionResult Index()
        {
            string hotelregno = _contx.HttpContext.Session.GetString("hotelRegNo");
            string hotelname = _contx.HttpContext.Session.GetString("hotelName");

            ViewBag.hotelregno = hotelregno;
            ViewBag.hotelname = hotelname;
            if (string.IsNullOrEmpty(hotelregno))
            {
               // return RedirectToAction("HotelLogin", "Account");
               return Redirect("/Account/HotelLogin");
            }
            else
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
            }
            
            //return View();
        }

        public IActionResult ImageCapture()
        {
            return View();
        }
    }
}
