namespace Sudoku.Interfaces
{
    public interface ISudokuGenerator
    {
        Matrix GenerateSudoku(int gridDimention, int emptyCells);
    }
}