using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebCLI.Core.Contracts;
using WebCLI.Core.Repositories;
using System;
using System.Collections.Generic;

namespace WebCLI.Core.Tests.Repositories
{
    [TestClass]
    public class QueryRepositoryTests
    {
        [TestMethod]
        public void QueryRepository_CanBeInstantiated()
        {
            // Arrange & Act
            var repository = new QueryRepository();

            // Assert
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void AddQueryDelegate_AddsNewQuery()
        {
            // Arrange
            var repository = new QueryRepository();
            var queryIdentifier = "testquery";
            var mockQueryResult = new Mock<IQueryResult>();
            Func<IQueryCriteria, IQueryResult> queryDelegate = (criteria) => mockQueryResult.Object;

            // Act
            repository.AddQueryDelegate(queryIdentifier, queryDelegate);

            // Assert
            var mockQueryCriteria = new Mock<IQueryCriteria>();
            mockQueryCriteria.SetupGet(c => c.Identifier).Returns(queryIdentifier);
            Assert.AreEqual(mockQueryResult.Object, repository[mockQueryCriteria.Object]);
        }

        [TestMethod]
        public void AddQueryDelegate_UpdatesExistingQuery()
        {
            // Arrange
            var repository = new QueryRepository();
            var queryIdentifier = "updatequery";
            var mockQueryResult1 = new Mock<IQueryResult>();
            Func<IQueryCriteria, IQueryResult> queryDelegate1 = (criteria) => mockQueryResult1.Object;
            repository.AddQueryDelegate(queryIdentifier, queryDelegate1);

            var mockQueryResult2 = new Mock<IQueryResult>();
            Func<IQueryCriteria, IQueryResult> queryDelegate2 = (criteria) => mockQueryResult2.Object;

            // Act
            repository.AddQueryDelegate(queryIdentifier, queryDelegate2);

            // Assert
            var mockQueryCriteria = new Mock<IQueryCriteria>();
            mockQueryCriteria.SetupGet(c => c.Identifier).Returns(queryIdentifier);
            Assert.AreEqual(mockQueryResult2.Object, repository[mockQueryCriteria.Object]);
        }

        [TestMethod]
        public void Indexer_ReturnsCorrectQueryResult_ForExistingQuery()
        {
            // Arrange
            var repository = new QueryRepository();
            var queryIdentifier = "existingquery";
            var mockQueryResult = new Mock<IQueryResult>();
            Func<IQueryCriteria, IQueryResult> queryDelegate = (criteria) => mockQueryResult.Object;
            repository.AddQueryDelegate(queryIdentifier, queryDelegate);

            var mockQueryCriteria = new Mock<IQueryCriteria>();
            mockQueryCriteria.SetupGet(c => c.Identifier).Returns(queryIdentifier);

            // Act
            var result = repository[mockQueryCriteria.Object];

            // Assert
            Assert.AreEqual(mockQueryResult.Object, result);
        }

        [TestMethod]
        public void Indexer_ThrowsKeyNotFoundException_ForNonExistentQuery()
        {
            // Arrange
            var repository = new QueryRepository();
            var nonExistentQuery = "nonexistent";
            var mockQueryCriteria = new Mock<IQueryCriteria>();
            mockQueryCriteria.SetupGet(c => c.Identifier).Returns(nonExistentQuery);

            // Act & Assert
            Assert.ThrowsException<KeyNotFoundException>(() => repository[mockQueryCriteria.Object]);
        }
    }
}
