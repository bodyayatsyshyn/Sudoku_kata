using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Interfaces
{
    public interface ISudokuService {
        public bool IsApplicable(Matrix matrix);
        public bool IsCorrectSolution(Matrix initialGrid, Matrix solution);
    }
}
