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
    
    public partial class LOITemplatePoint
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOITemplatePoint()
        {
            this.DealPoints = new HashSet<DealPoint>();
        }
    
        public int ID { get; set; }
        public string DealPoint { get; set; }
        public string InitialValue { get; set; }
        public int TemplateID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public byte Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DealPoint> DealPoints { get; set; }
        public virtual LOITemplate LOITemplate { get; set; }
    }
}
