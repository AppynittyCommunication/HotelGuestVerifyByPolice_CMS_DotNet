using HotelGuestVerifyByPolice_CMS.Areas.Department.Models;
using HotelGuestVerifyByPolice_CMS.Areas.HotelDashboard.Models;
using HotelGuestVerifyByPolice_CMS.Models;
using HotelGuestVerifyByPolice_CMS.Models.APIModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text.Json.Nodes;

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
                return View();
            }
           
        }

        public async Task<ActionResult> HotelGuestReg([FromBody] HotelGuestRegistration model)
        {
            HotelGuestRegBody HRegBody = new();
            List<AddOnGuestSource> addguestlist = new List<AddOnGuestSource>();
            List<AddOnGuest> addguestdata = new List<AddOnGuest>();
            HRegBody.hotelRegNo = model.hotelRegNo;
            HRegBody.guestName = model.guestName;
            HRegBody.guestType = model.guestType;
            HRegBody.gender = model.gender;
            HRegBody.email = model.email;
            HRegBody.country = model.country;
            HRegBody.state = model.state;
            HRegBody.city = model.city;
            HRegBody.numberOfGuest = model.numberOfGuest;
            HRegBody.age = model.age;
            HRegBody.mobile = model.mobile;
            HRegBody.visitPurpose = model.visitPurpose;
            HRegBody.roomType = model.roomType;
            HRegBody.roomNo = model.roomNo;
            HRegBody.comingFrom = model.comingFrom;
            HRegBody.guestIdType = model.guestIdType;
            HRegBody.guestIDProof = model.guestIDProof;
            HRegBody.guestPhoto  = model.guestPhoto;
            HRegBody.paymentMode = model.paymentMode;

            if(model.addOnGuest != null)
            {
                foreach(AddOnGuestSource  item in addguestlist)
                {
                    AddOnGuest addguestitem = new AddOnGuest
                    {
                        guestName = item.guestName,
                        age = item.age,
                        mobile = item.mobile,
                        relationWithGuest = item.relationWithGuest,
                        guestType = item.guestType,
                        gender = item.gender,
                        email = item.email,
                        country = item.country,
                        state = item.state,
                        city = item.city,
                        comingFrom = item.comingFrom,
                        guestIdType = item.guestIdType,
                        guestIDProof = item.guestIDProof,
                        guestPhoto = item.guestPhoto,

                    };
                    addguestdata.Add(addguestitem);
                    HRegBody.addOnGuest = addguestdata;
                }
            }
          


            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "Hotel/HotelGuestRegistration", HRegBody);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);
                var code = (int)response.StatusCode;
                var status = dynamicobject.status.ToString();
                var message = dynamicobject.message.ToString();

                if (status == "success")
                {
                    ViewBag.msg = message;
                    return View();
                }
                else
                {
                    ViewBag.msg = message;
                    return View();
                }
            }
            else
            {
                return View();
            }

               
        }


        public ActionResult SearchHotel()
        {
            //string departauthpin = _contx.HttpContext.Session.GetString("setdepartauthpin");
            return View();
        }

    }
}
