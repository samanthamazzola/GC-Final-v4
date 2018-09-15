//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GC_Final.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RAM()
        {
            this.BuildsRAMs = new HashSet<BuildsRAM>();
        }
    
        public string RAMID { get; set; }
        public string ProductID { get; set; }
        public Nullable<int> BusSpeed { get; set; }
        public Nullable<int> TotalCapacity { get; set; }
        public string RAMType { get; set; }
        public Nullable<int> Voltage { get; set; }
        public Nullable<byte> Quantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }
        public float Stars { get; set; }
        public string Manufacturer { get; set; }
        public string ImageLink { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BuildsRAM> BuildsRAMs { get; set; }
    }
}
