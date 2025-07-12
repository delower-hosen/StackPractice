using Domain.Queries;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using MediatR;

namespace Domain.QueryHandlers
{
    public class GetAllProductsQueryHandler(IRepository<Product> productRepository) : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IRepository<Product> _productRepository = productRepository;

        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllAsync();
        }
    }
}