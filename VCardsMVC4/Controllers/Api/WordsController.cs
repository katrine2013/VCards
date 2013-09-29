using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using VCardsDbConnection;

namespace VCardsMVC4.Controllers.Api
{
    public class WordsController : ApiController
    {
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
        [HttpGet] 
        public string PutWord(int userId, string word)
        {
            
            using (var db = new VCardsEntities())
            {
                

                if (ModelState.IsValid)
                {
                    var user = db.Users.FirstOrDefault();
                    var word1 = new Word() {Word1 = word, Translation = "блядь", ModifyDate = DateTime.UtcNow};
                    word1.UserId = user.Id;

                    db.Words.Add(word1);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return ex.StackTrace + ex.InnerException.StackTrace;
                    }

                    return Request.CreateResponse(HttpStatusCode.OK).ToString();
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest).ToString();
                }
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