using Domnita_Ionel_Proiect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domnita_Ionel_Proiect.Data
{
    public class DbInitializer
    {
        public static void Initialize(StoreContext context)
        {
            context.Database.EnsureCreated();
            if (context.Phones.Any())
            {
                return; // BD a fost creata anterior
            }
            var phones = new Phone[]
            {
new Phone{Model="Galaxy Note 10",Company ="Samsung",Price=Decimal.Parse("4200")},
new Phone{Model="Iphone 12",Company="Apple",Price=Decimal.Parse("3800")},
new Phone{Model="Huawei P40 Pro",Company="Huawei",Price=Decimal.Parse("3700")},
new Phone{Model="Galaxy Note 20",Company ="Samsung",Price=Decimal.Parse("5200")},
new Phone{Model="Iphone 12 Pro",Company="Apple",Price=Decimal.Parse("4800")},
new Phone{Model="Huawei P30 Pro",Company="Huawei",Price=Decimal.Parse("2700")}
            };
            foreach (Phone p in phones)
            {
                context.Phones.Add(p);
            }
            context.SaveChanges();
            var customers = new Customer[]
            {
new Customer{CustomerID=1050,Name="Popescu Cristina",BirthDate=DateTime.Parse("1999-09-01")},
new Customer{CustomerID=1045,Name="Pop Cornel",BirthDate=DateTime.Parse("1998-03-08")},
new Customer{CustomerID=1040,Name="Vasile Ion",BirthDate=DateTime.Parse("1997-07-08")},
            };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();
            var orders = new Order[]
            {
new Order{PhoneID=1,CustomerID=1040,OrderDate=DateTime.Parse("02-25-2020")},
new Order{PhoneID=3,CustomerID=1045,OrderDate=DateTime.Parse("09-28-2020")},
new Order{PhoneID=1,CustomerID=1045,OrderDate=DateTime.Parse("10-28-2020")},
new Order{PhoneID=2,CustomerID=1050,OrderDate=DateTime.Parse("09-28-2020")},
new Order{PhoneID=4,CustomerID=1040,OrderDate=DateTime.Parse("09-28-2020")},
new Order{PhoneID=6,CustomerID=1050,OrderDate=DateTime.Parse("10-28-2020")},
            };
            foreach (Order e in orders)
            {
                context.Orders.Add(e);
            }
            context.SaveChanges();
            var stores = new Store[]
            {
new Store{StoreName="Huawei",Adress="www.huawei.com"},
new Store{StoreName="Samsung",Adress="www.samsung.com"},
new Store{StoreName="Apple",Adress="www.apple.com"},
            };
            foreach (Store s in stores)
            {
                context.Stores.Add(s);
            }
            context.SaveChanges();
            var onlinestores = new OnlineStore[]
            {
new OnlineStore {
PhoneID = phones.Single(c => c.Model == "Galaxy Note 10" ).ID,
StoreID = stores.Single(i => i.StoreName == "Samsung").ID
},
new OnlineStore {
PhoneID = phones.Single(c => c.Model == "Galaxy Note 20" ).ID,
StoreID = stores.Single(i => i.StoreName == "Samsung").ID
},
new OnlineStore {
PhoneID = phones.Single(c => c.Model == "Iphone 12" ).ID,
StoreID = stores.Single(i => i.StoreName == "Apple").ID
},
new OnlineStore {
PhoneID = phones.Single(c => c.Model == "Iphone 12 Pro" ).ID,
StoreID = stores.Single(i => i.StoreName == "Apple").ID
},
new OnlineStore {
PhoneID = phones.Single(c => c.Model == "Huawei P40 Pro" ).ID,
StoreID = stores.Single(i => i.StoreName == "Huawei").ID
},
new OnlineStore {
PhoneID = phones.Single(c => c.Model == "Huawei P30 Pro" ).ID,
StoreID = stores.Single(i => i.StoreName == "Huawei").ID
},
            };
            foreach (OnlineStore pb in onlinestores)
            {
                context.OnlineStore.Add(pb);
            }
            context.SaveChanges();
        }
    }
}
