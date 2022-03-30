﻿using Client.Base;
using API.ViewModel;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LoginController : BaseController<LoginVM, LoginRepository, string>
    {
        private readonly LoginRepository repository;
        public LoginController(LoginRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Auth([FromBody] LoginVM login)
        {
            var jwtToken = await repository.Auth(login);
            var token = jwtToken.Token;

            if (token == null)
            {
                return RedirectToAction("index");
            }

            HttpContext.Session.SetString("JWToken", token);
            //HttpContext.Session.SetString("Name", jwtHandler.GetName(token));
            HttpContext.Session.SetString("ProfilePicture", "assets/img/theme/user.png");

            return RedirectToAction("index", "dashboard");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
