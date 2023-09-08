using HotelGuestVerifyByPolice_CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace HotelGuestVerifyByPolice_CMS.Controllers
{
    public class StatesController : Controller
    {
        
        private readonly string _myApi;
        private readonly HttpClient _httpClient;
        public StatesController(IConfiguration configuration) {
            _httpClient = new HttpClient();
            _myApi = configuration["MyApi:API"];
            Uri baseUri = new Uri(_myApi);
            _httpClient.BaseAddress = baseUri;
            }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<State>? states = new();
         
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage rs = await _httpClient.PostAsync(_httpClient.BaseAddress + "SelectList/GetStates",null);

            
            if(rs.IsSuccessStatusCode)
            {
                string data = rs.Content.ReadAsStringAsync().Result;
                
                 states = JsonConvert.DeserializeObject<List<State>>(data);
                
               
            }
            return View(states);
        }

        public async Task<IActionResult> StateList()
        {
            List<State>? states = new();

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage rs = await _httpClient.PostAsync(_httpClient.BaseAddress + "SelectList/GetStates", null);

            if (rs.IsSuccessStatusCode)
            {
                string data = rs.Content.ReadAsStringAsync().Result;

                states = JsonConvert.DeserializeObject<List<State>>(data);


            }
         
           return Json(states);

        }

        public async Task<IActionResult> deptTypeList()
        {
            List<DeptTypeList>? dtl = new();

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage rs = await _httpClient.PostAsync(_httpClient.BaseAddress + "SelectList/GetDepartmentType", null);

            if (rs.IsSuccessStatusCode)
            {
                string data = rs.Content.ReadAsStringAsync().Result;

                dtl = JsonConvert.DeserializeObject<List<DeptTypeList>>(data);


            }

            return Json(dtl);

        }

        public async Task<IActionResult> DistrictList(string stateID)
        {
            List<DistictList>? districtList = new();

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("stateID", stateID);
            HttpResponseMessage rs = await _httpClient.PostAsync(_httpClient.BaseAddress + "SelectList/GetDistrict", null);

            if (rs.IsSuccessStatusCode)
            {
                string data = rs.Content.ReadAsStringAsync().Result;

                districtList = JsonConvert.DeserializeObject<List<DistictList>>(data);


            }

            return Json(districtList);

        }

        public async Task<IActionResult> CityList(string stateID, string distID)
        {
            List<CityList>? cityList = new();

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("stateID", stateID);
            _httpClient.DefaultRequestHeaders.Add("distID", distID);
            HttpResponseMessage rs = await _httpClient.PostAsync(_httpClient.BaseAddress + "SelectList/GetCities", null);

            if (rs.IsSuccessStatusCode)
            {
                string data = rs.Content.ReadAsStringAsync().Result;

                cityList = JsonConvert.DeserializeObject<List<CityList>>(data);


            }

            return Json(cityList);

        }

        public async Task<IActionResult> PoliceStationList(string stateID, string distID, string cityID)
        {
            List<PoliceStationList>? psList = new();

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("stateID", stateID);
            _httpClient.DefaultRequestHeaders.Add("distID", distID);
            _httpClient.DefaultRequestHeaders.Add("cityID", cityID);
            HttpResponseMessage rs = await _httpClient.PostAsync(_httpClient.BaseAddress + "SelectList/GetPoliceStation", null);

            if (rs.IsSuccessStatusCode)
            {
                string data = rs.Content.ReadAsStringAsync().Result;

                psList = JsonConvert.DeserializeObject<List<PoliceStationList>>(data);


            }

            return Json(psList);

        }
    }
}
