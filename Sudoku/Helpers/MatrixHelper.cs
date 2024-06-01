namespace Sudoku.Helpers
{
    public static class MatrixHelper
    {
        public static Row GetRow(this Matrix matrix, int rowIndex)
        {
            return matrix.ToArray()[rowIndex];
        }
        public static Row GetColumn(this Matrix matrix, int colIndex)
        {
            var column = new List<int>();
            foreach(var row in matrix)
            {
                column.Add(row.ToArray()[colIndex]);
            }

            return column;
        }

        public static Matrix GetRegionForElement(this Matrix matrix, int rowIndex, int colIndex)
        {
            var regionSize = (int)Math.Sqrt(matrix.Count());

            var startRow = (rowIndex / regionSize) * regionSize;
            var startCol = (colIndex / regionSize) * regionSize;

            var matrixArray = matrix.Select(row => row.ToArray()).ToArray();

            var region = new List<Row>();

            for (int i = startRow; i < startRow + regionSize; ++i) {
                var row = new List<int>();

                for (int j = startCol; j < startCol + regionSize; ++j)
                {
                    row.Add(matrixArray[i][j]);
                }
                region.Add(row);
            }

            return region;
        }
    }
}
