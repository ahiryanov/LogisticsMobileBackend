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
    
    public partial class Equipment
    {
        public int IDEquipment { get; set; }
        public int IDModel { get; set; }
        public string HealthState { get; set; }
        public string PositionState { get; set; }
        public string AssignedPosition { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public string ISNumber { get; set; }
        public Nullable<int> IDRent { get; set; }
    
        public virtual Model Model { get; set; }
    }
}
