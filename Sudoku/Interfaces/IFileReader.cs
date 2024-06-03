namespace Sudoku.Interfaces
{
    public interface IFileReader {
        public Matrix ReadMatrix(string filePath, char separator = ',');
    }
}
