//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aloe.DB_Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class DealPoint
    {
        public int ID { get; set; }
        public string DealReview { get; set; }
        public int TemplatePointsID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public byte Status { get; set; }
        public int Deal_ID { get; set; }
    
        public virtual LOITemplatePoint LOITemplatePoint { get; set; }
        public virtual Deal Deal { get; set; }
    }
}