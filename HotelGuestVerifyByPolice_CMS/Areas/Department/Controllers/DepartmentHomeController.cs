using HotelGuestVerifyByPolice_CMS.Areas.Department.Models;
using HotelGuestVerifyByPolice_CMS.Models.APIModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HotelGuestVerifyByPolice_CMS.Areas.Department.Controllers
{
    [Area("Department")]
    public class DepartmentHomeController : Controller
    {
        private readonly IHttpContextAccessor _contx;
        private readonly string _myApi;
        private readonly HttpClient _httpClient;

        public DepartmentHomeController(IConfiguration configuration, IHttpContextAccessor contx)
        {
            _httpClient = new HttpClient();
            _myApi = configuration["MyApi:API"];
            Uri baseUri = new Uri(_myApi);
            _httpClient.BaseAddress = baseUri;
            _contx = contx;
        }
        public async Task<IActionResult> IndexAsync()
        {
            //string hotelregno = _contx.HttpContext.Session.GetString("hotelRegNo");
            string departuser = _contx.HttpContext.Session.GetString("departUser");
            string departtype = _contx.HttpContext.Session.GetString("dusertype");
           

            if (string.IsNullOrEmpty(departuser))
            {
                // return RedirectToAction("HotelLogin", "Account");
                return Redirect("/Account/DepartmentLogin");
            }
            else
            {
                DeptDashboardRes deptDashboardRes = new();
                deptDashboardRes.hotelLocOnDashboards = new();
                deptDashboardRes.hotelListDetailsForDashboards = new();
                deptDashboardRes.hotelGuestDetails_DeptDashes = new();
                deptDashboardRes.hotelGuestDetails_DeptDashes2 = new();



                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("userID", departuser);
                HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "Department/DeptDashboard");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);

                    deptDashboardRes.code = dynamicobject.code;
                    deptDashboardRes.status = dynamicobject.status;
                    deptDashboardRes.message = dynamicobject.message;
                    deptDashboardRes.stationName = dynamicobject.stationName;

                    if(dynamicobject.hotelLocOnDashboard != null)
                    {
                        foreach (var i in dynamicobject.hotelLocOnDashboard)
                        {
                            deptDashboardRes.hotelLocOnDashboards.Add(new HotelLocOnDashboard
                            {
                                hotelName = i.hotelName,
                                Mobile = i.mobile,
                                Address = i.address,
                                lat = i.lat,
                                _long = i._long,
                            });

                        }
                    }
              
                    if(dynamicobject.hotelListDetailsForDashboards != null)
                    {
                        foreach (var hld in dynamicobject.hotelListDetailsForDashboards)
                        {

                            deptDashboardRes.hotelListDetailsForDashboards.Add(new HotelListDetailsForDashboard
                            {
                                stationName = hld.stationName,
                                hotelCount = hld.hotelCount,
                                totalCheckIn = hld.totalCheckIn,
                                todaysCheckIn = hld.todaysCheckIn,
                                todaysCheckOut = hld.todaysCheckOut,

                            });
                        }
                    }
                  
                    if(dynamicobject.hotelGuestDetails_DeptDashes != null)
                    {
                        foreach (var hgd in dynamicobject.hotelGuestDetails_DeptDashes)
                        {
                            deptDashboardRes.hotelGuestDetails_DeptDashes.Add(new HotelGuestDetails_DeptDash1
                            {
                                roomBookingID = hgd.roomBookingID,
                                guestName = hgd.guestName,
                                guestPhoto = hgd.guestPhoto,
                                age = hgd.age,
                                city = hgd.city,
                                visitPurpose = hgd.visitPurpose,
                                comingFrom = hgd.comingFrom,
                                reservation = hgd.reservation,
                                hotelName = hgd.hotelName,
                                checkInDate = hgd.checkInDate,
                            });
                        }
                    }
                  if(dynamicobject.hotelGuestDetails_DeptDash2 != null)
                    {
                        foreach (var hgdt in dynamicobject.hotelGuestDetails_DeptDash2)
                        {
                            deptDashboardRes.hotelGuestDetails_DeptDashes2.Add(new HotelGuestDetails_DeptDash2
                            {
                                hotelName = hgdt.hotelName,
                                guestName = hgdt.guestName,
                                age = hgdt.age,
                                visitPurpose = hgdt.visitPurpose,
                                comingFrom = hgdt.comingFrom,
                                reservation = hgdt.reservation,
                                mobile = hgdt.mobile,
                                city = hgdt.city,
                                checkInDate = hgdt.checkInDate,
                            });
                        }
                    }
                 

                    ViewBag.departuser = departuser;

                    return View(deptDashboardRes);
                }
                else
                {
                    return Redirect("/Account/DepartmentLogin");
                   
                }
              
            }
          
        }

        public ActionResult SearchHotel()
        {
            string departuser = _contx.HttpContext.Session.GetString("departUser");
            string departtype = _contx.HttpContext.Session.GetString("dusertype");
            string dstateID = _contx.HttpContext.Session.GetString("dstateid");
            string ddistID = _contx.HttpContext.Session.GetString("ddistid");
            string dcityID = _contx.HttpContext.Session.GetString("dcityid");
            string dStationCode = _contx.HttpContext.Session.GetString("dstationcode");

            ViewBag.departuser = departuser;
            ViewBag.dType = departtype;
            ViewBag.dStateId = dstateID;
            ViewBag.dDistId = ddistID;
            ViewBag.dCityId = dcityID;
            ViewBag.dSCode = dStationCode;
            if (string.IsNullOrEmpty(departuser))
            {
                // return RedirectToAction("HotelLogin", "Account");
                return Redirect("/Account/DepartmentLogin");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> SearchHotel(String hotelRegNo)
        {
            DeptSearchHotelRes deptSearchHotelRes = new();
            deptSearchHotelRes.hotelTitle = new();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("hotelRegNo", hotelRegNo);
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "Department/SearchHotel");

            if(response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);

                deptSearchHotelRes.code = dynamicobject.code;
                deptSearchHotelRes.status = dynamicobject.status;
                deptSearchHotelRes.message = dynamicobject.message;

                foreach (var i in dynamicobject.hotelLocOnDashboard)
                {
                    deptSearchHotelRes.hotelTitle.Add(new HotelTitle
                    {
                        hotelName = i.hotelName,
                        mobile = i.mobile,
                        address = i.address,
                        city = i.city,
                        policeSation = i.policeSation,
                    });

                }
                return View(deptSearchHotelRes);
            }
            else
            {
                return View();
            }
        }
    }
}
