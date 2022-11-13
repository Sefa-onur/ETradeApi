using ETradeApi.Application.Repositories.CustomerRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Application.Features.Commands.Customer.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommandRequest, UpdateCustomerCommanResponse>
    {
        private readonly IWriteCustomer _contextwrite;
        private readonly IReadCustomer _contextread;
        public UpdateCustomerCommandHandler(IWriteCustomer contextwrite,IReadCustomer contextread)
        {
            _contextwrite = contextwrite;
            _contextread = contextread;
        }

        public async Task<UpdateCustomerCommanResponse> Handle(UpdateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Customer query = await _contextread.GetByIdAsync(request.Id);
            query.Name = request.Name;
            query.Address = request.Address;
            _contextwrite.Update(query);
            int sonuc = await _contextwrite.SaveAsync();
            if (sonuc > 0)
                return new() { Success = true,Message = "Güncelleme Başarılı" };
            return new() { Success = false, Message = "Güncelleme Başarısız" };
        }
    }
}
