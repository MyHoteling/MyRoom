using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestAPI.Models
{
    [Table("ACCESS_TOKENS")]
    public class Token
    {
        [Key]
        public int Id { get; set; }

        public User User { get; set; }
        public string AccessToken { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expiration { get; set; }
    }
}