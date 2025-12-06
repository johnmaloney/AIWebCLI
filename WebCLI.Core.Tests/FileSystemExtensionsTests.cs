using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCLI.Core;
using System.IO;

namespace WebCLI.Core.Tests
{
    [TestClass]
    public class FileSystemExtensionsTests
    {
        private const string TestDirectory = "TestFiles";
        private const string TestFilePath = TestDirectory + "/testfile.txt";
        private const string TestDirectoryPath = TestDirectory + "/SubDirectory";

        [TestInitialize]
        public void Setup()
        {
            if (Directory.Exists(TestDirectory))
            {
                Directory.Delete(TestDirectory, true);
            }
            Directory.CreateDirectory(TestDirectory);
            File.WriteAllText(TestFilePath, "This is a test file.");
            Directory.CreateDirectory(TestDirectoryPath);
        }

        [TestCleanup]
        public void Teardown()
        {
            if (Directory.Exists(TestDirectory))
            {
                Directory.Delete(TestDirectory, true);
            }
        }

        [TestMethod]
        public void IsDirectoryPath_ShouldReturnTrue_ForExistingDirectory()
        {
            // Arrange
            string path = TestDirectory;

            // Act
            bool result = path.IsDirectoryPath();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsDirectoryPath_ShouldReturnFalse_ForExistingFile()
        {
            // Arrange
            string path = TestFilePath;

            // Act
            bool result = path.IsDirectoryPath();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsDirectoryPath_ShouldReturnFalse_ForNullOrWhitespacePath()
        {
            // Arrange
            string nullPath = null;
            string whitespacePath = "   ";

            // Act & Assert
            Assert.IsFalse(nullPath.IsDirectoryPath());
            Assert.IsFalse(whitespacePath.IsDirectoryPath());
        }

        [TestMethod]
        public void IsDirectoryPath_ShouldReturnTrue_ForNonExistentPathWithNoExtension()
        {
            // Arrange
            string path = TestDirectory + "/NonExistentDirectory";

            // Act
            bool result = path.IsDirectoryPath();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsDirectoryPath_ShouldReturnFalse_ForNonExistentPathWithExtension()
        {
            // Arrange
            string path = TestDirectory + "/nonexistentfile.doc";

            // Act
            bool result = path.IsDirectoryPath();

            // Assert
            Assert.IsFalse(result);
        }
    }
}
