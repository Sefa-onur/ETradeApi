using ETradeApi.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommanRequest:IRequest<CreateProductCommanResponse>
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }

    }
}
