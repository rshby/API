using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
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

        [HttpPost("loginasmanager")]
        // Login As Manager
        public ActionResult LoginAsManager(LoginVM inputData)
        {
            try
            {
                var hasilLogin = _accountRoleRepository.SignManager(inputData);
                if (hasilLogin == 1)
                {
                    return Ok(new
                    {
                        status = HttpStatusCode.OK,
                        message = "Login Berhasil"
                    });
                }
                else if(hasilLogin == -2)
                {
                    return Ok(new
                    {
                        status = HttpStatusCode.OK,
                        message = "User Role Sudah Pernah dibuat"
                    });
                }
                else if (hasilLogin == -1)
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "User dengan email tersebut belum menjadi Director"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
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
