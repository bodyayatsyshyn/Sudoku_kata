namespace Sudoku.Services
{
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
    }
}
