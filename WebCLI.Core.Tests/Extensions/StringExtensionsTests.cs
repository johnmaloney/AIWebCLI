using Xunit;
using WebCLI.Core;
using System;
using System.Collections.Generic;

namespace WebCLI.Core.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("p:password", ":", "password")]
        [InlineData("key-value", "-", "value")]
        [InlineData("no:split", ":", "split")]
        public void RightSideOf_ReturnsCorrectString(string value, string splitCharacter, string expected)
        {
            // Act
            var result = value.RightSideOf(splitCharacter);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("p:password", ":", "p")]
        [InlineData("key-value", "-", "key")]
        [InlineData("no:split", ":", "no")]
        public void LeftSideOf_ReturnsCorrectString(string value, string splitCharacter, string expected)
        {
            // Act
            var result = value.LeftSideOf(splitCharacter);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Has_ReturnsTrue_WhenKeyExists()
        {
            // Arrange
            dynamic item = new Dictionary<string, object> { { "node1", "value1" } };

            // Act
            var result = StringExtensions.Has(item, "node1");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Has_ReturnsFalse_WhenKeyDoesNotExist()
        {
            // Arrange
            dynamic item = new Dictionary<string, object> { { "node1", "value1" } };

            // Act
            var result = StringExtensions.Has(item, "node2");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Has_ReturnsFalse_WhenItemIsNull()
        {
            // Arrange
            dynamic item = null;

            // Act
            var result = StringExtensions.Has(item, "node1");

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Has_ReturnsFalse_WhenNodeNameIsNullOrEmpty(string nodeName)
        {
            // Arrange
            dynamic item = new Dictionary<string, object> { { "node1", "value1" } };

            // Act
            var result = StringExtensions.Has(item, nodeName);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void HasCollection_ReturnsFalse_WhenItemIsNull()
        {
            // Arrange
            dynamic item = null;

            // Act
            var result = StringExtensions.HasCollection(item, 0);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void HasCollection_ReturnsFalse_WhenLocationIsOutOfRange()
        {
            // Arrange
            dynamic item = new List<object> { "item1", "item2" };

            // Act
            var result = StringExtensions.HasCollection(item, 2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void HasCollection_ReturnsFalse_WhenItemAtLocationIsNull()
        {
            // Arrange
            dynamic item = new List<object> { "item1", null, "item3" };

            // Act
            var result = StringExtensions.HasCollection(item, 1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void HasCollection_ReturnsTrue_WhenItemAtLocationIsNotNull()
        {
            // Arrange
            dynamic item = new List<object> { "item1", "item2", "item3" };

            // Act
            var result = StringExtensions.HasCollection(item, 1);

            // Assert
            Assert.True(result);
        }
    }
}
