using ETradeApi.Application.Repositories.ProductRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommanHandler : IRequestHandler<UpdateProductCommanRequest, UpdateProductCommanResponse>
    {
        private readonly IWriteProduct _writecontext;
        private readonly IReadProduct _readcontext;
        public UpdateProductCommanHandler(IWriteProduct writecontext,IReadProduct readcontext)
        {
            _writecontext = writecontext;
            _readcontext = readcontext;
        }
        public async Task<UpdateProductCommanResponse> Handle(UpdateProductCommanRequest request, CancellationToken cancellationToken)
        {
            Domain.Product entity = await _readcontext.GetByIdAsync(request.ID.ToString());
            entity.Name = request.Name;
            entity.Stock = request.Stock;
            entity.Price = request.Price;
            bool sonuc = _writecontext.Update(entity);
            await _writecontext.SaveAsync();
            if (sonuc)
                return new() { Success = true, Message = "Güncelleme Tamamlandı" };
            return new() { Success = false, Message = "Güncelleme Başarısız" };
        }
    }
}
