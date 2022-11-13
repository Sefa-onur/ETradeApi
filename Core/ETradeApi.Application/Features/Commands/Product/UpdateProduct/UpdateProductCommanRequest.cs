using ETradeApi.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommanRequest : IRequest<UpdateProductCommanResponse>
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
    }
}
