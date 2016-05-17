using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rockawish.Models
{
    public class EventLocations
    {
        public int EventLocationId { get; set; }
        public string EventLocationName { get; set; }
        public string EventLocationAddress { get; set; }
        public string EventLocationAddress2 { get; set; }
        public string EventLocationCity { get; set; }
        public int EventLocationStateId { get; set; }
        public string EventLocationZipCode { get; set; }
        public DateTime create_dt { get; set; }
    }
}
