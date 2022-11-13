using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Application.Features.Commands.Customer.CreateCustomer
{
    public class CreateCustomerCommanRequest:IRequest<CreateCustomerCommandResponse>
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
