using Xunit;
using WebCLI.Core.Models;

namespace WebCLI.Core.Tests.Models
{
    public class QueryTests
    {
        [Fact]
        public void Query_Properties_CanBeSetAndGet()
        {
            // Arrange
            var query = new Query();
            var name = "TestQuery";
            var description = "This is a test query.";

            // Act
            query.Name = name;
            query.Description = description;

            // Assert
            Assert.Equal(name, query.Name);
            Assert.Equal(description, query.Description);
        }

        [Fact]
        public void Query_Constructor_InitializesProperties()
        {
            // Arrange
            var name = "AnotherQuery";
            var description = "Another test description.";

            // Act
            var query = new Query { Name = name, Description = description };

            // Assert
            Assert.Equal(name, query.Name);
            Assert.Equal(description, query.Description);
        }
    }
}
