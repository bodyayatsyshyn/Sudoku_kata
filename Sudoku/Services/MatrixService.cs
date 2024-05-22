using Sudoku.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Services
{
    public class MatrixService : IMatrixService
    {
        public bool IsSquareMatrix(Matrix matrix)
        {
            var rowCount = matrix.Count();
            return !matrix.Any(row => row.Count() != rowCount);
        }

        public bool IsRowValuesUnique(Row row)
        {
            return row.Count() == row.Distinct().Count();
        }

        public Matrix Transpose(Matrix matrix)
        {
            return matrix.Select((_, colIndex) => matrix.Select(row => row.ElementAt(colIndex))).ToList();
        }

        public bool IsRowContainsRequiredRange(Row row)
        {
            return Enumerable.Range(1, row.Count()).All(x => row.Contains(x));
        }

        public bool IsRegionsValuesUnique(Matrix matrix)
        {
            var regionRowSize = 3;
            var regionSize = 9;

            for (int i = 0; i < matrix.Count(); i += regionRowSize)
            {
                var regionRows = matrix.Skip(i).Take(regionRowSize);
                for (int j = 0; j < matrix.Count(); j += regionRowSize)
                {
                    if (regionRows.SelectMany(x => x.Skip(j).Take(regionRowSize)).Distinct().Count() != regionSize)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool ValuesMatches(Matrix initialGrid, Matrix solution)
        {
            var initGridSize = initialGrid.SelectMany(x => x).Count();
            var solutionGridSize = solution.SelectMany(x => x).Count();

            if (initGridSize != solutionGridSize)
            {
                return false;
            }

            var rowLenght = initialGrid.Count();
            var init2DArray = initialGrid.Select(x => x.ToArray()).ToArray();
            var sol2DArray = solution.Select(x => x.ToArray()).ToArray();

            for (int i = 0; i < rowLenght; ++i)
            {
                for (int j = 0; j < rowLenght; ++j)
                {
                    try
                    {
                        if (init2DArray[i][j] == Consts.EmptyMatrixValue)
                        {
                            continue;
                        }

                        if (init2DArray[i][j] != sol2DArray[i][j])
                        {
                            return false;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
