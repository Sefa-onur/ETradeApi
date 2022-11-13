﻿using ETradeApi.Application.Repositories.CustomerRepository;
using ETradeApi.Domain;
using ETradeApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Persistence.Repositories.CustomerRepository
{
    public class ReadCustomer : ReadRepository<Customer>, IReadCustomer
    {
        public ReadCustomer(ETradeDbContext context) : base(context)
        {
        }
    }
}
