using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockawayWish.Models
{
    public class ContactUs
    {
        public int ContactUsId { get; set; }
        public string ContactUsName { get; set; }
        public string ContactUsAddress { get; set; }
        public string ContactUsAddress2 { get; set; }
        public string ContactUsCity { get; set; }
        public int ContactUsStateId { get; set; }
        public string ContactUsZipCode { get; set; }
        public string ContactUsPhone { get; set; }
        public string ContactUsFax { get; set; }
        public DateTime create_dt { get; set; }
    }
}
