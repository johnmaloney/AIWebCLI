using Xunit;
using WebCLI.Core.Models;

namespace WebCLI.Core.Tests.Models
{
    public class CriteriaTests
    {
        [Fact]
        public void Criteria_Properties_CanBeSetAndGet()
        {
            // Arrange
            var criteria = new Criteria();
            var identifier = "TestIdentifier";
            var parameters = new[] { "param1", "param2" };

            // Act
            criteria.Identifier = identifier;
            criteria.Parameters = parameters;

            // Assert
            Assert.Equal(identifier, criteria.Identifier);
            Assert.Equal(parameters, criteria.Parameters);
        }

        [Fact]
        public void Criteria_Constructor_InitializesProperties()
        {
            // Arrange
            var identifier = "AnotherIdentifier";
            var parameters = new[] { "paramA", "paramB" };

            // Act
            var criteria = new Criteria { Identifier = identifier, Parameters = parameters };

            // Assert
            Assert.Equal(identifier, criteria.Identifier);
            Assert.Equal(parameters, criteria.Parameters);
        }
    }
}
