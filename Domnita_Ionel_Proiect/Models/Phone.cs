using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domnita_Ionel_Proiect.Models
{
    public class Phone
    {
        public int ID { get; set; }
        public string Model { get; set; }
        public string Company { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<OnlineStore> OnlineStores { get; set; }
    }
}
