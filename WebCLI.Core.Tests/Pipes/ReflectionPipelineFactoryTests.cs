using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebCLI.Core.Contracts;
using WebCLI.Core.Models.Definitions;
using WebCLI.Core.Pipes;
using WebCLI.Core.Models;
using System.Threading.Tasks;

// Define some dummy classes for testing reflection
namespace WebCLI.Core.Tests.Pipes.TestDoubles
{
    public class TestPipe : APipe, IPipe
    {
        public override async System.Threading.Tasks.Task<ICommandResult> Handle(IContext context)
        {
            return await base.Handle(context);
        }
    }

    public class AnotherTestPipe : APipe, IPipe { 
        public override async Task<ICommandResult> Handle(IContext context)
        {
            return await base.Handle(context);
        }
    }

    public class NonPipeClass { /* ... */ }

    public class TestPipeContext : APipeContext, IContext // Corrected: implements IContext
    {
        public override void AddMessage(params string[] messages)
        {
            throw new NotImplementedException();
        }
    }

    public class AnotherTestPipeContext : APipeContext, IContext // Corrected: implements IContext
    {
        public override void AddMessage(params string[] messages)
        {
            throw new NotImplementedException();
        }
    }

    public class NonPipeContextClass  // This class intentionally does not implement IContext
    {
        // This class intentionally does not implement IContext
        // to test the exception throwing for invalid context types.
    }
}

namespace WebCLI.Core.Tests.Pipes
{
    using WebCLI.Core.Tests.Pipes.TestDoubles; // Alias for test doubles namespace

    [TestClass]
    public class ReflectionPipelineFactoryTests
    {
        private ReflectionPipelineFactory _factory;

        [TestInitialize]
        public void SetUp()
        {
            _factory = new ReflectionPipelineFactory();
        }

        [TestMethod]
        public void CreatePipe_ShouldCreateInstanceSuccessfully_WhenTypeAndAssemblyAreValid()
        {
            // Arrange
            var config = new PipeConfiguration
            {
                Type = typeof(TestPipe).FullName,
                Assembly = typeof(TestPipe).Assembly.GetName().Name
            };

            // Act
            var pipe = _factory.CreatePipe(config);

            // Assert
            Assert.IsNotNull(pipe);
            Assert.IsInstanceOfType(pipe, typeof(TestPipe));
        }

        [TestMethod]
        public void CreatePipe_ShouldCreateInstanceSuccessfully_WhenOnlyTypeIsValidAndInLoadedAssemblies()
        {
            // Arrange
            var config = new PipeConfiguration
            {
                Type = typeof(AnotherTestPipe).FullName,
                // Assembly is intentionally left null to test broad search
            };

            // Act
            var pipe = _factory.CreatePipe(config);

            // Assert
            Assert.IsNotNull(pipe);
            Assert.IsInstanceOfType(pipe, typeof(AnotherTestPipe));
        }

        [TestMethod]
        public void CreatePipe_ShouldThrowArgumentNullException_WhenConfigIsNull()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _factory.CreatePipe(null));
        }

        [TestMethod]

        public void CreatePipe_ShouldThrowArgumentException_WhenPipeTypeIsNullOrEmpty()

        {

            // Arrange

            var config = new PipeConfiguration { Type = null };

            // Act & Assert

            Assert.ThrowsException<ArgumentException>(() => _factory.CreatePipe(config));

            config.Type = " ";

            Assert.ThrowsException<ArgumentException>(() => _factory.CreatePipe(config));

        }

        [TestMethod]

        public void CreatePipe_ShouldThrowInvalidOperationException_WhenPipeTypeNotFound()

        {

            // Arrange

            var config = new PipeConfiguration

            {

                Type = "NonExistentNamespace.NonExistentPipe",

                Assembly = typeof(TestPipe).Assembly.GetName().Name

            };

            // Act & Assert

            Assert.ThrowsException<InvalidOperationException>(() => _factory.CreatePipe(config));

        }

        [TestMethod]

        public void CreatePipe_ShouldThrowInvalidOperationException_WhenTypeDoesNotImplementIPipe()

        {

            // Arrange

            var config = new PipeConfiguration

            {

                Type = typeof(NonPipeClass).FullName,

                Assembly = typeof(NonPipeClass).Assembly.GetName().Name

            };

            // Act & Assert

            Assert.ThrowsException<InvalidOperationException>(() => _factory.CreatePipe(config));

        }

        [TestMethod]

        public void CreatePipe_ShouldThrowInvalidOperationException_WhenAssemblyNotFound()

        {

            // Arrange

            var config = new PipeConfiguration

            {

                Type = typeof(TestPipe).FullName,

                Assembly = "NonExistentAssembly" 

            };

            // Act & Assert

            Assert.ThrowsException<InvalidOperationException>(() => _factory.CreatePipe(config));

        }

        [TestMethod]

        public void CreatePipeContext_ShouldCreateInstanceSuccessfully_WhenContextTypeAndAssemblyAreValid()

        {

            // Arrange

            var config = new PipeConfiguration

            {

                ContextType = typeof(TestPipeContext).FullName,

                Assembly = typeof(TestPipeContext).Assembly.GetName().Name

            };

            // Act

            var context = _factory.CreatePipeContext(config);

            // Assert

            Assert.IsNotNull(context);

            Assert.IsInstanceOfType(context, typeof(TestPipeContext));

        }

        [TestMethod]

        public void CreatePipeContext_ShouldCreateInstanceSuccessfully_WhenOnlyContextTypeIsValidAndInLoadedAssemblies()

        {

            // Arrange

            var config = new PipeConfiguration

            {

                ContextType = typeof(AnotherTestPipeContext).FullName,

                // Assembly is intentionally left null

            };

            // Act

            var context = _factory.CreatePipeContext(config);

            // Assert

            Assert.IsNotNull(context);

            Assert.IsInstanceOfType(context, typeof(AnotherTestPipeContext));

        }

        [TestMethod]

        public void CreatePipeContext_ShouldThrowArgumentNullException_WhenConfigIsNull()

        {

            // Act & Assert

            Assert.ThrowsException<ArgumentNullException>(() => _factory.CreatePipeContext(null));

        }

        [TestMethod]

        public void CreatePipeContext_ShouldThrowArgumentException_WhenContextTypeIsNullOrEmpty()

        {

            // Arrange

            var config = new PipeConfiguration { ContextType = null };

            // Act & Assert

            Assert.ThrowsException<ArgumentException>(() => _factory.CreatePipeContext(config));

            config.ContextType = " ";

            Assert.ThrowsException<ArgumentException>(() => _factory.CreatePipeContext(config));

        }

        [TestMethod]

        public void CreatePipeContext_ShouldThrowInvalidOperationException_WhenContextTypeNotFound()

        {

            // Arrange

            var config = new PipeConfiguration

            {

                ContextType = "NonExistentNamespace.NonExistentContext",

                Assembly = typeof(TestPipeContext).Assembly.GetName().Name

            };

            // Act & Assert

            Assert.ThrowsException<InvalidOperationException>(() => _factory.CreatePipeContext(config));

        }

        [TestMethod]

        public void CreatePipeContext_ShouldThrowInvalidOperationException_WhenTypeDoesNotImplementIContext()

        {

            // Arrange

            var config = new PipeConfiguration

            {
                ContextType = typeof(NonPipeContextClass).FullName,
                Assembly = typeof(NonPipeContextClass).Assembly.GetName().Name
            };

            // Act & Assert - Expecting an InvalidOperationException
            Assert.ThrowsException<InvalidOperationException>(() => _factory.CreatePipeContext(config));
        }

        [TestMethod]
        public void CreatePipeContext_ShouldThrowInvalidOperationException_WhenAssemblyNotFound()
        {
            // Arrange
            var config = new PipeConfiguration
            {
                ContextType = typeof(TestPipeContext).FullName,
                Assembly = "NonExistentAssembly"
            };

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _factory.CreatePipeContext(config));
        }
    }
}
