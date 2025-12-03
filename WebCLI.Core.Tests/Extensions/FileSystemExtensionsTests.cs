
using WebCLI.Core;
using System.IO;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebCLI.Core.Tests.Extensions
{
    public class FileSystemExtensionsTests : IDisposable
    {
        private readonly string _tempDirectory;

        public FileSystemExtensionsTests()
        {
            _tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_tempDirectory);
        }

        public void Dispose()
        {
            if (Directory.Exists(_tempDirectory))
            {
                Directory.Delete(_tempDirectory, true);
            }
        }

        [TestMethod]
        public void IsDirectoryPath_ReturnsTrue_ForExistingDirectory()
        {
            // Arrange
            var testDirPath = Path.Combine(_tempDirectory, "TestDir");
            Directory.CreateDirectory(testDirPath);

            // Act
            var result = testDirPath.IsDirectoryPath();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsDirectoryPath_ReturnsFalse_ForExistingFile()
        {
            // Arrange
            var testFilePath = Path.Combine(_tempDirectory, "TestFile.txt");
            File.WriteAllText(testFilePath, "test content");

            // Act
            var result = testFilePath.IsDirectoryPath();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsDirectoryPath_ReturnsTrue_ForNonExistentPath_WithoutExtension()
        {
            // Arrange
            var nonExistentDirPath = Path.Combine(_tempDirectory, "NonExistentDir");

            // Act
            var result = nonExistentDirPath.IsDirectoryPath();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsDirectoryPath_ReturnsFalse_ForNonExistentPath_WithExtension()
        {
            // Arrange
            var nonExistentFilePath = Path.Combine(_tempDirectory, "NonExistentFile.txt");

            // Act
            var result = nonExistentFilePath.IsDirectoryPath();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void IsDirectoryPath_ReturnsFalse_ForNullEmptyOrWhitespacePath(string path)
        {
            // Act
            var result = path.IsDirectoryPath();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsDirectoryPath_ReturnsFalse_ForExistingFile_EvenIfNoExtensionInPath()
        {
            // Arrange
            var testFilePath = Path.Combine(_tempDirectory, "TestFileWithNoExplicitExtension");
            File.WriteAllText(testFilePath, "test content");

            // Act
            var result = testFilePath.IsDirectoryPath();

            // Assert
            Assert.IsFalse(result);
        }
    }
}
