using ETradeApi.Application.Repositories.ProductRepository;
using ETradeApi.Domain;
using ETradeApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Persistence.Repositories.ProductRepository
{
    public class WriteProduct : WriteRepository<Product>, IWriteProduct
    {
        public WriteProduct(ETradeDbContext context) : base(context)
        {
        }
    }
}
