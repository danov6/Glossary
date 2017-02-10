using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewGlossary.Models
{
    public class Entry
    {
        public string Id { get; set; }

        [Display(Name = "Term")]
        public string Term { get; set; }

        [Display(Name = "Definition")]
        public string Definition { get; set; }
    }
}