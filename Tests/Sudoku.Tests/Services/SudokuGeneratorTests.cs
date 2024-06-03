namespace Sudoku.Services.Tests
{
    using Moq;
    using Sudoku.Interfaces;

    public class SudokuGeneratorTests
    {
        private Mock<ISudokuSolver> _mockSolver;

        private readonly SudokuGenerator _generator;
        public SudokuGeneratorTests()
        {
            _mockSolver = new Mock<ISudokuSolver>();
            _mockSolver.Setup(x => x.IsSolvable(ref It.Ref<Matrix>.IsAny)).Callback((ref Matrix matrix) =>
            {
                var tmpMatrix = new List<Row>();
                for(int i = 0; i < matrix.Count(); ++i)
                {
                    tmpMatrix.Add(Enumerable.Range(1, matrix.Count()));
                }
                matrix = tmpMatrix;
            }).Returns(true);

            _generator = new SudokuGenerator(_mockSolver.Object);
        }

        [Theory]
        [InlineData(3, 0)]
        [InlineData(9, 5)]
        [InlineData(9, 25)]
        [InlineData(3, 4)]
        public void GenerateSudoku_ReturnsValidSudokuGrid(int dimention, int emptyCells)
        {
            // Act
            var result = _generator.GenerateSudoku(dimention, emptyCells);

            // Assert
            Assert.Equal(dimention, result.Count());

            var array = result.SelectMany(x => x.ToArray());
            Assert.Equal(emptyCells, array.Count(x => x == Consts.EmptyMatrixValue));
        }
    }
}
