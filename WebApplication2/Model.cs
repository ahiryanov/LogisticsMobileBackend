//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Model
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Model()
        {
            this.Equipment = new HashSet<Equipment>();
        }
    
        public int IDModel { get; set; }
        public string ModelName { get; set; }
        public string Characteristics { get; set; }
        public string VendorName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string EquipmentType { get; set; }
        public string SubType { get; set; }
        public Nullable<int> WeightNet { get; set; }
        public Nullable<int> WeightGross { get; set; }
        public Nullable<int> LenghtGross { get; set; }
        public Nullable<int> WidthGross { get; set; }
        public Nullable<int> HeightGross { get; set; }
        public string AmountGross { get; set; }
        public Nullable<int> Price { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}
