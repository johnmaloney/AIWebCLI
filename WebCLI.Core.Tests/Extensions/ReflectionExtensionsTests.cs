using Xunit;
using WebCLI.Core;
using System;
using System.Reflection;

namespace WebCLI.Core.Tests.Extensions
{
    public class ReflectionExtensionsTests
    {
        [Fact]
        public void InstantiateObject_ReturnsCorrectType_ForValidTypeIdentifier()
        {
            // Arrange
            var typeIdentifier = $"{Assembly.GetExecutingAssembly().GetName().Name}, {typeof(TestReflectableClass).FullName}";

            // Act
            var instance = typeIdentifier.InstantiateObject<TestReflectableClass>();

            // Assert
            Assert.NotNull(instance);
            Assert.IsType<TestReflectableClass>(instance);
        }

        [Fact]
        public void InstantiateObject_ThrowsNotSupportedException_ForInvalidAssemblyName()
        {
            // Arrange
            var typeIdentifier = "NonExistentAssembly, WebCLI.Core.Tests.TestReflectableClass";

            // Act & Assert
            var ex = Assert.Throws<NotSupportedException>(() => typeIdentifier.InstantiateObject<TestReflectableClass>());
            Assert.Contains("The Assembly Name given was not found.", ex.Message);
            Assert.NotNull(ex.InnerException); // Should have an inner exception from Activator.CreateInstance
        }

        [Fact]
        public void InstantiateObject_ThrowsNotSupportedException_ForIncorrectClassName()
        {
            // Arrange
            var typeIdentifier = $"{Assembly.GetExecutingAssembly().GetName().Name}, WebCLI.Core.Tests.NonExistentClass";

            // Act & Assert
            var ex = Assert.Throws<NotSupportedException>(() => typeIdentifier.InstantiateObject<TestReflectableClass>());
            Assert.Contains("The Assembly Name given was not found.", ex.Message); // Activator.CreateInstance throws if class not found too.
            Assert.NotNull(ex.InnerException);
        }

        [Fact]
        public void InstantiateObject_ThrowsNotSupportedException_ForIncorrectCast()
        {
            // Arrange
            var typeIdentifier = $"{Assembly.GetExecutingAssembly().GetName().Name}, {typeof(TestReflectableClass).FullName}";

            // Act & Assert
            var ex = Assert.Throws<NotSupportedException>(() => typeIdentifier.InstantiateObject<string>());
            Assert.Contains("cannot be cast to", ex.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("WebCLI.Core.Tests")] // Missing comma and class name
        [InlineData("WebCLI.Core.Tests,")] // Missing class name
        [InlineData(",WebCLI.Core.Tests.TestReflectableClass")] // Missing assembly name
        public void InstantiateObject_ThrowsNotSupportedException_ForImproperlyFormedIdentifier(string typeIdentifier)
        {
            // Act & Assert
            var ex = Assert.Throws<NotSupportedException>(() => typeIdentifier.InstantiateObject<TestReflectableClass>());
            Assert.Contains("The type reference", ex.Message);
            Assert.Contains("is not properly formed", ex.Message);
        }
    }
}
