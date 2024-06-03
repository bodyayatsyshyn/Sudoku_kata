namespace Sudoku.Services.Tests
{
    using Moq;
    using Sudoku.Interfaces;

    public class SudokuSolverTests
    {
        private Mock<ISudokuService> _mockService;
        private readonly SudokuSolver _solver;

        public SudokuSolverTests()
        {
            _mockService = new Mock<ISudokuService>();

            _solver = new SudokuSolver(_mockService.Object);
        }

        [Fact]
        public void IsSolvable_Solvable_ReturnsTrueAndUpdateMatrix()
        {
            // Arrange
            Matrix matrix = new List<Row>()
            {
                new List<int> { 1, 2, 3 },
                new List<int> { 3, 1, 2 },
                new List<int> { 2, 3, Consts.EmptyMatrixValue },
            };
            var expectedElement = 1;
            var expectedRow = 2;
            var expectedCol = 2;

            var findEmptyCellResultsQueue = new Queue<bool>(new[] { true, false });
            _mockService.Setup(x => x.IsSafe(It.IsAny<Matrix>(), It.IsAny<int>(), It.IsAny<int>(), expectedElement)).Returns(true);
            _mockService.Setup(x => x.FindEmptyCell(It.IsAny<int[][]>(), ref It.Ref<int>.IsAny, ref It.Ref<int>.IsAny))
                .Callback((int[][] array, ref int row, ref int col) =>
                    {
                        row = expectedRow; col = expectedCol;
                    })
                .Returns(findEmptyCellResultsQueue.Dequeue);

            // Act
            var result = _solver.IsSolvable(ref matrix);

            // Assert
            Assert.True(result);
            Assert.Equal(expectedElement, matrix.ElementAt(expectedRow).ElementAt(expectedCol));
        }

        [Fact]
        public void IsSolvable_AlreadySolved_ReturnsTrue()
        {
            // Arrange
            Matrix matrix = new List<Row>()
            {
                new List<int> { 1, 2, 3 },
                new List<int> { 3, 1, 2 },
                new List<int> { 2, 3, 1 },
            };

            _mockService.Setup(x => x.FindEmptyCell(It.IsAny<int[][]>(), ref It.Ref<int>.IsAny, ref It.Ref<int>.IsAny))
                .Returns(false);

            // Act
            var result = _solver.IsSolvable(ref matrix);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsSolvable_NotSolvable_ReturnsFalse()
        {
            // Arrange
            Matrix matrix = new List<Row>()
            {
                new List<int> { 1, 2, 3 },
                new List<int> { 3, 1, 2 },
                new List<int> { 2, 3, Consts.EmptyMatrixValue },
            };

            _mockService.Setup(x => x.IsSafe(It.IsAny<Matrix>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            _mockService.Setup(x => x.FindEmptyCell(It.IsAny<int[][]>(), ref It.Ref<int>.IsAny, ref It.Ref<int>.IsAny)).Returns(true);

            // Act
            var result = _solver.IsSolvable(ref matrix);

            // Assert
            Assert.False(result);
            Assert.Equal(Consts.EmptyMatrixValue, matrix.ElementAt(2).ElementAt(2));
        }
    }
}
