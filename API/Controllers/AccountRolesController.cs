using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
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
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, int>
    {
        private readonly AccountRoleRepository _accountRoleRepository;
        public IConfiguration _configuration;
        public AccountRolesController(AccountRoleRepository acrRepo, IConfiguration configuration) : base(acrRepo)
        {
            this._accountRoleRepository = acrRepo;
            this._configuration = configuration;
        }

        [Authorize(Roles = "Director")]
        [HttpPost("loginasmanager")]
        // Login As Manager
        public ActionResult LoginAsManager(LoginVM inputData)
        {
            try
            {
                // nilai dari hasilLogin
                var hasilLogin = _accountRoleRepository.SignInManager(inputData);

                // cek hasilLogin
                if (hasilLogin == 1)
                {
                    return Ok(new
                    {
                        status = HttpStatusCode.OK,
                        message = "Login Sebagai Manager Berhasil"
                    });
                }
                else if (hasilLogin == -1)
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Password Anda Salah"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        status = HttpStatusCode.NotFound,
                        message = "Email Tidak Terdaftar di Database"
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = e.Message
                });
            }
        }
    }
}
