using NUnit.Framework;
using System;
using WebCLI.Core.Contracts;
using WebCLI.Core.Models.Definitions;
using WebCLI.Core.Pipes;
using WebCLI.Core.Models;

// Define some dummy classes for testing reflection
namespace WebCLI.Core.Tests.Pipes.TestDoubles
{
    public class TestPipe : APipe
    {
        public override async System.Threading.Tasks.Task<ICommandResult> Handle(IPipeContext context)
        {
            return await base.Handle(context);
        }
    }

    public class AnotherTestPipe : APipe { /* ... */ }

    public class NonPipeClass { /* ... */ }

    public class TestPipeContext : APipeContext { /* ... */ }

    public class AnotherTestPipeContext : APipeContext { /* ... */ }

    public class NonPipeContextClass { /* ... */ }
}

namespace WebCLI.Core.Tests.Pipes
{
    using WebCLI.Core.Tests.Pipes.TestDoubles; // Alias for test doubles namespace

    [TestFixture]
    public class ReflectionPipelineFactoryTests
    {
        private ReflectionPipelineFactory _factory;

        [SetUp]
        public void SetUp()
        {
            _factory = new ReflectionPipelineFactory();
        }

        [Test]
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
            Assert.IsInstanceOf<TestPipe>(pipe);
        }

        [Test]
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
            Assert.IsInstanceOf<AnotherTestPipe>(pipe);
        }

        [Test]
        public void CreatePipe_ShouldThrowArgumentNullException_WhenConfigIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _factory.CreatePipe(null));
        }

        [Test]
        public void CreatePipe_ShouldThrowArgumentException_WhenPipeTypeIsNullOrEmpty()
        {
            // Arrange
            var config = new PipeConfiguration { Type = null };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _factory.CreatePipe(config));

            config.Type = " ";
            Assert.Throws<ArgumentException>(() => _factory.CreatePipe(config));
        }

        [Test]
        public void CreatePipe_ShouldThrowInvalidOperationException_WhenPipeTypeNotFound()
        {
            // Arrange
            var config = new PipeConfiguration
            {
                Type = "NonExistentNamespace.NonExistentPipe",
                Assembly = typeof(TestPipe).Assembly.GetName().Name
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _factory.CreatePipe(config));
        }

        [Test]
        public void CreatePipe_ShouldThrowInvalidOperationException_WhenTypeDoesNotImplementIPipe()
        {
            // Arrange
            var config = new PipeConfiguration
            {
                Type = typeof(NonPipeClass).FullName,
                Assembly = typeof(NonPipeClass).Assembly.GetName().Name
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _factory.CreatePipe(config));
        }

        [Test]
        public void CreatePipe_ShouldThrowInvalidOperationException_WhenAssemblyNotFound()
        {
            // Arrange
            var config = new PipeConfiguration
            {
                Type = typeof(TestPipe).FullName,
                Assembly = "NonExistentAssembly"
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _factory.CreatePipe(config));
        }

        [Test]
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
            Assert.IsInstanceOf<TestPipeContext>(context);
        }

        [Test]
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
            Assert.IsInstanceOf<AnotherTestPipeContext>(context);
        }

        [Test]
        public void CreatePipeContext_ShouldThrowArgumentNullException_WhenConfigIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _factory.CreatePipeContext(null));
        }

        [Test]
        public void CreatePipeContext_ShouldThrowArgumentException_WhenContextTypeIsNullOrEmpty()
        {
            // Arrange
            var config = new PipeConfiguration { ContextType = null };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _factory.CreatePipeContext(config));

            config.ContextType = " ";
            Assert.Throws<ArgumentException>(() => _factory.CreatePipeContext(config));
        }

        [Test]
        public void CreatePipeContext_ShouldThrowInvalidOperationException_WhenContextTypeNotFound()
        {
            // Arrange
            var config = new PipeConfiguration
            {
                ContextType = "NonExistentNamespace.NonExistentContext",
                Assembly = typeof(TestPipeContext).Assembly.GetName().Name
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _factory.CreatePipeContext(config));
        }

        [Test]
        public void CreatePipeContext_ShouldThrowInvalidOperationException_WhenTypeDoesNotImplementIPipeContext()
        {
            // Arrange
            var config = new PipeConfiguration
            {
                ContextType = typeof(NonPipeContextClass).FullName,
                Assembly = typeof(NonPipeContextClass).Assembly.GetName().Name
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _factory.CreatePipeContext(config));
        }

        [Test]
        public void CreatePipeContext_ShouldThrowInvalidOperationException_WhenAssemblyNotFound()
        {
            // Arrange
            var config = new PipeConfiguration
            {
                ContextType = typeof(TestPipeContext).FullName,
                Assembly = "NonExistentAssembly"
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _factory.CreatePipeContext(config));
        }
    }
}
