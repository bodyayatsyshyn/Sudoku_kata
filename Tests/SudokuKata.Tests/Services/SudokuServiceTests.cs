using Sudoku.Services;
using Sudoku.Interfaces;
using Moq;

namespace Sudoku.Services.Tests
{
    public class SudokuServiceTests
    {
        private Mock<IMatrixService> _mockMatrixService;

        private readonly SudokuService _sudokyService;

        public SudokuServiceTests()
        {
            _mockMatrixService = new Mock<IMatrixService>();
            _mockMatrixService.Setup(x => x.Transpose(It.IsAny<IEnumerable<IEnumerable<int>>>())).Returns<IEnumerable<IEnumerable<int>>>(x => x);
            _sudokyService = new SudokuService(_mockMatrixService.Object);
        }

        [Fact]
        public void IsApplicable_ValidSudokuGrid_ReturnsTrue()
        {
            // Arrange
            _mockMatrixService.Setup(x => x.IsSquareMatrix(It.IsAny<IEnumerable<IEnumerable<int>>>())).Returns(true);
            _mockMatrixService.Setup(x => x.IsRegionsValuesUnique(It.IsAny<IEnumerable<IEnumerable<int>>>())).Returns(true);
            _mockMatrixService.Setup(x => x.IsRowValuesUnique(It.IsAny<IEnumerable<int>>())).Returns(true);
            _mockMatrixService.Setup(x => x.IsRowContainsRequiredRange(It.IsAny<IEnumerable<int>>())).Returns(true);

            var matrix = new List<IEnumerable<int>>
        {
            new List<int> { 1, 2, 3 },
            new List<int> { 4, 5, 6 },
            new List<int> { 7, 8, 9 }
        };

            // Act
            var result = _sudokyService.IsApplicable(matrix);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(true, true, true, false)]
        [InlineData(true, true, false, true)]
        [InlineData(true, false, true, true)]
        [InlineData(false, true, true, true)]
        public void IsApplicable_InvalidSudokuGrid_ReturnsFalse(bool isSquareMatrix, bool isRegionsValuesUnique, bool isRowValuesUnique, bool isRowContainsRequiredRange)
        {
            // Arrange
            _mockMatrixService.Setup(x => x.IsSquareMatrix(It.IsAny<IEnumerable<IEnumerable<int>>>())).Returns(isSquareMatrix);
            _mockMatrixService.Setup(x => x.IsRegionsValuesUnique(It.IsAny<IEnumerable<IEnumerable<int>>>())).Returns(isRegionsValuesUnique);
            _mockMatrixService.Setup(x => x.IsRowValuesUnique(It.IsAny<IEnumerable<int>>())).Returns(isRowValuesUnique);
            _mockMatrixService.Setup(x => x.IsRowContainsRequiredRange(It.IsAny<IEnumerable<int>>())).Returns(isRowContainsRequiredRange);

            var matrix = new List<IEnumerable<int>>
        {
            new List<int> { 1, 2, 3 },
            new List<int> { 4, 5, 6 },
            new List<int> { 7, 8, 9 }
        };

            // Act
            var result = _sudokyService.IsApplicable(matrix);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsCorrectSolution_ValidSolution_ReturnsTrue()
        {
            // Arrange
            _mockMatrixService.Setup(x => x.IsSquareMatrix(It.IsAny<IEnumerable<IEnumerable<int>>>())).Returns(true);
            _mockMatrixService.Setup(x => x.IsRegionsValuesUnique(It.IsAny<IEnumerable<IEnumerable<int>>>())).Returns(true);
            _mockMatrixService.Setup(x => x.IsRowValuesUnique(It.IsAny<IEnumerable<int>>())).Returns(true);
            _mockMatrixService.Setup(x => x.IsRowContainsRequiredRange(It.IsAny<IEnumerable<int>>())).Returns(true);

            _mockMatrixService.Setup(x => x.ValuesMatches(It.IsAny<IEnumerable<IEnumerable<int>>>(), It.IsAny<IEnumerable<IEnumerable<int>>>())).Returns(true);

            var init = new List<IEnumerable<int>>
        {
            new List<int> { -1, 2, 3 },
            new List<int> { 4, -1, 6 },
            new List<int> { 7, 8, 9 }
        };

            var solution = new List<IEnumerable<int>>
        {
            new List<int> { 1, 2, 3 },
            new List<int> { 4, 5, 6 },
            new List<int> { 7, 8, 9 }
        };

            // Act
            var result = _sudokyService.IsCorrectSolution(init, solution);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void IsCorrectSolution_InvalidGrids_ReturnsFalse(bool canBeSolution, bool valuesMatches)
        {
            // Arrange
            _mockMatrixService.Setup(x => x.IsSquareMatrix(It.IsAny<IEnumerable<IEnumerable<int>>>())).Returns(canBeSolution);
            _mockMatrixService.Setup(x => x.IsRegionsValuesUnique(It.IsAny<IEnumerable<IEnumerable<int>>>())).Returns(true);
            _mockMatrixService.Setup(x => x.IsRowValuesUnique(It.IsAny<IEnumerable<int>>())).Returns(true);
            _mockMatrixService.Setup(x => x.IsRowContainsRequiredRange(It.IsAny<IEnumerable<int>>())).Returns(true);

            _mockMatrixService.Setup(x => x.ValuesMatches(It.IsAny<IEnumerable<IEnumerable<int>>>(), It.IsAny<IEnumerable<IEnumerable<int>>>())).Returns(valuesMatches);

            var init = new List<IEnumerable<int>>
        {
            new List<int> { -1, 2, 3 },
            new List<int> { 4, -1, 6 },
            new List<int> { 7, 8, 9 }
        };

            var solution = new List<IEnumerable<int>>
        {
            new List<int> { 1, 2, 3 },
            new List<int> { 4, 5, 6 },
            new List<int> { 7, 8, 9 }
        };

            // Act
            var result = _sudokyService.IsCorrectSolution(init, solution);

            // Assert
            Assert.False(result);
        }
    }
}