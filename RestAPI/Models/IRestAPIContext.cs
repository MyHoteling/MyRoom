using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestAPI.Models
{
    public interface IRestAPIContext : IDisposable 
    {
        DbSet<User> Users { get; }
        DbSet<Token> Tokens { get; }
        int SaveChanges();
        void MarkAsModified(User item);
       
    }
}