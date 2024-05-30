namespace Sudoku.Interfaces
{
    using System.Collections.Generic;

    public interface IFileReader {
        public Matrix ReadMatrix(string filePath, char separator = ',');
    }
}
