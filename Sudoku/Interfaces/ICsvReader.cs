namespace Sudoku.Interfaces {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IFileReader {
        public List<IEnumerable<int>> ReadMatrix(string filePath, char separator = ',');
    }
}
