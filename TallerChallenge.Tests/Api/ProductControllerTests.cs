namespace TallerChallenge.Tests.Api
{
    using Moq;
    using TallerChallenge.Api.Products.Abstractions;
    using TallerChallenge.Api.Products.Controllers;
    using TallerChallenge.Application.Products;

    public class ProductControllerTests
    {
        private readonly Mock<IGetProductQueryHandler> _getProductQueryHandlerMock;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _getProductQueryHandlerMock = new Mock<IGetProductQueryHandler>();
            _controller = new ProductController(_getProductQueryHandlerMock.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnProducts_WhenProductsExist()
        {
            // Arrange
            var request = new GetProductsRequest(null, null, null, null);
            List<GetProductResponse> products =
            [
                new(1, "Product 1", "Description 1", 10),
                new(2, "Product 2", "Description 2", 20)
            ];
            _getProductQueryHandlerMock.Setup(h => h.Handle(request)).ReturnsAsync(products);

            // Act
            var result = await _controller.Get(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _getProductQueryHandlerMock.Verify(h => h.Handle(request), Times.Once);
        }

        [Fact]
        public async Task Get_ShouldReturnEmptyList_WhenNoProductsExist()
        {
            // Arrange
            var request = new GetProductsRequest(null, null, null, null);
            List<GetProductResponse> products = [];
            _getProductQueryHandlerMock.Setup(h => h.Handle(request)).ReturnsAsync(products);

            // Act
            var result = await _controller.Get(request);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _getProductQueryHandlerMock.Verify(h => h.Handle(request), Times.Once);
        }
    }
}
