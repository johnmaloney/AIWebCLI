using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCLI.Core;
using System.Dynamic;

namespace WebCLI.Core.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void RightSideOf_ShouldReturnCorrectSubstring_WhenSplitCharacterExists()
        {
            // Arrange
            string value = "p:password";
            string splitCharacter = ":";

            // Act
            string result = value.RightSideOf(splitCharacter);

            // Assert
            Assert.AreEqual("password", result);
        }

        [TestMethod]
        public void RightSideOf_ShouldReturnEmptyString_WhenSplitCharacterIsAtEnd()
        {
            // Arrange
            string value = "key:";
            string splitCharacter = ":";

            // Act
            string result = value.RightSideOf(splitCharacter);

            // Assert
            Assert.AreEqual("", result);
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void RightSideOf_ShouldThrowException_WhenSplitCharacterDoesNotExist()
        {
            // Arrange
            string value = "nopassword";
            string splitCharacter = ":";

            // Act
            value.RightSideOf(splitCharacter);
        }

        [TestMethod]
        public void RightSideOf_ShouldReturnCorrectSubstring_WhenMultipleSplitCharactersExist()
        {
            // Arrange
            string value = "first:second:third";
            string splitCharacter = ":";

            // Act
            string result = value.RightSideOf(splitCharacter);

            // Assert
            Assert.AreEqual("second:third", result);
        }

        [TestMethod]
        public void LeftSideOf_ShouldReturnCorrectSubstring_WhenSplitCharacterExists()
        {
            // Arrange
            string value = "p:password";
            string splitCharacter = ":";

            // Act
            string result = value.LeftSideOf(splitCharacter);

            // Assert
            Assert.AreEqual("p", result);
        }

        [TestMethod]
        public void LeftSideOf_ShouldReturnFullString_WhenSplitCharacterDoesNotExist()
        {
            // Arrange
            string value = "nopassword";
            string splitCharacter = ":";

            // Act
            string result = value.LeftSideOf(splitCharacter);

            // Assert
            Assert.AreEqual("nopassword", result);
        }

        [TestMethod]
        public void LeftSideOf_ShouldReturnEmptyString_WhenSplitCharacterIsAtBeginning()
        {
            // Arrange
            string value = ":password";
            string splitCharacter = ":";

            // Act
            string result = value.LeftSideOf(splitCharacter);

            // Assert
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void Has_ShouldReturnTrue_WhenNodeExists()
        {
            // Arrange
            dynamic item = new ExpandoObject();
            item.Name = "Test";

            // Act
            bool result = StringExtensions.Has(item, "Name");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Has_ShouldReturnFalse_WhenNodeDoesNotExist()
        {
            // Arrange
            dynamic item = new ExpandoObject();
            item.Age = 30;

            // Act
            bool result = StringExtensions.Has(item, "Name");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Has_ShouldReturnFalse_WhenItemIsNull()
        {
            // Arrange
            dynamic item = null;

            // Act
            bool result = StringExtensions.Has(item, "Name");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Has_ShouldReturnFalse_WhenNodeNameIsNullOrEmpty()
        {
            // Arrange
            dynamic item = new ExpandoObject();
            item.Name = "Test";

            // Act & Assert
            Assert.IsFalse(StringExtensions.Has(item, null));
            Assert.IsFalse(StringExtensions.Has(item, string.Empty));
        }

        [TestMethod]
        public void HasCollection_ShouldReturnTrue_WhenCollectionExistsAndLocationIsValid()
        {
            // Arrange
            dynamic item = new ExpandoObject();
            item.Items = new string[] { "One", "Two" };

            // Act
            bool result = StringExtensions.HasCollection(item.Items, 0);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HasCollection_ShouldReturnFalse_WhenCollectionExistsAndLocationIsInvalid()
        {
            // Arrange
            dynamic item = new ExpandoObject();
            item.Items = new string[] { "One", "Two" };

            // Act
            bool result = StringExtensions.HasCollection(item.Items, 2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasCollection_ShouldReturnFalse_WhenItemIsNull()
        {
            // Arrange
            dynamic item = null;

            // Act
            bool result = StringExtensions.HasCollection(item, 0);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasCollection_ShouldReturnFalse_WhenItemIsNotACollection()
        {
            // Arrange
            dynamic item = new ExpandoObject();
            item.Name = "Test";

            // Act
            bool result = StringExtensions.HasCollection(item, 0);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasCollection_ShouldReturnFalse_WhenCollectionIsEmpty()
        {
            // Arrange
            dynamic item = new ExpandoObject();
            item.Items = new string[] { };

            // Act
            bool result = StringExtensions.HasCollection(item.Items, 0);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
