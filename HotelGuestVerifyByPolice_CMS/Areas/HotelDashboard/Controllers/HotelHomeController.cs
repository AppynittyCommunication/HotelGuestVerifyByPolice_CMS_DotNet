using HotelGuestVerifyByPolice_CMS.Areas.Department.Models;
using HotelGuestVerifyByPolice_CMS.Areas.HotelDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

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

        public async Task<IActionResult> IndexAsync()
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
                HotelDashboardRes hotelDashRes = new();
                HotelDashResData hotelDashResData = new();
                hotelDashResData.guestDetails = new();
                hotelDashResData.monthlyCheckInOutCounts = new();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("hotelRegNo", hotelregno);
                HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "Hotel/GetGuestCheckInOutInfo", null);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);

                    hotelDashRes.code = dynamicobject.code;
                    hotelDashRes.status = dynamicobject.status;
                    hotelDashRes.message = dynamicobject.message;

                    if(hotelDashRes.status == "success")
                    {
                        hotelDashResData.totalGuest = dynamicobject.data[0].totalGuest;
                        hotelDashResData.todaysCheckIn = dynamicobject.data[0].todaysCheckIn;
                        hotelDashResData.todaysCheckOut = dynamicobject.data[0].todaysCheckOut;
                        foreach (var i in dynamicobject.data[0].guestDetails)
                        {
                            hotelDashResData.guestDetails.Add(new GuestDetail
                            {
                              guestName = i.guestName,
                              guestPhoto = i.guestPhoto,
                              reservation = i.reservation,
                              mobile = i.mobile,
                              state = i.state,
                              country = i.country,
                              checkInDate = i.checkInDate,
                            });
                        }
                        foreach(var c in dynamicobject.data[0].monthlyCheckInOutCounts)
                        {
                            hotelDashResData.monthlyCheckInOutCounts.Add(new MonthlyCheckInOutCount
                            {
                                month = c.month,
                                monthName = c.monthName,
                                checkInCount = c.checkInCount,
                                checkOutCount = c.checkOutCount,
                            });
                        }
                        return View(hotelDashResData);
                    }
                    return View();
                }
                return View();
            }
            
            //return View();
        }

        public IActionResult ImageCapture()
        {
            return View();
        }
        public IActionResult CheckIn()
        {
            return View();
        }
        public IActionResult CheckInS()
        {
            return View();
        }
    }
}
