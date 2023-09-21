﻿using HotelGuestVerifyByPolice_CMS.Areas.Department.Models;
using HotelGuestVerifyByPolice_CMS.Models.APIModels;
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
            DeptDashboardRes deptDashboardRes = new ();
            deptDashboardRes.hotelLocOnDashboards = new();
            deptDashboardRes.hotelListDetailsForDashboards = new();
            deptDashboardRes.hotelGuestDetails_DeptDashes = new();



            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("userID", departuser);
            HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "Department/DeptDashboard", null);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);

                deptDashboardRes.code = dynamicobject.code;
                deptDashboardRes.status = dynamicobject.status;
                deptDashboardRes.message = dynamicobject.message;
                deptDashboardRes.stationName = dynamicobject.stationName;

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
                foreach(var hld in dynamicobject.hotelListDetailsForDashboards) {

                    deptDashboardRes.hotelListDetailsForDashboards.Add(new HotelListDetailsForDashboard
                    {
                        stationName = hld.stationName,
                        hotelCount = hld.hotelCount,
                        totalCheckIn = hld.totalCheckIn,
                        todaysCheckIn = hld.todaysCheckIn,
                        todaysCheckOut = hld.todaysCheckOut,

                    });
                }

                foreach(var hgd in dynamicobject.hotelGuestDetails_DeptDashes)
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
                        total_Adult = hgd.total_Adult,
                        total_Child = hgd.total_Child,
                        hotelName = hgd.hotelName,
                        checkInDate = hgd.checkInDate,
                    });
                }


                ViewBag.departuser = departuser;

                return View(deptDashboardRes);
            }

            if (string.IsNullOrEmpty(departuser))
            {
                // return RedirectToAction("HotelLogin", "Account");
                return Redirect("/Account/DepartmentLogin");
            }
            return View();
        }
    }
}
