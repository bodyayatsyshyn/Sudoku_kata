namespace Sudoku.Services.Tests
{

    public class MatrixServiceTests
    {
        private readonly MatrixService _matrixService;

        public MatrixServiceTests()
        {
            _matrixService = new MatrixService();
        }

        [Fact]
        public void IsSquareMatrix_ValidSquareMatrix_ReturnsTrue()
        {
            // Arrange
            var squareMatrix = new List<Row>
        {
            new List<int> { 1, 2, 3 },
            new List<int> { 4, 5, 6 },
            new List<int> { 7, 8, 9 }
        };

            // Act
            var result = _matrixService.IsSquareMatrix(squareMatrix);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsSquareMatrix_NonSquareMatrix_ReturnsFalse()
        {
            // Arrange
            var nonSquareMatrix = new List<Row>
        {
            new List<int> { 1, 2, 3 },
            new List<int> { 4, 5, 6 }
        };

            // Act
            var result = _matrixService.IsSquareMatrix(nonSquareMatrix);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsAnyRowsValuesDuplicated_UniqueRows_ReturnsFalse()
        {
            // Arrange
            var uniqueRow =
                new[]
                {
                    new List<int> { 1, 2, 3, 4, 5 },
                    new List<int> { 1, 2, 3, 4, 5 }
                };

            // Act
            var result = _matrixService.IsAnyRowsValuesDuplicated(uniqueRow);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsAnyRowsValuesDuplicated_DuplicateInRow_ReturnsTrue()
        {
            // Arrange
            var duplicateRow =
                new[]
                {
                    new List<int> { 1, 2, 3, 3, 5 },
                    new List<int> { 1, 2, 3, 4, 5 }
                };

            // Act
            var result = _matrixService.IsAnyRowsValuesDuplicated(duplicateRow);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Transpose_TransposedMatrixReturned()
        {
            // Arrange
            var init = new List<Row>
        {
            new List<int> { 1, 2 },
            new List<int> { 3, 4 },
        };

            var transpoused = new List<Row>
        {
            new List<int> { 1, 3 },
            new List<int> { 2, 4 },
        };

            // Act
            var result = _matrixService.Transpose(init);

            // Assert
            Assert.Equivalent(transpoused, result);
        }

        [Fact]
        public void IsRowContainsRequiredRange_CorrectRange_ReturnsTrue()
        {
            // Arrange
            var row = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            var result = _matrixService.IsRowContainsRequiredRange(row);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsRowContainsRequiredRange_NotCorrectRange_ReturnsFalse()
        {
            // Arrange
            var row = new List<int> { 9, 2, 3, 4, 5 };

            // Act
            var result = _matrixService.IsRowContainsRequiredRange(row);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsRegionsValuesUnique_RegionsWithDuplicateValues_ReturnsFalse()
        {
            // Arrange
            var matrix = new List<Row>
        {
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
        };

            // Act
            var result = _matrixService.IsRegionsValuesUnique(matrix);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsRegionsValuesUnique_RegionsWithUniqueValues_ReturnsTrue()
        {
            // Arrange
            var matrix = new List<Row>
        {
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
            new List<int> { 4, 5, 6, 7, 8, 9, 1, 2, 3 },
            new List<int> { 7, 8, 9, 1, 2, 3, 4, 5, 6 },
            new List<int> { 2, 3, 4, 5, 6, 7, 8, 9, 1 },
            new List<int> { 5, 6, 7, 8, 9, 1, 2, 3, 4 },
            new List<int> { 8, 9, 1, 2, 3, 4, 5, 6, 7 },
            new List<int> { 3, 4, 5, 6, 7, 8, 9, 1, 2 },
            new List<int> { 6, 7, 8, 9, 1, 2, 3, 4, 5 },
            new List<int> { 9, 1, 2, 3, 4, 5, 6, 7, 8 }
        };

            // Act
            var result = _matrixService.IsRegionsValuesUnique(matrix);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsRegionsValuesUnique_InvalidDueToIncompleteRegion_ReturnsFalse()
        {
            // Arrange
            var matrix = new List<Row>
        {
            new List<int> { 5, 3, 4, 6, 7, 8, 9, 1, 2 },
            new List<int> { 6, 7, 2, 1, 9, 5, 3, 4, 8 },
            new List<int> { 1, 9, 8, 3, 4, 2, 5, 6, 7 },
            new List<int> { 8, 5, 9, 7, 6, 1, 4, 2, 3 },
            new List<int> { 4, 2, 6, 8, 5, 3, 7, 9, 1 },
            new List<int> { 7, 1, 3, 9, 2, 4, 8, 5, 6 },
            new List<int> { 9, 6, 1, 5, 3, 7, 2, 8, 4 },
            new List<int> { 2, 8, 7, 4, 1, 9, 6, 3, 5 },
            new List<int> { 3, 4, 5, 2, 8, 6, 1, 7 }
        };

            // Act
            var result = _matrixService.IsRegionsValuesUnique(matrix);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValuesMatches_DifferentGridSizes_ReturnsFalse()
        {
            // Arrange
            var initialGrid = new List<Row>
        {
            new List<int> { 1, 2, 3 },
        };

            var solution = new List<Row>
        {
            new List<int> { 1, 2, 3, 4,},
        };

            // Act
            var result = _matrixService.ValuesMatches(initialGrid, solution);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValuesMatches_DifferentValuesInSamePosition_ReturnsFalse()
        {
            // Arrange
            var initialGrid = new List<Row>
        {
            new List<int> { 1, 2, 3 },
            new List<int> { 4, 5, 6 }
        };

            var solution = new List<Row>
        {
            new List<int> { 1, 2, 3 },
            new List<int> { 4, 9, 6 }
        };

            // Act
            var result = _matrixService.ValuesMatches(initialGrid, solution);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValuesMatches_IdenticalGrids_ReturnsTrue()
        {
            // Arrange
            var grid = new List<Row>
        {
            new List<int> { 1, 2 },
            new List<int> { 3, 4 }
        };

            // Act
            var result = _matrixService.ValuesMatches(grid, grid);

            // Assert
            Assert.True(result);
        }
    }
}