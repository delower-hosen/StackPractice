using Domain.Queries;
using Infrastructure.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    public class ProductQueryController(ILogger<ProductQueryController> logger, IMediator mediator) : ApiControllerBase
    {
        private readonly ILogger<ProductQueryController> _logger = logger;
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }
    }
}
