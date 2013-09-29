using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VCardsDbConnection;

namespace VCardsMVC4.Models
{
    public class AddWordModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "WordRequired")]
        [Display(Name = "Word", ResourceType = typeof(Resources.Resource))]
        public string Word { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "TranslationRequired")]
        [Display(Name = "Translation", ResourceType = typeof(Resources.Resource))]
        public string Translation { get; set; }

        [Display(Name = "Tag", ResourceType = typeof(Resources.Resource))]
        public IEnumerable<Tag> TagList { get; set; }
    }

    public class AddTagModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "TagRequired")]
        [Display(Name = "Tag", ResourceType = typeof(Resources.Resource))]
        public string Tag { get; set; }
    }
}