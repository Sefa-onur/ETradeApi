using ETradeApi.Application.Repositories.CustomerRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Application.Features.Commands.Customer.RemoveCustomer
{
    public class RemoveCustomerCommandHandler : IRequestHandler<RemoveCustomerCommandRequest, RemoveCustomerCommandResponse>
    {
        private readonly IWriteCustomer _context;
        public RemoveCustomerCommandHandler(IWriteCustomer context)
        {
            _context = context;
        }

        public async Task<RemoveCustomerCommandResponse> Handle(RemoveCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            var query = await _context.RemoveAsync(request.ID);
            var sonuc = await _context.SaveAsync();
            return new();
        }
    }
}
