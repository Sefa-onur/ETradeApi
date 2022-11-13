using ETradeApi.Application.Repositories.ProductRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Application.Features.Commands.Product.RemoveProduct
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
    {
        private readonly IWriteProduct _context;
        public RemoveProductCommandHandler(IWriteProduct context)
        {
            _context = context;
        }
        public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _context.RemoveAsync(request.ID);
            await _context.SaveAsync();
            return new();
        }
    }
}
