//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RockawayWish.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class EventLocations
    {
        public EventLocations()
        {
            this.Events = new HashSet<Events>();
        }
    
        public int EventLocationId { get; set; }
        public string EventLocationName { get; set; }
        public string EventLocationAddress { get; set; }
        public string EventLocationAddress2 { get; set; }
        public string EventLocationCity { get; set; }
        public int EventLocationStateId { get; set; }
        public string EventLocationZipCode { get; set; }
        public System.DateTime create_dt { get; set; }
    
        public virtual States State { get; set; }
        public virtual ICollection<Events> Events { get; set; }
    }
}