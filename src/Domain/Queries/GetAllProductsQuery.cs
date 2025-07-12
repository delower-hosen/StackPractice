using Infrastructure.Entities;
using MediatR;

namespace Domain.Queries
{
    public record GetAllProductsQuery() : IRequest<IEnumerable<Product>>;
}
