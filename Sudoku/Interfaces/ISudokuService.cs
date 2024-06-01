namespace Sudoku.Interfaces
{
    public interface ISudokuService {
        public bool IsApplicable(Matrix matrix);
        public bool IsCorrectSolution(Matrix initialGrid, Matrix solution);

        public bool IsSolvable(ref Matrix matrix);
    }
}
