namespace Sudoku.Interfaces
{
    public interface ISudokuService
    {
        bool FindEmptyCell(int[][] matrix, ref int row, ref int col);
        bool IsColContains(Row row, int num);
        bool IsRegionContains(Matrix region, int num);
        bool IsRowContains(Row row, int num);
        bool IsSafe(Matrix matrix, int row, int col, int num);
    }
}