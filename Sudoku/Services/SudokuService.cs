namespace Sudoku.Services
{
    using Sudoku.Helpers;
    using Sudoku.Interfaces;

    public class SudokuService : ISudokuService
    {
        public bool FindEmptyCell(int[][] matrix, ref int row, ref int col)
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

        public bool IsSafe(Matrix matrix, int row, int col, int num)
        {
            return !(IsRowContains(matrix.GetRow(row), num)
                || IsColContains(matrix.GetColumn(col), num)
                || IsRegionContains(matrix.GetRegionForElement(row, col), num));
        }

        public bool IsRowContains(Row row, int num)
        {
            return row.Contains(num);
        }

        public bool IsColContains(Row row, int num)
        {
            return IsRowContains(row, num);
        }

        public bool IsRegionContains(Matrix region, int num)
        {
            var regionArr = region.SelectMany(x => x);

            return IsRowContains(regionArr, num);
        }
    }
}
