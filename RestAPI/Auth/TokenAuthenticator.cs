using RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace RestAPI.Auth
{
    public class TokenAuthenticator : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext context)
        {
            bool output = false;
            try
            {
                string token = context.Request.Headers.GetValues("Access-Token").FirstOrDefault();

                IRestAPIContext db = new RestAPIContext();

                Token dbToken = db.Tokens
                                    .Where(t => t.AccessToken == token)
                                    .FirstOrDefault();

                if (dbToken != null)
                {
                    if (dbToken.Expiration > DateTime.Now)
                    {
                        output = true;
                    }
                }
            }
            catch (Exception e)
            {
                output = false;
            }
            

            return output;
        }
    }
}