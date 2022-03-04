using API.Context;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository employeeRepository)
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
                if (resultInsert > 0)
                {
                    return Ok("Data Berhasil ditambah");
                }
                else if (resultInsert == -1)
                {
                    return BadRequest("Data Gagal ditambah, email sudah ada");
                }
                else if (resultInsert == 0)
                {
                    return BadRequest("No hp dan email sudah ada");
                }
                else
                {
                    return BadRequest("Data Gagal ditambah, no hp sudah ada");
                }
            }
            catch (Exception)
            {
                return BadRequest("Error saat insert data");
            }
        }

        //show data
        [HttpGet]
        public ActionResult GET()
        {
            try
            {
                if (employeeRepository.GET().Count() != 0)
                {
                    return Ok(employeeRepository.GET());
                }
                else
                {
                    return NotFound("Data tidak ada");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error SHowing Data");
            }
        }

        //show data by NIK
        [HttpGet("{NIK}")]
        public ActionResult GetEmployeeById(string NIK)
        {
            try
            {
                var hasilData = employeeRepository.Get(NIK);
                if (hasilData != null)
                {
                    return Ok(employeeRepository.Get(NIK));
                }
                else
                {
                    return BadRequest("Data tidak ada");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error SHowing Data");
            }
        }

        //Update data employee
        [HttpPut("update")]
        public ActionResult UpdateEmployee(Employee employee)
        {
            try
            {
                if (employee != null)
                {
                    var hasilUpdate = employeeRepository.Update(employee);
                    if (hasilUpdate != 0)
                    {
                        return Ok("Data Berhasil diupdate");
                    }
                    else
                    {
                        return BadRequest("Data Gagal Diupdate");
                    }
                }
                else
                {
                    return BadRequest("Data Harus Di isi");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
            }
        }

        //Delete Data 
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
                return BadRequest($"NIK {NIK} tidak ditemukan");
            }
        }
    }
}
