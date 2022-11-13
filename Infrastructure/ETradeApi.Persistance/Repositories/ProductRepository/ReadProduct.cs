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
    public class ReadProduct : ReadRepository<Product>, IReadProduct
    {
        public ReadProduct(ETradeDbContext context) : base(context)
        {
        }
    }
}
