using NUnit.Framework;
using System.IO;
using System.Linq;
using WebCLI.Core.Models.Definitions;
using WebCLI.Core.Repositories;

namespace WebCLI.Core.Tests.Repositories
{
    [TestFixture]
    public class JsonFilePipelineDefinitionRepositoryTests
    {
        private string _testPipelinePath;

        [SetUp]
        public void SetUp()
        {
            _testPipelinePath = Path.Combine(Path.GetTempPath(), "WebCLITestPipelines", Path.GetRandomFileName());
            Directory.CreateDirectory(_testPipelinePath);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_testPipelinePath))
            {
                Directory.Delete(_testPipelinePath, true);
            }
        }

        private void CreatePipelineFile(string fileName, string content)
        {
            File.WriteAllText(Path.Combine(_testPipelinePath, fileName), content);
        }

        [Test]
        public void GetAllPipelineDefinitions_ShouldReturnAllDefinitionsInDirectory()
        {
            // Arrange
            CreatePipelineFile("test1.json", "{\"Name\":\"TestCommand1\", \"Description\":\"Desc1\", \"Type\":\"Command\", \"Pipes\":[]}");
            CreatePipelineFile("test2.json", "{\"Name\":\"TestQuery1\", \"Description\":\"Desc2\", \"Type\":\"Query\", \"Pipes\":[]}");
            var repository = new JsonFilePipelineDefinitionRepository(_testPipelinePath);

            // Act
            var definitions = repository.GetAllPipelineDefinitions().ToList();

            // Assert
            Assert.IsNotNull(definitions);
            Assert.AreEqual(2, definitions.Count);
            Assert.IsTrue(definitions.Any(d => d.Name == "TestCommand1"));
            Assert.IsTrue(definitions.Any(d => d.Name == "TestQuery1"));
        }

        [Test]
        public void GetPipelineDefinition_ShouldReturnCorrectDefinitionByName()
        {
            // Arrange
            CreatePipelineFile("test1.json", "{\"Name\":\"SpecificCommand\", \"Description\":\"Specific Description\", \"Type\":\"Command\", \"Pipes\":[]}");
            CreatePipelineFile("test2.json", "{\"Name\":\"OtherCommand\", \"Description\":\"Other Description\", \"Type\":\"Command\", \"Pipes\":[]}");
            var repository = new JsonFilePipelineDefinitionRepository(_testPipelinePath);

            // Act
            var definition = repository.GetPipelineDefinition("SpecificCommand");

            // Assert
            Assert.IsNotNull(definition);
            Assert.AreEqual("SpecificCommand", definition.Name);
            Assert.AreEqual("Specific Description", definition.Description);
        }

        [Test]
        public void GetPipelineDefinition_ShouldBeCaseInsensitive()
        {
            // Arrange
            CreatePipelineFile("test1.json", "{\"Name\":\"CaseSensitiveTest\", \"Description\":\"Desc\", \"Type\":\"Command\", \"Pipes\":[]}");
            var repository = new JsonFilePipelineDefinitionRepository(_testPipelinePath);

            // Act
            var definition = repository.GetPipelineDefinition("casesensitivetest");

            // Assert
            Assert.IsNotNull(definition);
            Assert.AreEqual("CaseSensitiveTest", definition.Name);
        }

        [Test]
        public void GetPipelineDefinition_ShouldReturnNullIfNotFound()
        {
            // Arrange
            CreatePipelineFile("test1.json", "{\"Name\":\"ExistingCommand\", \"Description\":\"Desc\", \"Type\":\"Command\", \"Pipes\":[]}");
            var repository = new JsonFilePipelineDefinitionRepository(_testPipelinePath);

            // Act
            var definition = repository.GetPipelineDefinition("NonExistentCommand");

            // Assert
            Assert.IsNull(definition);
        }

        [Test]
        public void Constructor_ShouldHandleNonExistentDirectory()
        {
            // Arrange
            var nonExistentPath = Path.Combine(Path.GetTempPath(), "WebCLINonExistent", Path.GetRandomFileName());
            // Act
            var repository = new JsonFilePipelineDefinitionRepository(nonExistentPath);

            // Assert
            Assert.DoesNotThrow(() => repository.GetAllPipelineDefinitions());
            Assert.IsEmpty(repository.GetAllPipelineDefinitions());
        }

        [Test]
        public void Constructor_ShouldHandleEmptyDirectory()
        {
            // Arrange
            var repository = new JsonFilePipelineDefinitionRepository(_testPipelinePath);

            // Act
            var definitions = repository.GetAllPipelineDefinitions().ToList();

            // Assert
            Assert.IsNotNull(definitions);
            Assert.IsEmpty(definitions);
        }

        [Test]
        public void Constructor_ShouldIgnoreInvalidJsonFiles()
        {
            // Arrange
            CreatePipelineFile("valid.json", "{\"Name\":\"ValidCommand\", \"Description\":\"Desc\", \"Type\":\"Command\", \"Pipes\":[]}");
            CreatePipelineFile("invalid.json", "{This is not valid JSON}");
            var repository = new JsonFilePipelineDefinitionRepository(_testPipelinePath);

            // Act
            var definitions = repository.GetAllPipelineDefinitions().ToList();

            // Assert
            Assert.IsNotNull(definitions);
            Assert.AreEqual(1, definitions.Count);
            Assert.AreEqual("ValidCommand", definitions.First().Name);
        }
    }
}
