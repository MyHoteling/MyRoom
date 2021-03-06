﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestAPI.Models
{
    public class RestAPIContext : DbContext, IRestAPIContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public RestAPIContext() : base("name=RestAPIContext")
        {
        }

        public System.Data.Entity.DbSet<RestAPI.Models.User> Users { get; set; }
        public System.Data.Entity.DbSet<RestAPI.Models.Token> Tokens { get; set; }

        public void MarkAsModified(User item)
        {
            Entry(item).State = EntityState.Modified;
        }
    }
}
