using CLC.Models;
using CLC.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CLC.Services.Business
{
    public class RegisterSecurityService
    {



        public RegisterResponse Authenticate(RegisterRequest registerRequest)
        {

            RegisterResponse response = new RegisterResponse();
            response.Success = false;

            RegisterSecurityDAO dataService = new RegisterSecurityDAO();



            if (dataService.userExists(registerRequest))
            {
                response.Message = "Username already exists.";
            }
            else if (dataService.emailExists(registerRequest))
            {
                response.Message = "Email already in use.";
            }
            else if (dataService.createUser(registerRequest))
            {
                response.Success = true;
            }


            return response;

        }

    }
}