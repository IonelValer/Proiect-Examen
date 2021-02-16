using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domnita_Ionel_Proiect.Models
{
    public class OnlineStore
    {
        public int StoreID { get; set; }
        public int PhoneID { get; set; }
        public Store Store { get; set; }
        public Phone Phone { get; set; }
    }
}
