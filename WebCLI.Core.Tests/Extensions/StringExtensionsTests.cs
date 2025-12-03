
using WebCLI.Core;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebCLI.Core.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [TestMethod]
        [DataRow("p:password", ":", "password")]
        [DataRow("key-value", "-", "value")]
        [DataRow("no:split", ":", "split")]
        public void RightSideOf_ReturnsCorrectString(string value, string splitCharacter, string expected)
        {
            // Act
            var result = value.RightSideOf(splitCharacter);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow("p:password", ":", "p")]
        [DataRow("key-value", "-", "key")]
        [DataRow("no:split", ":", "no")]
        public void LeftSideOf_ReturnsCorrectString(string value, string splitCharacter, string expected)
        {
            // Act
            var result = value.LeftSideOf(splitCharacter);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Has_ReturnsTrue_WhenKeyExists()
        {
            // Arrange
            dynamic item = new Dictionary<string, object> { { "node1", "value1" } };

            // Act
            var result = StringExtensions.Has(item, "node1");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Has_ReturnsFalse_WhenKeyDoesNotExist()
        {
            // Arrange
            dynamic item = new Dictionary<string, object> { { "node1", "value1" } };

            // Act
            var result = StringExtensions.Has(item, "node2");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Has_ReturnsFalse_WhenItemIsNull()
        {
            // Arrange
            dynamic item = null;

            // Act
            var result = StringExtensions.Has(item, "node1");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void Has_ReturnsFalse_WhenNodeNameIsNullOrEmpty(string nodeName)
        {
            // Arrange
            dynamic item = new Dictionary<string, object> { { "node1", "value1" } };

            // Act
            var result = StringExtensions.Has(item, nodeName);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasCollection_ReturnsFalse_WhenItemIsNull()
        {
            // Arrange
            dynamic item = null;

            // Act
            var result = StringExtensions.HasCollection(item, 0);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasCollection_ReturnsFalse_WhenLocationIsOutOfRange()
        {
            // Arrange
            dynamic item = new List<object> { "item1", "item2" };

            // Act
            var result = StringExtensions.HasCollection(item, 2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasCollection_ReturnsFalse_WhenItemAtLocationIsNull()
        {
            // Arrange
            dynamic item = new List<object> { "item1", null, "item3" };

            // Act
            var result = StringExtensions.HasCollection(item, 1);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasCollection_ReturnsTrue_WhenItemAtLocationIsNotNull()
        {
            // Arrange
            dynamic item = new List<object> { "item1", "item2", "item3" };

            // Act
            var result = StringExtensions.HasCollection(item, 1);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
