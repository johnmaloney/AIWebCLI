using WebCLI.Core.Models;
using Xunit;

namespace WebCLI.Core.Tests.Models
{
    public class CommandTests
    {
        [Fact]
        public void Command_Properties_CanBeSetAndGet()
        {
            // Arrange
            var command = new Command();
            var name = "TestCommand";
            var description = "This is a test command.";

            // Act
            command.Name = name;
            command.Descriptor = description;

            // Assert
            Assert.Equal(name, command.Name);
            Assert.Equal(description, command.Descriptor);
        }

        [Fact]
        public void Command_Constructor_InitializesProperties()
        {
            // Arrange
            var name = "AnotherCommand";
            var description = "Another test description.";

            // Act
            var command = new Command { Name = name, Descriptor = description };

            // Assert
            Assert.Equal(name, command.Name);
            Assert.Equal(description, command.Descriptor);
        }
    }
}
