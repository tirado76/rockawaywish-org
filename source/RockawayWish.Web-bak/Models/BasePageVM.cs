using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockawayWish.Web.Models
{
    public class BasePageVM
    {
        public string pageTitle { get; set; }
        public string headerText { get; set; }
        public string metaKeywords { get; set; }
        public string metaDescription { get; set; }
    }
}