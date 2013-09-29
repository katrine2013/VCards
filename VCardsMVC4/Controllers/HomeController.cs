using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using VCardsDbConnection;

using System.Web.Security;
using VCardsDbConnection;
using VCardsMVC4.Filters;
using VCardsMVC4.Models;

namespace VCardsMVC4.Controllers
{
    
    [Culture]
    public class HomeController : Controller
    {
        

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            using (var db = new VCardsEntities())
            {
                var T = db.Tags.FirstOrDefault();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }


        public ActionResult Words()
        {
            ViewBag.Message = "Your app description page.";

            return View(new AddWordModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Words(AddWordModel newWord)
        {
            ViewBag.Message = "Your app description page.";
            if(ModelState.IsValid)
            {
                using (var db = new VCardsEntities())
                {
                    db.Words.Add(new Word()
                                     {
                                         FailsCount = 0,
                                         SucceedsCount = 0,
                                         IsIdiom = false,
                                         ModifyDate = DateTime.UtcNow,
                                         Word1 = newWord.Word,
                                         Translation = newWord.Translation,
                                         Uid = Guid.NewGuid(),
                                         UserId = 2
                                     });
                    db.SaveChanges();
                }
            }

            return View(newWord);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            using (VCardsEntities db = new VCardsEntities())
            {
                var user1 = db.Users.Include("Tags").FirstOrDefault();

                if (user1 != null)
                {
                    return View(new Models.User()
                        {
                            Language = user1.LastName,
                            Name = user1.Name,
                            TagList = user1.Tags,
                            Id = user1.Id
                        });
                }
            }

            return View(new Models.User());
        }

        [HttpPost]
        public ActionResult AddTag (string tag)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            if(!String.IsNullOrEmpty(tag))
            {
                using (VCardsEntities db = new VCardsEntities())
                {
                    int userId = Convert.ToInt32(User.Identity.Name);
                    if (db.Tags.FirstOrDefault(x => x.UserId == userId && x.Tag1 == tag) == null)
                    {
                        Tag tagAdd = new Tag();
                        tagAdd.UserId = userId;
                        tagAdd.Tag1 = tag;
                        db.Tags.Add(tagAdd);
                        db.SaveChanges();
                    }
                }
            }
            return Redirect(returnUrl);
        }

        public ActionResult ChangeCulture(string lang, int id)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            // Список культур
            List<string> cultures = new List<string>() { "ru", "en" };
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }
            // Сохраняем выбранную культуру в куки
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;   // если куки уже установлено, то обновляем значение
            else
            {

                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;               
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            using (VCardsEntities db = new VCardsEntities())
            {
                var user1 = db.Users.FirstOrDefault(x=> x.Id == id);

                if (user1 != null)
                    user1.UILanguage = lang;
                db.SaveChanges();
                FormsAuthentication.SetAuthCookie(id.ToString(), false);

            }

            return Redirect(returnUrl);
        }

    }
}
