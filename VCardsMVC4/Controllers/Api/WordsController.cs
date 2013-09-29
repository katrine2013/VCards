using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using VCardsDbConnection;
using System.Runtime.Serialization.Json;

namespace VCardsMVC4.Controllers.Api
{
    public class WordsController : Controller
    {


        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonDotNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
        

        private VCardsEntities db = new VCardsEntities();

        //// GET api/Words
        //public IEnumerable<Word> GetWords()
        //{
        //    var words = db.Words.Include(w => w.User);
        //    return words.AsEnumerable();
        //}

        //// GET api/Words/5
        //public Word GetWord(int id)
        //{
        //    Word word = db.Words.Find(id);
        //    if (word == null)
        //    {
        //        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
        //    }

        //    return word;
        //}

        //// PUT api/Words/5
        [System.Web.Http.HttpGet]
        public string PutWord(int userId, string word, string translate, int id = 0, string ugid = "5a1caf8e-1cbc-42f7-ae66-6a22718c9e57")
        {
            using (var db = new VCardsEntities())
            {
                var guid = Guid.Empty;
                Guid.TryParse(ugid, out guid);

                var user = db.Users.Where(_=>_.SocialId == guid).FirstOrDefault();
                Word word1 = null;
                if (user != null)
                {
                    word1 = new Word() {Word1 = word, Translation = translate, ModifyDate = DateTime.UtcNow, Uid = Guid.NewGuid()};
                    word1.UserId = user.Id;
                    db.Words.Add(word1);
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

        //[System.Web.Http.HttpGet]
        //public JsonResult GetWords()
        //{
        //    IEnumerable<Word> result = new List<Word>();
        //    return 
        //}

        [System.Web.Http.HttpGet]
        public JsonResult GetWord(string id = "5a1caf8e-1cbc-42f7-ae66-6a22718c9e57")
        {
            using (var db = new VCardsEntities())
            {
                IEnumerable<Word> result = new List<Word>();
                var guid = Guid.Empty;
                Guid.TryParse(id, out guid);
                var user = db.Users.Where(_=>_.SocialId == guid).FirstOrDefault();
                if (user != null)
                {
                    result = db.Words.Where(_=>_.UserId == user.Id).ToArray();
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        //// POST api/Words
        //public HttpResponseMessage PostWord(Word word)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Words.Add(word);
        //        db.SaveChanges();

        //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, word);
        //        response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = word.Id }));
        //        return response;
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }
        //}

        //// DELETE api/Words/5
        //public HttpResponseMessage DeleteWord(int id)
        //{
        //    Word word = db.Words.Find(id);
        //    if (word == null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound);
        //    }

        //    db.Words.Remove(word);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound);
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK, word);
        //}
    }
}