using ETradeApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Domain
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
