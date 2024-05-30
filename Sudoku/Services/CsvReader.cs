namespace Sudoku.Services
{
    using Sudoku.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CsvReader : IFileReader {
        public Matrix ReadMatrix(string filePath, char separator = ',') {
            var matrix = new List<IEnumerable<int>>();
            try {
                var lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    var row = line
                        .Split(separator, StringSplitOptions.TrimEntries)
                        .Select(x => string.IsNullOrEmpty(x) ? Consts.EmptyMatrixValue
                            : int.TryParse(x, out var result) 
                                ? result 
                                : throw new ArgumentException($"'{x}' cannot be parsed to int. Ensure you have the vallid CSV file and try again."));
                    matrix.Add(row);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            return matrix;
        }
    }
}
