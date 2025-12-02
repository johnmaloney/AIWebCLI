using Xunit;
using Moq;
using WebCLI.Core.Contracts;
using WebCLI.Core.Pipes;
using System;
using System.Collections.Generic;

namespace WebCLI.Core.Tests.Pipes
{
    public class PipelineInitializerTests
    {
        [Fact]
        public void PipelineInitializer_CanBeInstantiated()
        {
            // Arrange & Act
            var initializer = new PipelineInitializer();

            // Assert
            Assert.NotNull(initializer);
        }

        [Fact]
        public void PipeInitializer_CanBeSetAndGet()
        {
            // Arrange
            var initializer = new PipelineInitializer();
            var mockPipe = new Mock<IPipe>();
            Func<IPipe> pipeFunc = () => mockPipe.Object;

            // Act
            initializer.PipeInitializer = pipeFunc;

            // Assert
            Assert.Equal(pipeFunc, initializer.PipeInitializer);
            Assert.Equal(mockPipe.Object, initializer.PipeInitializer());
        }

        [Fact]
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
            Assert.Equal(contextFunc, initializer.ContextInitializer);
            Assert.Equal(mockContext.Object, initializer.ContextInitializer("test", null, null, null));
        }
    }
}
