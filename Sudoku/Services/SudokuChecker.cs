namespace Sudoku.Services
{
    using Sudoku.Helpers;
    using Sudoku.Interfaces;

    public class SudokuChecker : ISudokuChecker
    {
        private readonly IMatrixService _matrixService;

        public SudokuChecker(IMatrixService matrixService)
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
