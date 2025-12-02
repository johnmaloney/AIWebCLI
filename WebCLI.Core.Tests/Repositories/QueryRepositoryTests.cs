using Xunit;
using Moq;
using WebCLI.Core.Contracts;
using WebCLI.Core.Repositories;
using System;

namespace WebCLI.Core.Tests.Repositories
{
    public class QueryRepositoryTests
    {
        [Fact]
        public void QueryRepository_CanBeInstantiated()
        {
            // Arrange & Act
            var repository = new QueryRepository();

            // Assert
            Assert.NotNull(repository);
        }

        [Fact]
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
            Assert.Equal(mockQueryResult.Object, repository[mockQueryCriteria.Object]);
        }

        [Fact]
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
            Assert.Equal(mockQueryResult2.Object, repository[mockQueryCriteria.Object]);
        }

        [Fact]
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
            Assert.Equal(mockQueryResult.Object, result);
        }

        [Fact]
        public void Indexer_ThrowsKeyNotFoundException_ForNonExistentQuery()
        {
            // Arrange
            var repository = new QueryRepository();
            var nonExistentQuery = "nonexistent";
            var mockQueryCriteria = new Mock<IQueryCriteria>();
            mockQueryCriteria.SetupGet(c => c.Identifier).Returns(nonExistentQuery);

            // Act & Assert
            Assert.Throws<System.Collections.Generic.KeyNotFoundException>(() => repository[mockQueryCriteria.Object]);
        }
    }
}
