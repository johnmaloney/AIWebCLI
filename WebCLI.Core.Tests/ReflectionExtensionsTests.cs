using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebCLI.Core;
using System.Reflection;

namespace WebCLI.Core.Tests
{
    [TestClass]
    public class ReflectionExtensionsTests
    {
        [TestMethod]
        public void InstantiateObject_ShouldInstantiateTypeSuccessfully_WithAssemblyAndClassName()
        {
            // Arrange
            string typeIdentifier = "WebCLI.Core.Tests, WebCLI.Core.Tests.TestReflectableClass";

            // Act
            TestReflectableClass instance = typeIdentifier.InstantiateObject<TestReflectableClass>();

            // Assert
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof(TestReflectableClass));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void InstantiateObject_ShouldThrowException_WhenTypeIdentifierIsNull()
        {
            // Arrange
            string typeIdentifier = null;

            // Act
            typeIdentifier.InstantiateObject<TestReflectableClass>();

            // Assert: Expected exception
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void InstantiateObject_ShouldThrowException_WhenTypeIdentifierIsWhitespace()
        {
            // Arrange
            string typeIdentifier = " ";

            // Act
            typeIdentifier.InstantiateObject<TestReflectableClass>();

            // Assert: Expected exception
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void InstantiateObject_ShouldThrowException_WhenAssemblyNotFound()
        {
            // Arrange
            string typeIdentifier = "NonExistentAssembly, WebCLI.Core.Tests.TestReflectableClass";

            // Act
            typeIdentifier.InstantiateObject<TestReflectableClass>();

            // Assert: Expected exception
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void InstantiateObject_ShouldThrowException_WhenClassNameNotFound()
        {
            // Arrange
            string typeIdentifier = "WebCLI.Core.Tests, WebCLI.Core.Tests.NonExistentClass";

            // Act
            typeIdentifier.InstantiateObject<TestReflectableClass>();

            // Assert: Expected exception
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void InstantiateObject_ShouldThrowException_WhenCannotCastType()
        {
            // Arrange
            string typeIdentifier = "System.Private.CoreLib, System.String"; // Trying to cast string to TestReflectableClass

            // Act
            typeIdentifier.InstantiateObject<TestReflectableClass>();

            // Assert: Expected exception
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void InstantiateObject_ShouldThrowException_WhenTypeIdentifierIsNotProperlyFormed()
        {
            // Arrange
            string typeIdentifier = "WebCLI.Core.Tests.TestReflectableClass"; // Missing assembly name

            // Act
            typeIdentifier.InstantiateObject<TestReflectableClass>();

            // Assert: Expected exception
        }
    }
}
