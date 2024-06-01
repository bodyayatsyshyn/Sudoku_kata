namespace Sudoku.Interfaces
{
    public interface ISudokuSolver
    {
        bool IsSolvable(ref Matrix matrix);
    }
}