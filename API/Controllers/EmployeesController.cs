using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository _empRepo;

        // Constructor
        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _empRepo = employeeRepository;
        }

        // Get All MasterEMployee
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
    }
}
