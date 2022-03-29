using API.Models;
using API.ViewModel;
using Client.Base;
using Client.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;

        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));
        }

        public async Task<List<EmployeeProfileVM>> GetAllProfile()
        {
            /// isi codingan kalian disini
            List<EmployeeProfileVM> entities = new List<EmployeeProfileVM>();

            using (var response = await httpClient.GetAsync(address.link + request + "master/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<EmployeeProfileVM>>(apiResponse);
            }

            return entities;
        }

        public HttpStatusCode Register(RegisterVM entity)
        {
            string requestAcc = "accounts/";
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + requestAcc + "register/", content).Result;
            return result.StatusCode;
        }

        public HttpStatusCode Update(Employee entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(address.link + request, content).Result;
            return result.StatusCode;
        }
    }
}