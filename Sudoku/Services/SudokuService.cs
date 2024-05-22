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
            var notSquareMatrix = !_matrixService.IsSquareMatrix(matrix);
            var rowOrColsValuesDuplicated = matrix.Any(row => !_matrixService.IsRowValuesUnique(row)) 
                || _matrixService.Transpose(matrix).Any(row => !_matrixService.IsRowValuesUnique(row));
            var valuesNotInRange = matrix.Any(row => !_matrixService.IsRowContainsRequiredRange(row));
            var regionValuesDuplicated = !_matrixService.IsRegionsValuesUnique(matrix);

            if (notSquareMatrix || rowOrColsValuesDuplicated || valuesNotInRange || regionValuesDuplicated)
            {
                return false;
            }

            return true;
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
