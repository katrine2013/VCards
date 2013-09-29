using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VCardsDbConnection;

namespace VCardsMVC4.Controllers
{
    public class oauth2callbackController : Controller
    {
        //
        // GET: /oauth2callback/
        [HttpGet]
        public string Index(string id = "5a1caf8e-1cbc-42f7-ae66-6a22718c9e57")
        {
            using (var db = new VCardsEntities())
            {
                var guid = Guid.Empty;
                Guid.TryParse(id, out guid);

                var user = db.Users.Where(_ => _.SocialId == guid).FirstOrDefault();
                
                if (user == null)
                {
                   db.Users.Add(new User() { LastUpdate = DateTime.UtcNow, Points = 0, Tocken = Guid.NewGuid(), SocialId = guid, UILanguage = "en"});
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return ex.StackTrace + ex.InnerException.StackTrace;
                }

                return "OK";

            }
        }

    }
}
