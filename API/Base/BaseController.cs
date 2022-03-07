using API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;

        // Costructor
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        // Get All Data
        [HttpGet]
        public ActionResult<Entity> GET()
        {
            if (repository.GET().Count() != 0)
            {
                return Ok(repository.GET());
            }
            else
            {
                return NotFound("Data Tidak Ditemukan"); 
            }
            
        }

        // Get Data By Id
        [HttpGet("{Id}")]
        public ActionResult<Entity> GetDataById(Key Id)
        {
            var hasilData = repository.Get(Id);
            if (hasilData != null)
            {
                return Ok(hasilData);
            }
            else
            {
                return NotFound("Data Tidak Ada");
            }
        }

        // Insert Data to Database
        [HttpPost]
        public ActionResult<Entity> InsertData(Entity classModel)
        {
            var hasilInsert = repository.Insert(classModel);
            if (hasilInsert != 0)
            {
                return Ok("Selamat Data Sudah Ditambahkan");
            }
            else
            {
                return BadRequest("Data Gagal Ditambahkan");
            }
        }

        // Update Data
        [HttpPut]
        public ActionResult<Entity> UpdateData(Entity classModel)
        {
            var hasilUpdate = repository.Update(classModel);
            if (hasilUpdate != 0)
            {
                return Ok("Selamat Data Berhasil Diupdate");
            }
            else
            {
                return BadRequest("Data Gagal Diupdate");
            }
        }

        // Delete Data By Id
        [HttpDelete("{Id}")]
        public ActionResult<Entity> DeleteDataById(Key Id)
        {
            try
            {
                var dataTerpilih = repository.Get(Id);  
                var hasilDelete = repository.Delete(Id);
                if (dataTerpilih != null)
                {
                    if (hasilDelete != 0)
                    {
                        return Ok("Selamat Data Berhasil Dihapus");

                    }
                    else
                    {
                        return BadRequest("Gagal Delete Data");
                    }
                }
                else
                {
                    return NotFound($"Maaf, Data Dengan Id {Id} Tidak Ada");
                }
            }
            catch (Exception)
            {
                return BadRequest("Gagal Delete Data");
            }
            
        }
    }
}
