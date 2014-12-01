using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestAPI.Models
{
    public class Auth
    {
        public string Identity { get; set; }
        public string Credential { get; set; }
    }
}