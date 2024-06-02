namespace Sudoku.Services
{
    using Sudoku.Helpers;
    using Sudoku.Interfaces;
    public class SudokuSolver(ISudokuService sudokuService) : ISudokuSolver
    {
        public bool IsSolvable(ref Matrix matrix)
        {
            var matrixList = matrix.Select(x => x.ToArray()).ToArray();

            var result = SolveInternal(matrixList);
            matrix = matrixList;

            return result;
        }
        private bool SolveInternal(int[][] matrix)
        {
            var row = 0;
            var col = 0;
            var minValue = 1;
            var maxValue = matrix.Count();

            if (!sudokuService.FindEmptyCell(matrix, ref row, ref col))
            {
                // solved
                return true;
            }

            for (int num = minValue; num <= maxValue; ++num)
            {
                if (sudokuService.IsSafe(matrix, row, col, num))
                {
                    matrix[row][col] = num;

                    if (SolveInternal(matrix))
                    {
                        return true;
                    }

                    matrix[row][col] = Consts.EmptyMatrixValue;
                }
            }
            return false;
        }
    }
}
