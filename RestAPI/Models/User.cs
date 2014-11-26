using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.Models
{
    [Table("BACKEND_USER")]
    public class User
    {
        [Key]
        public int Id
        {
            get;
            set;
        }

        [Column("Name", TypeName="varchar")]
        [StringLength(100)]
        public string Name
        {
            get;
            set;
        }

        [Column("Surname", TypeName="varchar")]
        [StringLength(100)]
        public string Surname
        {
            get;
            set;
        }

        [Required(ErrorMessage="The email is required")]
        [Column("E-mail", TypeName="varchar")]
        [Index(IsUnique=true)]
        [StringLength(100)]
        public string Email
        {
            get;
            set;
        }

        [Column("Password", TypeName="varchar")]
        [StringLength(50)]
        public string Password
        {
            get;
            set;
        }

        [Required(ErrorMessage="The user status is required")]
        [Column("Active", TypeName="bit")]
        public bool Active
        {
            get;
            set;
        }
    }
}