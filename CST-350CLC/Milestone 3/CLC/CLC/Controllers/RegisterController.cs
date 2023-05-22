using CLC.Models;
using CLC.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CLC.Controllers
{
    public class RegisterController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {

            return View("Register");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult doRegister(RegisterRequest registerRequest)
        {
            RegisterSecurityService ss = new RegisterSecurityService();
            RegisterResponse response;

            if (ModelState.IsValid)
            {
                response = ss.Authenticate(registerRequest);

                if (response.Success)
                {
                    return View("RegisterPassed", registerRequest);

                }
            }
            else
            {
                string errors = string.Join("<br/> ", ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage));
                response = new RegisterResponse(false, errors);
            }

            return View("RegisterFailed", response);

        }

    }
}