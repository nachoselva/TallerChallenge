namespace TallerChallenge.Tests.Application
{
    using Moq;
    using TallerChallenge.Application.Products;
    using TallerChallenge.Application.Products.Abstractions;
    using TallerChallenge.Domain;

    public class GetProductsQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly GetProductsQueryHandler _handler;

        public GetProductsQueryHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new GetProductsQueryHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnProducts_WhenProductsExist()
        {
            // Arrange
            var request = new GetProductsRequest(null, null, null, null);
            List<Product> products =
            [
                new(1, "Product 1", "Description 1", 10),
                new(2, "Product 2", "Description 2", 20)
            ];
            _productRepositoryMock.Setup(r => r.GetAllByFilter(request.Name, request.Description, request.PriceFrom, request.PriceTo))
                .ReturnsAsync(products);

            // Act
            var result = await _handler.Handle(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _productRepositoryMock.Verify(r => r.GetAllByFilter(request.Name, request.Description, request.PriceFrom, request.PriceTo), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoProductsExist()
        {
            // Arrange
            var request = new GetProductsRequest(null, null, null, null);
            List<Product> products = [];
            _productRepositoryMock.Setup(r => r.GetAllByFilter(request.Name, request.Description, request.PriceFrom, request.PriceTo))
                .ReturnsAsync(products);

            // Act
            var result = await _handler.Handle(request);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _productRepositoryMock.Verify(r => r.GetAllByFilter(request.Name, request.Description, request.PriceFrom, request.PriceTo), Times.Once);
        }
    }
}
