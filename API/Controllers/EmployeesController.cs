using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository _empRepo;
        public IConfiguration _configration;

        // Constructor
        public EmployeesController(EmployeeRepository employeeRepository, IConfiguration configuration) : base(employeeRepository)
        {
            this._empRepo = employeeRepository;
            this._configration = configuration;
        }

        // Get All MasterEMployee
        //[Authorize(Roles ="Director, Manager")]
        [HttpGet("master")]
        public ActionResult GetAllMasterEmployee()
        {
            try
            {
                var hasilData = _empRepo.MasterEmployee();
                if (hasilData != null )
                {
                    return Ok(_empRepo.MasterEmployee());
                }
                else
                {
                    return NotFound("Data Tidak Ditemukan");
                }
            }
            catch(Exception e)
            {
                return BadRequest($"Error System Get All Master : {e.Message}");
            }
        }

        // Test Cors
        [HttpGet("testcors")]
        public ActionResult TestCors()
        {
            return Ok("Test Cors Berhasil");
        }
    }
}
