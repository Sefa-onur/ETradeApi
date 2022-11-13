using ETradeApi.Application.Repositories.CustomerRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Application.Features.Commands.Customer.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommanRequest, CreateCustomerCommandResponse>
    {
        private readonly IWriteCustomer _context;
        public CreateCustomerCommandHandler(IWriteCustomer context)
        {
            _context = context;
        }

        public async Task<CreateCustomerCommandResponse> Handle(CreateCustomerCommanRequest request, CancellationToken cancellationToken)
        {
            var query = await _context.AddAsync(new()
            {
                Name = request.Name,
                Address = request.Address
            });
            int sonuc = await _context.SaveAsync();
            if (sonuc > 0)
                return new() { Success = true, Message = "Kayıt Başarılı" };
            return new() { Success = false, Message = "Kayıt Başarısız"};
        }
    }
}
