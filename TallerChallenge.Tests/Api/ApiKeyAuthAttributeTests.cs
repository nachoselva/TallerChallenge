namespace TallerChallenge.Tests.Api
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using TallerChallenge.Api;

    public class ApiKeyAuthAttributeTests
    {
        private readonly ApiKeyAuthAttribute _attribute;
        private readonly Mock<IConfiguration> _configurationMock;

        public ApiKeyAuthAttributeTests()
        {
            _attribute = new ApiKeyAuthAttribute();
            _configurationMock = new Mock<IConfiguration>();
        }

        private ActionExecutingContext CreateActionExecutingContext(IHeaderDictionary headers)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IConfiguration))).Returns(_configurationMock.Object);

            var httpContext = new DefaultHttpContext
            {
                RequestServices = serviceProviderMock.Object
            };
            foreach (var header in headers)
            {
                httpContext.Request.Headers[header.Key] = header.Value;
            }

            var actionContext = new ActionContext(
                httpContext,
                new RouteData(),
                new ActionDescriptor()
            );

            return new ActionExecutingContext(
                actionContext,
                [],
                new Dictionary<string, object?>(),
                new Mock<ControllerBase>().Object
            );
        }

        [Fact]
        public async Task OnActionExecutionAsync_ShouldReturnUnauthorized_WhenApiKeyIsMissing()
        {
            // Arrange
            var context = CreateActionExecutingContext(new HeaderDictionary());
            var next = new Mock<ActionExecutionDelegate>().Object;

            // Act
            await _attribute.OnActionExecutionAsync(context, next);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(context.Result);
            Assert.Equal("API Key missing.", ((UnauthorizedObjectResult)context.Result).Value);
        }

        [Fact]
        public async Task OnActionExecutionAsync_ShouldReturnUnauthorized_WhenApiKeyIsInvalid()
        {
            // Arrange
            var headers = new HeaderDictionary
            {
                { "X-API-KEY", "invalid-key" }
            };
            var context = CreateActionExecutingContext(headers);
            _configurationMock.Setup(c => c["ApiKey"]).Returns("valid-key");
            var next = new Mock<ActionExecutionDelegate>().Object;

            // Act
            await _attribute.OnActionExecutionAsync(context, next);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(context.Result);
            Assert.Equal("Invalid API Key.", ((UnauthorizedObjectResult)context.Result).Value);
        }

        [Fact]
        public async Task OnActionExecutionAsync_ShouldCallNext_WhenApiKeyIsValid()
        {
            // Arrange
            var headers = new HeaderDictionary
            {
                { "X-API-KEY", "valid-key" }
            };
            var context = CreateActionExecutingContext(headers);
            _configurationMock.Setup(c => c["ApiKey"]).Returns("valid-key");
            var nextMock = new Mock<ActionExecutionDelegate>();
            nextMock.Setup(n => n()).ReturnsAsync(new ActionExecutedContext(context, [], null));

            // Act
            await _attribute.OnActionExecutionAsync(context, nextMock.Object);

            // Assert
            Assert.Null(context.Result);
            nextMock.Verify(n => n(), Times.Once);
        }
    }
}
