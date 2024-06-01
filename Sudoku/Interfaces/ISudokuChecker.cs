namespace Sudoku.Interfaces
{
    public interface ISudokuChecker {
        public bool IsApplicable(Matrix matrix);
        public bool IsCorrectSolution(Matrix initialGrid, Matrix solution);
    }
}
