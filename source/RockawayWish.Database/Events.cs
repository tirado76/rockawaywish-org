using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockawayWish.Database
{
    public class Events
    {
        public int EventId { get; set; }
        public int EventLocationId { get; set; }
        public DateTime EventDateTime { get; set; }
        public string EventDescription { get; set; }
        public DateTime create_dt { get; set; }
    }
}
