namespace Sudoku.Services.Tests
{
    using Sudoku.Services;
    using Sudoku.Interfaces;
    using Moq;

    public class SudokuServiceTests
    {
        private Mock<IMatrixService> _mockMatrixService;

        private readonly SudokuService _sudokyService;

        public SudokuServiceTests()
        {
            _mockMatrixService = new Mock<IMatrixService>();
            _mockMatrixService.Setup(x => x.Transpose(It.IsAny<Matrix>())).Returns<Matrix>(x => x);
            _sudokyService = new SudokuService(_mockMatrixService.Object);
        }

        [Fact]
        public void IsApplicable_ValidSudokuGrid_ReturnsTrue()
        {
            // Arrange
            _mockMatrixService.Setup(x => x.IsSquareMatrix(It.IsAny<Matrix>())).Returns(true);
            _mockMatrixService.Setup(x => x.IsRegionsValuesUnique(It.IsAny<Matrix>())).Returns(true);
            _mockMatrixService.Setup(x => x.IsAnyRowsValuesDuplicated(It.IsAny<Matrix>())).Returns(false);
            _mockMatrixService.Setup(x => x.IsRowContainsRequiredRange(It.IsAny<Row>())).Returns(true);

            var matrix = new List<Row>
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
        [InlineData(true, true, false, false)]
        [InlineData(true, true, true, true)]
        [InlineData(true, false, false, true)]
        [InlineData(false, true, false, true)]
        public void IsApplicable_InvalidSudokuGrid_ReturnsFalse(bool isSquareMatrix, bool isRegionsValuesUnique, bool isRowValuesDuplicated, bool isRowContainsRequiredRange)
        {
            // Arrange
            _mockMatrixService.Setup(x => x.IsSquareMatrix(It.IsAny<Matrix>())).Returns(isSquareMatrix);
            _mockMatrixService.Setup(x => x.IsRegionsValuesUnique(It.IsAny<Matrix>())).Returns(isRegionsValuesUnique);
            _mockMatrixService.Setup(x => x.IsAnyRowsValuesDuplicated(It.IsAny<Matrix>())).Returns(isRowValuesDuplicated);
            _mockMatrixService.Setup(x => x.IsRowContainsRequiredRange(It.IsAny<Row>())).Returns(isRowContainsRequiredRange);

            var matrix = new List<Row>
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
            _mockMatrixService.Setup(x => x.IsSquareMatrix(It.IsAny<Matrix>())).Returns(true);
            _mockMatrixService.Setup(x => x.IsRegionsValuesUnique(It.IsAny<Matrix>())).Returns(true);
            _mockMatrixService.Setup(x => x.IsAnyRowsValuesDuplicated(It.IsAny<Matrix>())).Returns(false);
            _mockMatrixService.Setup(x => x.IsRowContainsRequiredRange(It.IsAny<Row>())).Returns(true);

            _mockMatrixService.Setup(x => x.ValuesMatches(It.IsAny<Matrix>(), It.IsAny<Matrix>())).Returns(true);

            var init = new List<Row>
        {
            new List<int> { -1, 2, 3 },
            new List<int> { 4, -1, 6 },
            new List<int> { 7, 8, 9 }
        };

            var solution = new List<Row>
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
            _mockMatrixService.Setup(x => x.IsSquareMatrix(It.IsAny<Matrix>())).Returns(canBeSolution);
            _mockMatrixService.Setup(x => x.IsRegionsValuesUnique(It.IsAny<Matrix>())).Returns(true);
            _mockMatrixService.Setup(x => x.IsAnyRowsValuesDuplicated(It.IsAny<Matrix>())).Returns(true);
            _mockMatrixService.Setup(x => x.IsRowContainsRequiredRange(It.IsAny<Row>())).Returns(true);

            _mockMatrixService.Setup(x => x.ValuesMatches(It.IsAny<Matrix>(), It.IsAny<Matrix>())).Returns(valuesMatches);

            var init = new List<Row>
        {
            new List<int> { -1, 2, 3 },
            new List<int> { 4, -1, 6 },
            new List<int> { 7, 8, 9 }
        };

            var solution = new List<Row>
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