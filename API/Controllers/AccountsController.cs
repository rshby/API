using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository _accountRepo;

        // Constructor
        public AccountsController(AccountRepository accRepo) : base(accRepo)
        {
            this._accountRepo = accRepo;    
        }

        // Insert Register
        [HttpPost("register")]
        public ActionResult Register(RegisterVM inputData)
        {
            try
            {
                if (inputData != null)
                {
                    var hasilRegister = _accountRepo.Register(inputData); 
                    if (hasilRegister > 0)
                    {
                        return Ok("Sukses, Register Berhasil");
                    }
                    else if (hasilRegister == -1)
                    {
                        return BadRequest("Maaf, Email sudah ada");
                    }
                    else if (hasilRegister == -2)
                    {
                        return BadRequest("Maaf, nomor telepon sudah ada");
                    }
                    else
                    {
                        return BadRequest("Maaf, email dan nomor telepon sudah ada");
                    }
                }
                else
                {
                    return BadRequest("Data Kosong");
                }
            }
            catch (Exception)
            {
                return BadRequest("Error Saat Register");
            }
        }
    }
}
