//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TipezeNyumbaService.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FenceType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FenceType()
        {
            this.Houses = new HashSet<House>();
        }
    
        public int fenceTypeID { get; set; }
        public string typeOfFence { get; set; }
        public int state { get; set; }
        public System.DateTime dateCreated { get; set; }
    
        public virtual FieldState FieldState { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<House> Houses { get; set; }
    }
}