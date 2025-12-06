using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCLI.Core;

namespace WebCLI.Core.Tests
{
    [TestClass]
    public class TestReflectableClassTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializeMessageToDefault()
        {
            // Arrange & Act
            var instance = new TestReflectableClass();

            // Assert
            Assert.AreEqual("Default Message", instance.Message);
        }

        [TestMethod]
        public void Message_SetAndGet_ShouldWorkCorrectly()
        {
            // Arrange
            var instance = new TestReflectableClass();
            var testMessage = "Hello from Test!";

            // Act
            instance.Message = testMessage;

            // Assert
            Assert.AreEqual(testMessage, instance.Message);
        }
    }
}
