using ETradeApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Domain
{
    public class Customer:BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Product> Products { get; set; }

    }
}
