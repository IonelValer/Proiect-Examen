using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domnita_Ionel_Proiect.Models.StoreViewModels
{
    public class StoreIndexData
    {
        public IEnumerable<Store> Stores { get; set; }
        public IEnumerable<Phone> Phones { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
