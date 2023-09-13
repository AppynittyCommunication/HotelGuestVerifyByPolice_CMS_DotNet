using HotelGuestVerifyByPolice_CMS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using HotelGuestVerifyByPolice_CMS.Models.APIModels;
using Newtonsoft.Json;
//using System.Dynamic;
//using System.Drawing.Printing;
//using Microsoft.AspNetCore.Http;
//using System.Web;
//using AspNetCore;

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

        [HttpPost]
        public ActionResult HotelLogout()
        {
            _contx.HttpContext.Session.SetString("hotelRegNo", "");
            _contx.HttpContext.Session.SetString("hotelName", "");
            return Redirect("/Account/HotelLogin");
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

        public async Task<IActionResult> VerifyMoNo(string mobileno)
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("mobileno", mobileno);
            HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "Account/VerifyMobileNo", null);

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

        public async Task<IActionResult> CheckDepartUsername(string dusername)
        {

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("userId", dusername);
            HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "Account/DepartUsernameExist", null);

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
                //string host = Dns.GetHostName();

                //// Getting ip address using host name
                //IPHostEntry ip = Dns.GetHostEntry(host);
                //string hname = ip.HostName.ToString();
                //string ipAdd = (ip.AddressList[3].ToString());

                string userIpAddress = _contx.HttpContext.Connection.RemoteIpAddress.ToString();
                string ipAdd = HttpContext.Connection.RemoteIpAddress.ToString();
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
                hotelRegBody.isMobileVerify = model.isMobileVerify;
                hotelRegBody.diviceIp = ipAdd;
                
                



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
                    var data = dynamicobject.data;

                    string hotelregno = data[0].hotelRegNo.ToString();
                    string hotelname = data[0].hotelName.ToString();

                   
                    if (status == "success" && otp == true)
                    {
                        _contx.HttpContext.Session.SetString("husername", model.UserName);
                        _contx.HttpContext.Session.SetString("otp", model.Password);

                        return RedirectToAction("SetHotelPassUsingOTP", "Account");
                    }
                    else if(status == "success" && otp != true)
                    {
                        _contx.HttpContext.Session.SetString("hotelRegNo", hotelregno);
                        _contx.HttpContext.Session.SetString("hotelName", hotelname);
                        return RedirectToAction("Index", "HotelHome", new { area = "HotelDashboard" });
                    }
                    //TempData["message"] = message;
                    //return RedirectToAction("HotelRegistrationSuccess", "Account", new { msg = message });
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DepartmentLogin(DepartLogin model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                DepartLoginBody departLoginBody = new();
                departLoginBody.dUsername = model.userName;
                departLoginBody.dPassword = model.password;

                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "Account/DeptLogin", departLoginBody);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);

                    var code = (int)response.StatusCode;
                    var status = dynamicobject.status.ToString();
                    var otp = dynamicobject.otp;
                    var message = dynamicobject.message.ToString();
                    var data = dynamicobject.data;

                    string hotelregno = data[0].policeId.ToString();
                    string hotelname = data[0].userId.ToString();


                    if (status == "success" && otp == true)
                    {
                        _contx.HttpContext.Session.SetString("dusername", model.userName);
                        _contx.HttpContext.Session.SetString("dotp", model.password);

                        return RedirectToAction("SetDepartPassUsingOTP", "Account");
                    }
                    else if (status == "success" && otp != true)
                    {
                        _contx.HttpContext.Session.SetString("hotelRegNo", hotelregno);
                        _contx.HttpContext.Session.SetString("hotelName", hotelname);
                        return RedirectToAction("Index", "DepartmentHome", new { area = "Department" });
                    }
                 
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
        public ActionResult SetDepartPassUsingOTP()
        {
            return View();
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ForgetPassword(ForgetHotelPass model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                ForgetHotelPassBody FHPB = new();

                FHPB.otpstatus = model.otpstatus;
                FHPB.hUserId = model.username;
                FHPB.hPassword = model.pass;

                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "Account/ForgetHotelPassword", FHPB);

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

        [HttpPost]
        public async Task<ActionResult> SetDepartPassUsingOTP(SetDepartPassUsingOTP model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                SetHotelPassUsingOTPBody tn = new();

                tn.hUsername = _contx.HttpContext.Session.GetString("dusername");
                tn.otp = _contx.HttpContext.Session.GetString("dotp");
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
        public async Task<IActionResult> CheckHotelUsername(string username, string mobileno)
        {

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("username", username);
            _httpClient.DefaultRequestHeaders.Add("mobileno", mobileno);
            HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "Account/CheckHotelUsername", null);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);
                var otp = dynamicobject.otp.ToString();
                ViewBag.userid = username;
                //TempData.Keep("userid");
                ViewBag.otp = otp;

                return Json(responseString);
            }
            else
            {
                return Json("");
            }

        }

        public ActionResult DepartmentRegistration()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DepartmentRegistration(DepartmentReg model)
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
                //string host = Dns.GetHostName();

                //// Getting ip address using host name
                //IPHostEntry ip = Dns.GetHostEntry(host);
                //string hname = ip.HostName.ToString();
                //string ipAdd = (ip.AddressList[3].ToString());

                string userIpAddress = _contx.HttpContext.Connection.RemoteIpAddress.ToString();
                string ipAdd = HttpContext.Connection.RemoteIpAddress.ToString();
                //------------Get Ip End---------------------
                DepartmentRegBody deptRegBody = new();

                deptRegBody.userType = model.userType.ToString();
                deptRegBody.email = model.email;
                deptRegBody.mobile = model.mobile;
                deptRegBody.userId = model.userId;
                deptRegBody.lat = model.lat;
                deptRegBody._long = model._long;
                deptRegBody.stateId = model.stateId;
                deptRegBody.distId = model.distId;
                deptRegBody.cityId = model.cityId;
                deptRegBody.stationCode = model.stationCode;
                //deptRegBody.password = model.password;
                deptRegBody.isMobileVerify = model.isMobileVerify;
                deptRegBody.policeName = model.firstName + " " + model.lastName;
                deptRegBody.diviceIp = ipAdd;
                
                




                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "Account/PoliceRegistration", deptRegBody);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);

                    var code = (int)response.StatusCode;
                    var status = dynamicobject.status.ToString();
                    var message = dynamicobject.message.ToString();


                    //TempData["message"] = message;
                    return RedirectToAction("DepartmentRegistrationSuccess", "Account", new { msg = message });
                }
                else
                {
                    return View();
                }
            }


            //return View();
        }
        public ActionResult DepartmentRegistrationSuccess(string msg)
        {
            ViewBag.msg = msg;
            return View();
        }
    }
}
