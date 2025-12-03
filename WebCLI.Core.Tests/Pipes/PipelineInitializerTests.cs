using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebCLI.Core.Contracts;
using WebCLI.Core.Pipes;
using System;
using System.Collections.Generic;

namespace WebCLI.Core.Tests.Pipes
{
    [TestClass]
    public class PipelineInitializerTests
    {
        [TestMethod]
        public void PipelineInitializer_CanBeInstantiated()
        {
            // Arrange & Act
            var initializer = new PipelineInitializer();

            // Assert
            Assert.IsNotNull(initializer);
        }

        [TestMethod]
        public void PipeInitializer_CanBeSetAndGet()
        {
            // Arrange
            var initializer = new PipelineInitializer();
            var mockPipe = new Mock<IPipe>();
            Func<IPipe> pipeFunc = () => mockPipe.Object;

            // Act
            initializer.PipeInitializer = pipeFunc;

            // Assert
            Assert.AreEqual(pipeFunc, initializer.PipeInitializer);
            Assert.AreEqual(mockPipe.Object, initializer.PipeInitializer());
        }

        [TestMethod]
        public void ContextInitializer_CanBeSetAndGet()
        {
            // Arrange
            var initializer = new PipelineInitializer();
            var mockContext = new Mock<IContext>();
            Func<string, object, string[], Dictionary<string, object>, IContext> contextFunc = 
                (identifier, authContext, parameters, args) => mockContext.Object;

            // Act
            initializer.ContextInitializer = contextFunc;

            // Assert
            Assert.AreEqual(contextFunc, initializer.ContextInitializer);
            Assert.AreEqual(mockContext.Object, initializer.ContextInitializer("test", null, null, null));
        }
    }
}
