using HotelGuestVerifyByPolice_CMS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using HotelGuestVerifyByPolice_CMS.Models.APIModels;
using Newtonsoft.Json;
using System.Dynamic;
using System.Drawing.Printing;

namespace HotelGuestVerifyByPolice_CMS.Controllers
{

    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _contx;
        private readonly string _myApi;
        private readonly HttpClient _httpClient;
        private object msg;

        public AccountController(IConfiguration configuration, IHttpContextAccessor contx)
		{
			_httpClient = new HttpClient();
            _myApi = configuration["MyApi:API"];
            Uri baseUri = new Uri(_myApi);
            _httpClient.BaseAddress = baseUri;
            _contx = contx;
        }
		public IActionResult Index()
        {
            return View();
        }

        public ActionResult HotelRegistration()
        {
            return View();
        }

        public ActionResult HotelLogin()
        {
            return View();
        }
        public ActionResult DepartmentLogin()
        {
            return View();
        }

        public async Task<IActionResult> CheckHotelRegNo(string hotelregno)
        {
            
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("hotelRegNumber", hotelregno);
            HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "Account/HotelRegExist", null);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                return Json(responseString);
            }
            else
            {
                return Json("");
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> HotelRegistrationAsync(HotelReg model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                List<HotelRegResult> objResult = new();
                //------------Get Ip Start---------------------

                // Getting host name
                string host = Dns.GetHostName();

                //// Getting ip address using host name
                IPHostEntry ip = Dns.GetHostEntry(host);
                string hname = ip.HostName.ToString();
                string ipAdd = (ip.AddressList[3].ToString());
                //------------Get Ip End---------------------
                HotelRegBody hotelRegBody = new();

                hotelRegBody.firstName = model.firstName;
                hotelRegBody.lastName = model.lastName;
                hotelRegBody.email = model.email;
                hotelRegBody.address = model.address;
                hotelRegBody.mobile = model.mobile;
                hotelRegBody.pinCode = model.pinCode;
                hotelRegBody.hotelName = model.hotelName;
                hotelRegBody.hotelRegNo = model.hotelRegNo;
                hotelRegBody.userId = model.userId;
                hotelRegBody.lat = model.lat;
                hotelRegBody._long = model._long;
                hotelRegBody.stateId = model.stateId;
                hotelRegBody.distId = model.distId;
                hotelRegBody.cityId = model.cityId;
                hotelRegBody.stationCode = model.stationCode;
                if (model.diviceIp == null)
                {
                    hotelRegBody.diviceIp = ipAdd;
                }
                else
                {
                    hotelRegBody.diviceIp = model.diviceIp;
                }




                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "Account/HotelRegistration", hotelRegBody);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);

                    var code = (int)response.StatusCode;
                    var status = dynamicobject.status.ToString();
                    var message = dynamicobject.message.ToString();


                    //TempData["message"] = message;
                    return RedirectToAction("HotelRegistrationSuccess", "Account", new { msg = message });
                }
                else
                {
                    return View();
                }
            }
            
            
			//return View();
		}

        [HttpPost]
        public async Task<ActionResult> HotelLogin(HotelLogin model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                HotelLoginBody hotelLoginBody = new();
                hotelLoginBody.hUsername = model.UserName;
                hotelLoginBody.hPassword = model.Password;

                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "Account/HotelLogin", hotelLoginBody);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);

                    var code = (int)response.StatusCode;
                    var status = dynamicobject.status.ToString();
                    var otp = dynamicobject.otp;
                    var message = dynamicobject.message.ToString();

                    if(status == "success" && otp == true)
                    {
                        _contx.HttpContext.Session.SetString("husername", model.UserName);
                        _contx.HttpContext.Session.SetString("otp", model.Password);

                        return RedirectToAction("SetHotelPassUsingOTP", "Account");
                    }
                    else if(status == "success" && otp != true)
                    {
                        return RedirectToAction("Index", "HotelHome", new { area = "HotelDashboard" });
                    }
                    //TempData["message"] = message;
                    return RedirectToAction("HotelRegistrationSuccess", "Account", new { msg = message });
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
        public ActionResult HotelRegistrationSuccess(string msg)
        {
            ViewBag.msg = msg;
            return View();
        }

        public ActionResult SetHotelPassUsingOTP()
        {
            return View();
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SetHotelPassUsingOTP(SetHotelPassUsingOTP model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                SetHotelPassUsingOTPBody tn = new();

                tn.hUsername = _contx.HttpContext.Session.GetString("husername");
                tn.otp = _contx.HttpContext.Session.GetString("otp");
                tn.pass = model.pass;

                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "Account/ResetHotelPasswordUsingOTP", tn);

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
                    }
                    else if (status == "error")
                    {
                        ViewBag.msg = message;
                    }
                    
                }
                else
                {
                    return View(model);
                }
                return View();
            }
        }
    }
}
