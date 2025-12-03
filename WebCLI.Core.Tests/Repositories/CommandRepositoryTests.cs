using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebCLI.Core.Contracts;
using WebCLI.Core.Repositories;
using System;
using WebCLI.Core.Models;
using System.Collections.Generic;

namespace WebCLI.Core.Tests.Repositories
{
    [TestClass]
    public class CommandRepositoryTests
    {
        [TestMethod]
        public void CommandRepository_CanBeInstantiated()
        {
            // Arrange & Act
            var repository = new CommandRepository();

            // Assert
            Assert.IsNotNull(repository);
        }

        [TestMethod]
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
            Assert.AreEqual(mockPipe.Object, repository[mockContext.Object]);
        }

        [TestMethod]
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
            Assert.AreEqual(mockPipe2.Object, repository[mockContext.Object]);
        }

        [TestMethod]
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

            // Assert
            var mockContext = new Mock<IContext>();
            mockContext.SetupGet(c => c.Identifier).Returns(commandIdentifier);
            Assert.AreEqual(mockPipe.Object, repository[mockContext.Object]); 
        }

        [TestMethod]
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
            Assert.AreEqual(mockPipe.Object, result);
        }

        [TestMethod]
        public void Indexer_ThrowsKeyNotFoundException_ForNonExistentCommand()
        {
            // Arrange
            var repository = new CommandRepository();
            var nonExistentCommand = "nonexistent";
            var mockContext = new Mock<IContext>();
            mockContext.SetupGet(c => c.Identifier).Returns(nonExistentCommand);

            // Act & Assert
            Assert.ThrowsException<KeyNotFoundException>(() => repository[mockContext.Object]);
        }
    }
}
