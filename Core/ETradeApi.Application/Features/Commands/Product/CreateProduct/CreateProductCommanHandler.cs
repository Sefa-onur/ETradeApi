using ETradeApi.Application.Repositories.ProductRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommanHandler : IRequestHandler<CreateProductCommanRequest, CreateProductCommanResponse>
    {
        private readonly IWriteProduct _context;
        public CreateProductCommanHandler(IWriteProduct context)
        {
            _context = context;
        }
        public async Task<CreateProductCommanResponse> Handle(CreateProductCommanRequest request, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
            });
            var query = await _context.SaveAsync();
            if (query > 0)
                return new() { Seccess = true,Message = "Kayıt Başarılı" };
            return new() { Seccess = false, Message = "Kayıt Oluştulamadı" };
        }
    }
}
