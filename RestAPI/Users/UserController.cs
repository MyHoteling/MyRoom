using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Security.Cryptography;
using RestAPI.Models;
using System.Text;

namespace RestAPI.Users
{
    public class UserController : ApiController
    {
        private IRestAPIContext db = new RestAPIContext();

        public UserController() { }

        public UserController(IRestAPIContext context) {
            this.db = context;
        }

        // GET: api/User
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/User/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/User/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            User dbUser = db.Users.Find(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            dbUser.Email = user.Email;
            dbUser.Active = user.Active;
            dbUser.Name = user.Name;
            dbUser.Surname = user.Surname;
            
            //Sólo actualiza la contraseña si la proporciona
            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                dbUser.Password = this.EncryptPassword(user.Password);
            }

            db.MarkAsModified(dbUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/User
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.Password = this.EncryptPassword(user.Password);

            db.Users.Add(user);

            try 
            {
                db.SaveChanges();
            }
            catch (DbUpdateException dex)
            {
                return BadRequest(dex.InnerException.InnerException.Message);
            }
            

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }

        private string EncryptPassword(string password)
        {
            SHA1 sha1 = SHA1.Create();

            byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(password));

            StringBuilder returnValue = new StringBuilder();

            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString("x2"));
            }

            return returnValue.ToString();
        }
    }
}