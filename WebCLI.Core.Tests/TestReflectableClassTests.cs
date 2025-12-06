using NUnit.Framework;
using WebCLI.Core;

namespace WebCLI.Core.Tests
{
    [TestFixture]
    public class TestReflectableClassTests
    {
        [Test]
        public void Constructor_ShouldInitializeMessageToDefault()
        {
            // Arrange & Act
            var instance = new TestReflectableClass();

            // Assert
            Assert.AreEqual("Default Message", instance.Message);
        }

        [Test]
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
