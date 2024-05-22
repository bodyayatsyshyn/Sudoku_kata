using Xunit;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Services;

namespace Sudoku.Services.Tests
{
    public class CsvReaderTests
    {
        private readonly CsvReader _csvReader;

        public CsvReaderTests()
        {
            _csvReader = new CsvReader();
        }

        [Fact]
        public void ReadMatrix_ValidFile_ReturnsCorrectMatrix()
        {
            // Arrange
            string filePath = "valid_file.csv";
            File.WriteAllLines(filePath, ["1, 2, 3", "4, 5, 6", "7, 8, 9"]);

            // Act
            var matrix = _csvReader.ReadMatrix(filePath);

            // Assert
            Assert.Equal(3, matrix.Count);
            Assert.Equal([1, 2, 3], matrix[0]);
            Assert.Equal([4, 5, 6], matrix[1]);
            Assert.Equal([7, 8, 9], matrix[2]);

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void ReadMatrix_MissingValues_ReturnsMatrixWithDefaultValues()
        {
            // Arrange
            string filePath = "missing_values.csv";
            File.WriteAllLines(filePath, ["1, , 3", ", 5, ", "7, , 9"]);

            // Act
            var matrix = _csvReader.ReadMatrix(filePath);

            // Assert
            Assert.Equal(3, matrix.Count);
            Assert.Equal([1, -1, 3], matrix[0]);
            Assert.Equal([-1, 5, -1], matrix[1]);
            Assert.Equal([7, -1, 9], matrix[2]);

            // Cleanup
            File.Delete(filePath);
        }
    }
}