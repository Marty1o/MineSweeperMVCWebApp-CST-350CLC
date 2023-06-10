using CLC.Models;
using CLC.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CLC.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {

            return View("Login");
        }


        [HttpPost]
        public ActionResult doLogin(LoginRequest loginRequest)
        {

            LoginResponse response;

            if (ModelState.IsValid)
            {

                SecurityService ss = new SecurityService();
                response = ss.Authenticate(loginRequest);

                if (response.Success)
                {

                    //load user model 
                    UserService userService = new UserService();
                    var user = userService.loadUser(loginRequest);
                    Session["user"] = user;


                    return View("LoginPassed", loginRequest);

                }
            }
            else
            {
                string errors = string.Join("<br/> ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                response = new LoginResponse(false, errors);
            }
            return View("LoginFailed", response);


        }

    }
}