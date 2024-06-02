namespace Sudoku.Services
{
    using Sudoku.Interfaces;

    public class SudokuGenarator(ISudokuSolver sudokuSolver) : ISudokuGenarator
    {
        private readonly Random _rnd = new Random();

        public Matrix GenerateSudoku(int gridDimention, int emptyCells)
        {
            var matrix = InitialiseEmptyMatrix(gridDimention);
            sudokuSolver.IsSolvable(ref matrix);

            return RemoveValues(matrix, emptyCells);
        }

        private Matrix InitialiseEmptyMatrix(int gridDimention)
        {
            var matrix = new List<Row>();
            for (int i = 0; i < gridDimention; ++i)
            {
                matrix.Add(new List<int>(Enumerable.Repeat(Consts.EmptyMatrixValue, gridDimention)));
            }
            return matrix;
        }

        private Matrix RemoveValues(Matrix matrix, int amountOfValuesToRemove) {
            var indexes = GetIndexes(amountOfValuesToRemove, matrix.Count() - 1);
            var matrixArr = matrix.Select(x => x.ToArray()).ToArray();
            foreach (var index in indexes)
            {
                matrixArr[index.Item1][index.Item2] = Consts.EmptyMatrixValue;
            }

            return matrixArr;
        }

        private IEnumerable<(int, int)> GetIndexes(int count, int maxValue)
        {
            var uniqueIndexPairs = new HashSet<(int, int)>();
            do
            {
                uniqueIndexPairs.Add((_rnd.Next(0, maxValue), _rnd.Next(0, maxValue)));
            } while (uniqueIndexPairs.Count() < count);
            return uniqueIndexPairs;
        }
    }
}
