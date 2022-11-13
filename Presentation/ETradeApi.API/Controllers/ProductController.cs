using ETradeApi.Application.Features.Commands.Product.CreateProduct;
using ETradeApi.Application.Features.Commands.Product.RemoveProduct;
using ETradeApi.Application.Features.Commands.Product.UpdateProduct;
using ETradeApi.Application.Repositories.ProductRepository;
using ETradeApi.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace ETradeApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IReadProduct _context;
        private readonly IWriteProduct _context2;
        readonly IMediator _mediator;
        public ProductController(IReadProduct context, IWriteProduct context2,IMediator mediator)
        {
            _context = context;
            _context2 = context2;
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var query = _context.GetAll().ToList();
            return Ok(query);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommanRequest req)
        {
            if (ModelState.IsValid)
            {
                return BadRequest();
            }
            var query = await _mediator.Send(req);            
            return Ok(query);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(RemoveProductCommandRequest ID)
        {
            var query = await _mediator.Send(ID);
            return Ok(query);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Update(UpdateProductCommanRequest req)
        {
            var query = await _mediator.Send(req);
            return Ok(query);
        }
    }
}
