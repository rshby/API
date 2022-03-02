using API.Context;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeeController(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        //insert data
        [HttpPost]
        public ActionResult Post(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }
                var resultInsert = employeeRepository.Insert(employee);
                if (resultInsert != 0)
                {
                    return Ok("Data Berhasil ditambah");
                }
                else
                {
                    return NotFound("Data Gagal ditambah");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new employee record");
            }
        }

        //show data
        [HttpGet]
        public ActionResult GET()
        {
            return Ok(employeeRepository.GET());
        }

        //show data by NIK
        [HttpGet("{NIK}")]
        public ActionResult GetEmployeeById(string NIK)
        {
            return Ok(employeeRepository.Get(NIK));
        }

        //Update data employee
        [HttpPatch("update/{NIK}")]
        public async Task<ActionResult<Employee>> UpdateEmployeeById(string NIK, [FromBody] JsonPatchDocument patchEntity)
        {
            try
            {
                await employeeRepository.UpdateEmployee(NIK, patchEntity);
                return Ok("Data Berhasil diupdate");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
            }
        }

        //delete data
        [HttpDelete("delete/{NIK}")]
        public ActionResult<Employee> DeleteEmployeeById(string NIK)
        {
            try
            {
                if (NIK == null)
                {
                    return BadRequest("NIK tidak boleh kosong");
                }
                var dataDihapus = employeeRepository.Delete(NIK);
                if (dataDihapus != 0)
                {
                    return Ok("data berhasil dihapus");
                }
                else
                {
                    return NotFound($"data dengan nik {NIK} tidak ditemukan");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
            }
        }
    }
}
