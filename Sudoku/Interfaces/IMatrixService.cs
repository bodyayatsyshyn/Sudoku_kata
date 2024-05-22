namespace Sudoku.Interfaces
{
    public interface IMatrixService
    {
        bool IsRegionsValuesUnique(Matrix matrix);
        bool IsRowValuesUnique(Row row);
        bool IsRowContainsRequiredRange(Row row);
        bool IsSquareMatrix(Matrix matrix);
        Matrix Transpose(Matrix matrix);
        bool ValuesMatches(Matrix first, Matrix second);
    }
}