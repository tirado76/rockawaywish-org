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
    
    public partial class ContactU
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
        public System.DateTime create_dt { get; set; }
    
        public virtual State State { get; set; }
    }
}
