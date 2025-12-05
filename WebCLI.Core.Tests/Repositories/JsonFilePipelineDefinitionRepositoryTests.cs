using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using WebCLI.Core.Models.Definitions;
using WebCLI.Core.Repositories;
using Newtonsoft.Json;

namespace WebCLI.Core.Tests.Repositories
{
    [TestClass]
    public class JsonFilePipelineDefinitionRepositoryTests
    {
        private string _testPipelinePath;

        [TestInitialize]
        public void SetUp()
        {
            _testPipelinePath = Path.Combine(Path.GetTempPath(), "WebCLITestPipelines", Path.GetRandomFileName());
            Directory.CreateDirectory(_testPipelinePath);
        }

        [TestCleanup]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        public void Constructor_ShouldHandleNonExistentDirectory()
        {
            // Arrange
            var nonExistentPath = Path.Combine(Path.GetTempPath(), "WebCLINonExistent", Path.GetRandomFileName());
            var repository = new JsonFilePipelineDefinitionRepository(nonExistentPath);

            // Act & Assert
            Assert.IsNotNull(repository.GetAllPipelineDefinitions()); // Should not throw
            Assert.AreEqual(0, repository.GetAllPipelineDefinitions().Count()); // Should be empty
        }

        [TestMethod]
        public void Constructor_ShouldHandleEmptyDirectory()
        {
            // Arrange
            var repository = new JsonFilePipelineDefinitionRepository(_testPipelinePath);

            // Act
            var definitions = repository.GetAllPipelineDefinitions().ToList();

            // Assert
            Assert.IsNotNull(definitions);
            Assert.AreEqual(0, definitions.Count);
        }

        [TestMethod]
        public void Constructor_ShouldIgnoreInvalidJsonFiles()
        {
            // Arrange
            CreatePipelineFile("valid.json", "{\"Name\":\"ValidCommand\", \"Description\":\"Desc\", \"Type\":\"Command\", \"Pipes\":[]}");
            CreatePipelineFile("invalid.json", "{ \"Name\": \"InvalidCommand\", \"Description\": \"Desc\", \"Type\": \"Command\", \"Pipes\": [ { \"Type\": \"InvalidPipe\", \"Assembly\": \"InvalidAssembly\" } \"extra_content\" ] }"); // Malformed JSON
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
