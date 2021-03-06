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
    
    public partial class OpticalDriver
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OpticalDriver()
        {
            this.BuildODs = new HashSet<BuildOD>();
        }
    
        public string OpticalDriverID { get; set; }
        public string ProductID { get; set; }
        public Nullable<bool> Rewritable { get; set; }
        public string Interface { get; set; }
        public Nullable<int> ReadSpeed { get; set; }
        public Nullable<int> WriteSpeed { get; set; }
        public Nullable<int> Wattage { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<float> Stars { get; set; }
        public string Manufacturer { get; set; }
        public string ImageLink { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BuildOD> BuildODs { get; set; }
    }
}
