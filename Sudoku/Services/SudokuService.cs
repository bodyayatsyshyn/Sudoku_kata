namespace Sudoku.Services
{
    using Sudoku.Helpers;
    using Sudoku.Interfaces;

    public class SudokuService : ISudokuService
    {
        private readonly IMatrixService _matrixService;

        public SudokuService(IMatrixService matrixService)
        {
            _matrixService = matrixService;
        }

        public bool IsApplicable(Matrix matrix) 
        {
            var isSquareMatrix = _matrixService.IsSquareMatrix(matrix);
            var isAnyDuplicatesInRows = _matrixService.IsAnyRowsValuesDuplicated(matrix);
            var isAnyDuplicatesInCols = _matrixService.IsAnyColsValuesDuplicated(matrix);
            var allValuesInRange = matrix.Any(row => _matrixService.IsRowContainsRequiredRange(row));
            var isRegionValuesUnique = _matrixService.IsRegionsValuesUnique(matrix);

            return isSquareMatrix && !isAnyDuplicatesInRows && !isAnyDuplicatesInCols && allValuesInRange && isRegionValuesUnique;
        }

        public bool IsCorrectSolution(Matrix initialGrid, Matrix solution)
        {
            if (!IsApplicable(solution)
                || !_matrixService.ValuesMatches(initialGrid, solution))
            {
                return false;
            }

            return true;
        }

        public bool IsSolved(Matrix matrix)
        {
            return !matrix.SelectMany(x => x).Contains(Consts.EmptyMatrixValue);
        }

        #region test

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

                    matrix[row][col] = 0;
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

        #endregion



        public static void PrintMatrix(Matrix matrix)
        {
            foreach (var row in matrix)
            {
                foreach (var element in row)
                    Console.Write($"{element} ");
                Console.WriteLine();
            }
        }
    }
}
