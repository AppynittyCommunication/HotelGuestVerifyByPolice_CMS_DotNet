using HotelGuestVerifyByPolice_CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
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

        public async Task<IActionResult> CountryList()
        {
            //List<State>? states = new();
            List<CountryData> countryList = new();

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "SelectList/GetCountry");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);
                Country stateResponse = JsonConvert.DeserializeObject<Country>(responseString);

                var code = (int)response.StatusCode;
                var status = dynamicobject.status.ToString();
                var message = dynamicobject.message.ToString();
                var data = dynamicobject.data;
                if (status == "success")
                {
                    countryList = stateResponse.Data;

                    return Json(countryList);
                }




            }

            return Json(countryList);

        }
        public async Task<IActionResult> CountryWiseStateList(string cCode)
        {
            List<StateData> stateList = new();

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("countryCode", cCode);
            HttpResponseMessage rs = await _httpClient.GetAsync(_httpClient.BaseAddress + "SelectList/GetCountryWiseStates");

            if (rs.IsSuccessStatusCode)
            {
                var responseString = await rs.Content.ReadAsStringAsync();
                var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);
                State distResponse = JsonConvert.DeserializeObject<State>(responseString);

                var code = (int)rs.StatusCode;
                var status = dynamicobject.status.ToString();
                var message = dynamicobject.message.ToString();
                var data = dynamicobject.data;
                if (status == "success")
                {
                    stateList = distResponse.Data;

                    return Json(stateList);
                }


            }

            return Json(stateList);

        }
        public async Task<IActionResult> StateList()
        {
            //List<State>? states = new();
            List<StateData> stateList = new();

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "SelectList/GetStates");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);
                State stateResponse = JsonConvert.DeserializeObject<State>(responseString);

                var code = (int)response.StatusCode;
                var status = dynamicobject.status.ToString();
                var message = dynamicobject.message.ToString();
                var data = dynamicobject.data;
                if (status == "success")
                {
                     stateList = stateResponse.Data;

                    return Json(stateList);
                }
                
                


            }
         
           return Json(stateList);

        }

        public async Task<IActionResult> deptTypeList()
        {
            List<DeptData>? dtl = new();

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage rs = await _httpClient.GetAsync(_httpClient.BaseAddress + "SelectList/GetDepartmentType");

            if (rs.IsSuccessStatusCode)
            {
                var responseString = await rs.Content.ReadAsStringAsync();
                var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);
                DeptTypeList deprtResponse = JsonConvert.DeserializeObject<DeptTypeList>(responseString);

                var code = (int)rs.StatusCode;
                var status = dynamicobject.status.ToString();
                var message = dynamicobject.message.ToString();
                var data = dynamicobject.data;
                if (status == "success")
                {
                    dtl = deprtResponse.Data;

                    return Json(dtl);
                }


            }

            return Json(dtl);

        }

        public async Task<IActionResult> DistrictList(string stateID)
        {
            List<DistData>? districtList = new();

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("stateID", stateID);
            HttpResponseMessage rs = await _httpClient.GetAsync(_httpClient.BaseAddress + "SelectList/GetDistrict");

            if (rs.IsSuccessStatusCode)
            {
                var responseString = await rs.Content.ReadAsStringAsync();
                var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);
                DistictList distResponse = JsonConvert.DeserializeObject<DistictList>(responseString);

                var code = (int)rs.StatusCode;
                var status = dynamicobject.status.ToString();
                var message = dynamicobject.message.ToString();
                var data = dynamicobject.data;
                if (status == "success")
                {
                    districtList = distResponse.Data;

                    return Json(districtList);
                }


            }

            return Json(districtList);

        }

        public async Task<IActionResult> CityList(string stateID, string distID)
        {
            List<CityData>? cityList = new();

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("stateID", stateID);
            _httpClient.DefaultRequestHeaders.Add("distID", distID);
            HttpResponseMessage rs = await _httpClient.GetAsync(_httpClient.BaseAddress + "SelectList/GetCities");

            if (rs.IsSuccessStatusCode)
            {
                var responseString = await rs.Content.ReadAsStringAsync();
                var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);
                CityList cityResponse = JsonConvert.DeserializeObject<CityList>(responseString);

                var code = (int)rs.StatusCode;
                var status = dynamicobject.status.ToString();
                var message = dynamicobject.message.ToString();
                var data = dynamicobject.data;
                if (status == "success")
                {
                    cityList = cityResponse.Data;

                    return Json(cityList);
                }


            }

            return Json(cityList);

        }

        public async Task<IActionResult> PoliceStationList(string stateID, string distID, string cityID)
        {
            List<PSList>? psList = new();

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("stateID", stateID);
            _httpClient.DefaultRequestHeaders.Add("distID", distID);
            _httpClient.DefaultRequestHeaders.Add("cityID", cityID);
            HttpResponseMessage rs = await _httpClient.GetAsync(_httpClient.BaseAddress + "SelectList/GetPoliceStation");

            if (rs.IsSuccessStatusCode)
            {
                var responseString = await rs.Content.ReadAsStringAsync();
                var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);
                PoliceStationList cityResponse = JsonConvert.DeserializeObject<PoliceStationList>(responseString);

                var code = (int)rs.StatusCode;
                var status = dynamicobject.status.ToString();
                var message = dynamicobject.message.ToString();
                var data = dynamicobject.data;
                if (status == "success")
                {
                    psList = cityResponse.Data;

                    return Json(psList);
                }



            }

            return Json(psList);

        }

        public async Task<IActionResult> VisitPurposeList()
        {
            //List<State>? states = new();
            List<VisitPurposeData> vpList = new();

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "Hotel/SelectVisitPurpose");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);
                VisitPurpose purposeResponse = JsonConvert.DeserializeObject<VisitPurpose>(responseString);

                var code = (int)response.StatusCode;
                var status = dynamicobject.status.ToString();
                var message = dynamicobject.message.ToString();
                var data = dynamicobject.data;
                if (status == "success")
                {
                    vpList = purposeResponse.Data;

                    return Json(vpList);
                }




            }

            return Json(vpList);

        }

        public async Task<IActionResult> IdProofTypeList()
        {
            //List<State>? states = new();
            List<IdProofTypeData> idProofList = new();

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "Hotel/SelectSelectIDType");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);
                IdProofType purposeResponse = JsonConvert.DeserializeObject<IdProofType>(responseString);

                var code = (int)response.StatusCode;
                var status = dynamicobject.status.ToString();
                var message = dynamicobject.message.ToString();
                var data = dynamicobject.data;
                if (status == "success")
                {
                    idProofList = purposeResponse.Data;

                    return Json(idProofList);
                }




            }

            return Json(idProofList);

        }

        public async Task<IActionResult> RelationTypeList()
        {
            //List<State>? states = new();
            List<IdProofTypeData> idProofList = new();

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "Hotel/SelectRelation");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);
                IdProofType purposeResponse = JsonConvert.DeserializeObject<IdProofType>(responseString);

                var code = (int)response.StatusCode;
                var status = dynamicobject.status.ToString();
                var message = dynamicobject.message.ToString();
                var data = dynamicobject.data;
                if (status == "success")
                {
                    idProofList = purposeResponse.Data;

                    return Json(idProofList);
                }

            }

            return Json(idProofList);

        }
    }
}
