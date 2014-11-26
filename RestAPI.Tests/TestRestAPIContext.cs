using RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Tests
{
    class TestRestAPIContext : IRestAPIContext
    {
        public TestRestAPIContext()
        {
            this.Users = new TestUserDbSet();
        }

        public DbSet<User> Users { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(User item) { }
        public void Dispose() { }
    }
}
