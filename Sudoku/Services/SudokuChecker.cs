namespace Sudoku.Services
{
    using Sudoku.Helpers;
    using Sudoku.Interfaces;

    public class SudokuChecker(IMatrixService matrixService) : ISudokuChecker
    {

        public bool IsApplicable(Matrix matrix) 
        {
            var isSquareMatrix = matrixService.IsSquareMatrix(matrix);
            var isAnyDuplicatesInRows = matrixService.IsAnyRowsValuesDuplicated(matrix);
            var isAnyDuplicatesInCols = matrixService.IsAnyColsValuesDuplicated(matrix);
            var allValuesInRange = matrix.Any(row => matrixService.IsRowContainsRequiredRange(row));
            var isRegionValuesUnique = matrixService.IsRegionsValuesUnique(matrix);

            return isSquareMatrix && !isAnyDuplicatesInRows && !isAnyDuplicatesInCols && allValuesInRange && isRegionValuesUnique;
        }

        public bool IsCorrectSolution(Matrix initialGrid, Matrix solution)
        {
            if (!IsApplicable(solution)
                || !matrixService.ValuesMatches(initialGrid, solution))
            {
                return false;
            }

            return true;
        }
    }
}
