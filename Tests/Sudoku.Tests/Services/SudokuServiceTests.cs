namespace Sudoku.Services.Tests
{
    using Sudoku.Services;

    public class SudokuServiceTests
    {
        private readonly SudokuService _service;

        public SudokuServiceTests()
        {
            _service = new SudokuService();
        }

        [Fact]
        public void FindEmptyCell_NoEmptyCells_ReturnsFalse()
        {
            // Arrange
            var matrix = new[]
            {
                new[]{1, 2, 3, 4},
                new[]{1, 2, 3, 4}
            };
            var row = 0;
            var col = 0;
            // Act
            var result = _service.FindEmptyCell(matrix, ref row, ref col);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void FindEmptyCell_EmptyCell_ReturnsTrue()
        {
            // Arrange
            var matrix = new[]
            {
                new[]{1, 2, 3, 4},
                new[]{1, 2, Consts.EmptyMatrixValue, 4}
            };
            var row = 0;
            var col = 0;
            // Act
            var result = _service.FindEmptyCell(matrix, ref row, ref col);

            // Assert
            Assert.True(result);
            Assert.Equal(1, row);
            Assert.Equal(2, col);
        }

        [Fact]
        public void IsRowContains_NumInRow_ReturnsTrue()
        {
            // Arrange
            var num = 1;
            var row = new[] { num, 3, 4, 5 };

            // Act
            var result = _service.IsRowContains(row, num);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsRowContains_NumNotInRow_ReturnsFalse()
        {
            // Arrange
            var row = new[] { 1, 3, 4, 5 };

            // Act
            var result = _service.IsRowContains(row, 0);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsColContains_NumInRow_ReturnsTrue()
        {
            // Arrange
            var num = 1;
            var row = new[] { num, 3, 4, 5 };

            // Act
            var result = _service.IsColContains(row, num);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsColContains_NumNotInRow_ReturnsFalse()
        {
            // Arrange
            var row = new[] { 1, 3, 4, 5 };

            // Act
            var result = _service.IsColContains(row, 0);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsRegionContains_NumInRow_ReturnsTrue()
        {
            // Arrange
            var num = 1;
            var row = new[] { new []{num, 3, 4, 5 } };

            // Act
            var result = _service.IsRegionContains(row, num);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsRegionContains_NumNotInRow_ReturnsFalse()
        {
            // Arrange
            var row = new[] { new[] { 1, 3, 4, 5 } };

            // Act
            var result = _service.IsRegionContains(row, 0);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsSafe_NumFound_ReturnsFalse()
        {
            // Arrange
            var row = 0;
            var col = 0;
            var num = 1;
            var matrix = new[]
            {
                new []{ num, 2, 3 },
                new []{ 4, 5, 6 },
                new []{ 7, 8, 9 },
            };

            // Act
            var result = _service.IsSafe(matrix, row, col, num);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsSafe_NumNotFound_ReturnsTrue()
        {
            // Arrange
            var row = 0;
            var col = 0;
            var num = 9;
            var matrix = new[]
            {
                new []{ 1, 2, 3 },
                new []{ 4, 5, 6 },
                new []{ 7, 8, num },
            };

            // Act
            var result = _service.IsSafe(matrix, row, col, num);

            // Assert
            Assert.True(result);
        }
    }
}
