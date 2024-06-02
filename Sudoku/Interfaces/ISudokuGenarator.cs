namespace Sudoku.Interfaces
{
    public interface ISudokuGenarator
    {
        Matrix GenerateSudoku(int gridDimention, int emptyCells);
    }
}