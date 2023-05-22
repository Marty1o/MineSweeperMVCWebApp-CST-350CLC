using CLC.Models;
using CLC.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CLC.Services.Business
{
    public class SecurityService
    {

        public LoginResponse Authenticate(LoginRequest loginRequest)
        {

            LoginResponse response = new LoginResponse();
            response.Success = false;

            SecurityDAO dataService = new SecurityDAO();

            if (dataService.validUser(loginRequest))
            {
                response.Success = true;
            }
            else
            {
                response.Message = "Invalid username or password.";
            }

            return response;

        }

    }
}