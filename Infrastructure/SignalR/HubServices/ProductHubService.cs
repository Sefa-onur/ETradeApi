﻿using ETradeApi.Application.Abstractions.Hubs;
using Microsoft.AspNetCore.SignalR;
using SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.HubServices
{
    public class ProductHubService : IProductHubService
    {
        readonly IHubContext<ProductHub> _context;
        public ProductHubService(IHubContext<ProductHub> context)
        {
            _context = context;
        }
    
        public async Task ProductAddedMessageAsync(string message)
        {
            await _context.Clients.All.SendAsync(ReceiveFunctionNames.ProductAddedMessage, message);
        }
    }
}
