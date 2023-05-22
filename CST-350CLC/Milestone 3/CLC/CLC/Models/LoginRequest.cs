using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CLC.Models
{
    public class LoginRequest
    {

        private string username;
        private string password;

        public LoginRequest()
        {
        }

        public LoginRequest(string username, string password)
        {
            this.username = username;
            this.password = password;
        }



        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string Username { get => username; set => username = value; }


        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        [DataType(DataType.Password)]
        public string Password { get => password; set => password = value; }

    }
}