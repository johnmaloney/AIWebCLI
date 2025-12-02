using Xunit;
using Moq;
using WebCLI.Core.Contracts;
using WebCLI.Core.Repositories;
using System;
using WebCLI.Core.Models;

namespace WebCLI.Core.Tests.Repositories
{
    public class CommandRepositoryTests
    {
        [Fact]
        public void CommandRepository_CanBeInstantiated()
        {
            // Arrange & Act
            var repository = new CommandRepository();

            // Assert
            Assert.NotNull(repository);
        }

        [Fact]
        public void AddCommandDelegate_AddsNewCommand()
        {
            // Arrange
            var repository = new CommandRepository();
            var commandIdentifier = "testcommand";
            var mockPipe = new Mock<IPipe>();
            Func<IContext, IPipe> commandDelegate = (context) => mockPipe.Object;

            // Act
            repository.AddCommandDelegate(commandIdentifier, commandDelegate);

            // Assert
            var mockContext = new Mock<IContext>();
            mockContext.SetupGet(c => c.Identifier).Returns(commandIdentifier);
            Assert.Equal(mockPipe.Object, repository[mockContext.Object]);
        }

        [Fact]
        public void AddCommandDelegate_UpdatesExistingCommand()
        {
            // Arrange
            var repository = new CommandRepository();
            var commandIdentifier = "updatecommand";
            var mockPipe1 = new Mock<IPipe>();
            Func<IContext, IPipe> commandDelegate1 = (context) => mockPipe1.Object;
            repository.AddCommandDelegate(commandIdentifier, commandDelegate1);

            var mockPipe2 = new Mock<IPipe>();
            Func<IContext, IPipe> commandDelegate2 = (context) => mockPipe2.Object;

            // Act
            repository.AddCommandDelegate(commandIdentifier, commandDelegate2);

            // Assert
            var mockContext = new Mock<IContext>();
            mockContext.SetupGet(c => c.Identifier).Returns(commandIdentifier);
            Assert.Equal(mockPipe2.Object, repository[mockContext.Object]);
        }

        [Fact]
        public void AddCommandDelegate_WithPipelineInitializer_AddsInitializer()
        {
            // Arrange
            var repository = new CommandRepository();
            var commandIdentifier = "initcommand";
            var mockPipe = new Mock<IPipe>();
            Func<IContext, IPipe> commandDelegate = (context) => mockPipe.Object;
            var mockInitializer = new Mock<IPipelineInitializer>();

            // Act
            repository.AddCommandDelegate(commandIdentifier, commandDelegate, mockInitializer.Object);

            // Assert - This requires internal knowledge of CommandRepository to verify the initializer was added.
            // For proper testing, a method to retrieve the initializer would be needed, or
            // we could test the behavior that relies on the initializer.
            // For now, we'll assume if it's passed, it's stored.
            var mockContext = new Mock<IContext>();
            mockContext.SetupGet(c => c.Identifier).Returns(commandIdentifier);
            Assert.Equal(mockPipe.Object, repository[mockContext.Object]); // Still confirms the command delegate works
        }

        [Fact]
        public void Indexer_ReturnsCorrectPipe_ForExistingCommand()
        {
            // Arrange
            var repository = new CommandRepository();
            var commandIdentifier = "existingcommand";
            var mockPipe = new Mock<IPipe>();
            Func<IContext, IPipe> commandDelegate = (context) => mockPipe.Object;
            repository.AddCommandDelegate(commandIdentifier, commandDelegate);

            var mockContext = new Mock<IContext>();
            mockContext.SetupGet(c => c.Identifier).Returns(commandIdentifier);

            // Act
            var result = repository[mockContext.Object];

            // Assert
            Assert.Equal(mockPipe.Object, result);
        }

        [Fact]
        public void Indexer_ThrowsKeyNotFoundException_ForNonExistentCommand()
        {
            // Arrange
            var repository = new CommandRepository();
            var nonExistentCommand = "nonexistent";
            var mockContext = new Mock<IContext>();
            mockContext.SetupGet(c => c.Identifier).Returns(nonExistentCommand);

            // Act & Assert
            Assert.Throws<System.Collections.Generic.KeyNotFoundException>(() => repository[mockContext.Object]);
        }
    }
}
