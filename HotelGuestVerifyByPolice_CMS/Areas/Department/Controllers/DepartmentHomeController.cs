using HotelGuestVerifyByPolice_CMS.Areas.Department.Models;
using HotelGuestVerifyByPolice_CMS.Areas.HotelDashboard.Models;
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

            if (string.IsNullOrEmpty(departuser))
            {
                // return RedirectToAction("HotelLogin", "Account");
                return Redirect("/Account/DepartmentLogin");
            }
            else
            {
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
                    var model = new DeptSearchHotelRes
                    {
                        // Initialize the addOnGuest property as an empty list

                        hotelGuests = new List<HotelGuest>(),
                    };

                    return View(model);


                }
            }
            
        }

     
        public async Task<ActionResult> ShowHotelGuestDetails(string roombookingId)
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("roomBookingID", roombookingId);
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "Department/ShowHotelGuestDetails");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                ShowHotelGuestDetailsRes GuestDetailsResponce = JsonConvert.DeserializeObject<ShowHotelGuestDetailsRes>(responseString);

                var code = GuestDetailsResponce.code;
                var status = GuestDetailsResponce.status;
                var message = GuestDetailsResponce.message;
                var data = GuestDetailsResponce.data;

                if(status == "success")
                {
                    return Json(data);
                }
                else
                {
                    return Json("");
                }

            }
            else
            {
                return Json(response);
            }
        }

        [HttpPost]
        public async Task<ActionResult> SearchHotel(HotelSearchFormModal obj)
        {

            if(obj.hotelRegNo != null)
            {
                string departuser = _contx.HttpContext.Session.GetString("departUser");
                string departtype = _contx.HttpContext.Session.GetString("dusertype");
                string dstateID = _contx.HttpContext.Session.GetString("dstateid");
                string ddistID = _contx.HttpContext.Session.GetString("ddistid");
                string dcityID = _contx.HttpContext.Session.GetString("dcityid");
                string dStationCode = _contx.HttpContext.Session.GetString("dstationcode");

                if (string.IsNullOrEmpty(departuser))
                {
                    // return RedirectToAction("HotelLogin", "Account");
                    return Redirect("/Account/DepartmentLogin");
                }
                else
                {
                    ViewBag.departuser = departuser;
                    ViewBag.dType = departtype;
                    ViewBag.dStateId = dstateID;
                    ViewBag.dDistId = ddistID;
                    ViewBag.dCityId = dcityID;
                    ViewBag.dSCode = dStationCode;

                    DeptSearchHotelRes deptSearchHotelRes = new();
                    deptSearchHotelRes.hotelTitle = new();
                    deptSearchHotelRes.hotelGuests = new();
                    deptSearchHotelRes.lastVisitors = new();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    _httpClient.DefaultRequestHeaders.Add("hotelRegNo", obj.hotelRegNo);
                    HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "Department/SearchHotel");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);

                        deptSearchHotelRes.code = dynamicobject.code;
                        deptSearchHotelRes.status = dynamicobject.status;
                        deptSearchHotelRes.message = dynamicobject.message;

                        if (dynamicobject.hotelTitle != null)
                        {
                            deptSearchHotelRes.hotelTitle.hotelName = dynamicobject.hotelTitle.hotelName;
                            deptSearchHotelRes.hotelTitle.mobile = dynamicobject.hotelTitle.mobile;
                            deptSearchHotelRes.hotelTitle.address = dynamicobject.hotelTitle.address;
                            deptSearchHotelRes.hotelTitle.city = dynamicobject.hotelTitle.city;
                            deptSearchHotelRes.hotelTitle.policeSation = dynamicobject.hotelTitle.policeSation;

                        }
                        if (dynamicobject.hotelGuests != null)
                        {
                            foreach (var h in dynamicobject.hotelGuests)
                            {
                                deptSearchHotelRes.hotelGuests.Add(new HotelGuest
                                {
                                    guestName = h.guestName,
                                    reservation = h.reservation.ToString(),
                                    nightStayed = h.nightStayed.ToString(),
                                    lastVisit = h.lastVisit,
                                    mobile = h.mobile,
                                    city = h.city,
                                    address = h.address,
                                    country = h.country,
                                });
                            }
                        }
                        if (dynamicobject.lastVisitors != null)
                        {
                            deptSearchHotelRes.lastVisitors.guestName = dynamicobject.lastVisitors.guestName;
                            deptSearchHotelRes.lastVisitors.age = dynamicobject.lastVisitors.age.ToString();
                            deptSearchHotelRes.lastVisitors.city = dynamicobject.lastVisitors.city;
                            deptSearchHotelRes.lastVisitors.purpose = dynamicobject.lastVisitors.purpose;
                            deptSearchHotelRes.lastVisitors.commingFrom = dynamicobject.lastVisitors.commingFrom;
                            deptSearchHotelRes.lastVisitors.reservaion = dynamicobject.lastVisitors.reservaion;
                            deptSearchHotelRes.lastVisitors.checkInDate = dynamicobject.lastVisitors.checkInDate;
                            deptSearchHotelRes.lastVisitors.guestPhoto = dynamicobject.lastVisitors.photo;


                        }
                        else
                        {
                            deptSearchHotelRes.lastVisitors = null;
                        }
                        return View(deptSearchHotelRes);
                    }
                    else
                    {
                        return View(obj);
                    }
                }
               
            }
            else
            {
                return View(obj);
            }
            
        }


    }
}
