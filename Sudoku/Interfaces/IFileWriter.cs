namespace Sudoku.Interfaces
{
    public interface IFileWriter
    {
        void WriteMatrix(string filepath, Matrix matrix, char separator = ',');
    }
}