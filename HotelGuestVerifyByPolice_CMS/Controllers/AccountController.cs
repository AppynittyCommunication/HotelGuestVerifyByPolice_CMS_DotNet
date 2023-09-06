using HotelGuestVerifyByPolice_CMS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;

namespace HotelGuestVerifyByPolice_CMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly string _myApi;
        private readonly HttpClient _httpClient;

		public AccountController(IConfiguration configuration)
		{
			_httpClient = new HttpClient();
            _myApi = configuration["MyApi:API"];
            Uri baseUri = new Uri(_myApi);
            _httpClient.BaseAddress = baseUri;
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

        [HttpPost]
        public async Task<ActionResult> HotelRegistrationAsync(HotelReg model)
        {
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			HttpResponseMessage rs = await _httpClient.PostAsync(_httpClient.BaseAddress + "SelectList/GetStates", null);

			return View();
		}
    }
}
