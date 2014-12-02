using RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace RestAPI.Users
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {
        private IRestAPIContext db = new RestAPIContext();

        [ResponseType(typeof(string))]
        [AllowAnonymous]
        public IHttpActionResult GetAuth()
        {
            return Ok("test");
        }

        [ResponseType(typeof(Token))]
        [AllowAnonymous]
        public IHttpActionResult PostAuth(Models.Auth auth)
        {
            User user = db.Users
                          .Where(u => u.Email == auth.Identity)
                          .FirstOrDefault();

            try { 
                Token token = new Token();

                token.User = user;
                token.Created = DateTime.Now;
                token.Expiration = DateTime.Now.AddDays(1);

                token.AccessToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray()) + "_" + Guid.NewGuid().ToString();

                db.Tokens.Add(token);

                db.SaveChanges();

                return Ok(token);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}
