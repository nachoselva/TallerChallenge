namespace TallerChallenge.Tests.Domain
{
    using TallerChallenge.Domain;

    public class ProductTests
    {
        [Fact]
        public void Product_Properties_CanBeSetAndGet()
        {
            // Arrange
            var product = new Product(1, "Test Product", "Test Description", 10.0m);

            // Assert
            Assert.Equal(1, product.Id);
            Assert.Equal("Test Product", product.Name);
            Assert.Equal("Test Description", product.Description);
            Assert.Equal(10.0m, product.Price);
        }
    }
}
