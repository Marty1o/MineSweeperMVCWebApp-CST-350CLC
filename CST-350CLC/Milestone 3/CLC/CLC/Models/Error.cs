using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CLC.Models
{
    public class Error
    {

 
        private string content;

        public Error(string content)
        {
            this.content = content;
        }

        public string Content { get => content; set => content = value; }

    }
}