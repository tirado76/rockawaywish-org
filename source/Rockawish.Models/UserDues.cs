using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockawayWish.Models
{
    public class UserDues
    {
        public int UserDueId { get; set; }
        public int UserId { get; set; }
        public int DuesId { get; set; }
        public int PaymentTypeId { get; set; }
        public DateTime DuesPaidDate { get; set; }
        public DateTime create_dt { get; set; }
    }
}
