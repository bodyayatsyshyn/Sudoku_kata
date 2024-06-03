namespace Sudoku.Services.Tests
{
    using Sudoku.Services;
    using System;
    using System.IO;
    using Xunit;

    public class CsvWriterTests
    {
        private readonly string _testFilePath = "folder/test.csv";
        private readonly CsvWriter _writer;

        public CsvWriterTests()
        {
            _writer = new CsvWriter();
        }

        [Fact]
        public void WriteMatrix_WritesCorrectDataToFile()
        {
            // Arrange
            var matrix = new int[][]
            {
            new int[] { 1, 2, 3 },
            new int[] { 4, 5, 6 },
            new int[] { 7, 8, 9 }
            };

            // Act
            _writer.WriteMatrix(_testFilePath, matrix);

            // Assert
            var expectedContent = "1,2,3\n4,5,6\n7,8,9\n";
            var actualContent = File.ReadAllText(_testFilePath);
            Assert.Equal(expectedContent, actualContent);

            // Cleanup
            File.Delete(_testFilePath);
        }

        [Fact]
        public void WriteMatrix_UsesCustomSeparator()
        {
            // Arrange
            var matrix = new int[][]
            {
            new int[] { 1, 2, 3 },
            new int[] { 4, 5, 6 },
            new int[] { 7, 8, 9 }
            };

            // Act
            _writer.WriteMatrix(_testFilePath, matrix);

            // Assert
            var expectedContent = "1,2,3\n4,5,6\n7,8,9\n";
            var actualContent = File.ReadAllText(_testFilePath);
            Assert.Equal(expectedContent, actualContent);

            // Cleanup
            File.Delete(_testFilePath);
        }

        [Fact]
        public void WriteMatrix_CreatesEmptyFileForEmptyMatrix()
        {
            // Arrange
            var matrix = new int[][] { };

            // Act
            _writer.WriteMatrix(_testFilePath, matrix);

            // Assert
            var actualContent = File.ReadAllText(_testFilePath);
            Assert.Equal(string.Empty, actualContent);

            // Cleanup
            File.Delete(_testFilePath);
        }
    }
}

