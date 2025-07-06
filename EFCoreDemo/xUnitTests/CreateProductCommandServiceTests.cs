using Domain.Commands;
using EfCore.PostgreSql.DomainModels.Entities;
using EfCore.PostgreSql.Repositories;
using Moq;
using Services.CommandServices;

namespace xUnitTests
{
    public class CreateProductCommandServiceTests
    {
        [Fact]
        public async Task CreateProductAsync_ShouldAdd_SaveProduct()
        {
            // Arrange
            var productRepoMock = new Mock<IRepository<Product>>();

            var service = new CreateProductCommandService(productRepoMock.Object);

            var command = new CreateProductCommand
            {
                Name = "Laptop",
                Description = "Gaming Laptop",
                Price = 1500,
                Quantity = 3
            };

            // Act
            await service.CreateProductAsync(command);

            // Assert
            productRepoMock.Verify(r => r.AddAsync(It.Is<Product>(p =>
                p.Name == command.Name &&
                p.Description == command.Description &&
                p.Price == command.Price &&
                p.Quantity == command.Quantity
            )), Times.Once);

            productRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
