using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VCardsDbConnection;

namespace VCardsMVC4.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "NameRequired")]
        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "LanguageRequired")]
        [Display(Name = "Language", ResourceType = typeof(Resources.Resource))]
        public string Language { get; set; }

        [Display(Name = "Tag", ResourceType = typeof(Resources.Resource))]
        public IEnumerable<Tag> TagList { get; set; }
    }
}