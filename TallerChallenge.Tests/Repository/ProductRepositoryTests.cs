namespace TallerChallenge.Tests.Repository
{
    using TallerChallenge.Repository.Products;

    public class ProductRepositoryTests
    {
        private readonly ProductRepository _repository;

        public ProductRepositoryTests()
        {
            _repository = new ProductRepository();
        }

        [Fact]
        public async Task GetAllByFilter_ShouldReturnAllProducts_WhenNoFiltersAreApplied()
        {
            // Act
            var result = await _repository.GetAllByFilter(null, null, null, null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetAllByFilter_ShouldReturnFilteredProducts_WhenNameFilterIsApplied()
        {
            // Act
            var result = await _repository.GetAllByFilter("Product 1", null, null, null);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Product 1", result.First().Name);
        }

        [Fact]
        public async Task GetAllByFilter_ShouldReturnFilteredProducts_WhenDescriptionFilterIsApplied()
        {
            // Act
            var result = await _repository.GetAllByFilter(null, "Description 2", null, null);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Description 2", result.First().Description);
        }

        [Fact]
        public async Task GetAllByFilter_ShouldReturnFilteredProducts_WhenPriceFromFilterIsApplied()
        {
            // Act
            var result = await _repository.GetAllByFilter(null, null, 25, null);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetAllByFilter_ShouldReturnFilteredProducts_WhenPriceToFilterIsApplied()
        {
            // Act
            var result = await _repository.GetAllByFilter(null, null, null, 25);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllByFilter_ShouldReturnFilteredProducts_WhenAllFiltersAreApplied()
        {
            // Act
            var result = await _repository.GetAllByFilter("Product 3", "Description 3", 25, 35);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Product 3", result.First().Name);
        }

        [Fact]
        public async Task GetAllByFilter_ShouldReturnEmptyList_WhenNoProductsMatchFilters()
        {
            // Act
            var result = await _repository.GetAllByFilter("Non-existent product", null, null, null);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
