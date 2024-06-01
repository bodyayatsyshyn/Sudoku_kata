namespace Sudoku.Services
{
    using Sudoku.Helpers;
    using Sudoku.Interfaces;
    public class SudokuSolver : ISudokuSolver
    {
        public bool IsSolvable(ref Matrix matrix)
        {
            var matrixList = matrix.Select(x => x.ToList()).ToList();

            var result = SolveInternal(matrixList);
            matrix = matrixList;

            return result;
        }
        private bool SolveInternal(List<List<int>> matrix)
        {
            var row = 0;
            var col = 0;
            var minValue = 1;
            var maxValue = matrix.Count();

            if (!FindEmptyCell(matrix, ref row, ref col))
            {
                // solved
                return true;
            }

            for (int num = minValue; num <= maxValue; ++num)
            {
                if (IsSafe(matrix, row, col, num))
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

        private bool FindEmptyCell(List<List<int>> matrix, ref int row, ref int col)
        {
            for (row = 0; row < matrix.Count(); ++row)
            {
                for (col = 0; col < matrix[row].Count(); ++col)
                {
                    if (matrix[row][col] == Consts.EmptyMatrixValue)
                        return true;
                }
            }

            return false;
        }

        private bool IsSafe(Matrix matrix, int row, int col, int num)
        {
            return !(IsRowContains(matrix.GetRow(row), num)
                || IsColContains(matrix.GetColumn(col), num)
                || IsRegionContains(matrix.GetRegionForElement(row, col), num));
        }

        private bool IsRowContains(Row row, int num)
        {
            return row.Contains(num);
        }

        private bool IsColContains(Row row, int num)
        {
            return IsRowContains(row, num);
        }

        private bool IsRegionContains(Matrix region, int num)
        {
            var regionArr = region.SelectMany(x => x);

            return IsRowContains(regionArr, num);
        }
    }
}
