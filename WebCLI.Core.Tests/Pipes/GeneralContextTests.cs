using Xunit;
using WebCLI.Core.Pipes;
using WebCLI.Core.Contracts;
using System.Collections.Generic;
using Moq;

namespace WebCLI.Core.Tests.Pipes
{
    public class GeneralContextTests
    {
        [Fact]
        public void GeneralContext_CanBeInstantiated()
        {
            // Arrange & Act
            var context = new GeneralContext("test", null, null, null);

            // Assert
            Assert.NotNull(context);
        }

        [Fact]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            // Arrange
            var identifier = "testIdentifier";
            var mockAuthContext = new Mock<IAuthContext>();
            var parameters = new string[] { "param1", "param2" };
            var args = new Dictionary<string, object> { { "key1", "value1" } };

            // Act
            var context = new GeneralContext(identifier, mockAuthContext.Object, parameters, args);

            // Assert
            Assert.Equal(identifier, context.Identifier);
            Assert.Equal(mockAuthContext.Object, context.AuthContext);
            Assert.Equal(parameters, context.Parameters);
            Assert.Equal(args, context.Args);
        }

        [Fact]
        public void Set_And_Get_WorksForValidKey()
        {
            // Arrange
            var context = new GeneralContext("test", null, null, null);
            var key = "testKey";
            var value = "testValue";

            // Act
            context.Set(key, value);
            var retrievedValue = context.Get<string>(key);

            // Assert
            Assert.Equal(value, retrievedValue);
        }

        [Fact]
        public void Get_ReturnsDefaultValue_ForNonExistentKey()
        {
            // Arrange
            var context = new GeneralContext("test", null, null, null);
            var key = "nonExistentKey";

            // Act
            var retrievedValue = context.Get<string>(key);

            // Assert
            Assert.Null(retrievedValue);
        }

        [Fact]
        public void Get_ReturnsCorrectType()
        {
            // Arrange
            var context = new GeneralContext("test", null, null, null);
            var key = "intValue";
            var value = 123;

            // Act
            context.Set(key, value);
            var retrievedValue = context.Get<int>(key);

            // Assert
            Assert.Equal(value, retrievedValue);
        }

        [Fact]
        public void Get_ThrowsInvalidCastException_ForIncorrectType()
        {
            // Arrange
            var context = new GeneralContext("test", null, null, null);
            var key = "stringValue";
            var value = "hello";
            context.Set(key, value);

            // Act & Assert
            Assert.Throws<System.InvalidCastException>(() => context.Get<int>(key));
        }
    }
}
