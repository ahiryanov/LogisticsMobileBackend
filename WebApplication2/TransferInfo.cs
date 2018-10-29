using System.Collections.Generic;
using WebApplication2;

namespace LogisticsMobile
{
    public class TransferInfo
    {
        public List<Equipment> Equipments { get; set; }
        public int UserID { get; set; } 
        public string NewPosition { get; set; }
    }
}
