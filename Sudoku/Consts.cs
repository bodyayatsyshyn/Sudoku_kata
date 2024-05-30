namespace Sudoku
{
    public class Consts
    {
        public static int EmptyMatrixValue => -1;

        public static class Responses
        {
            public static string IncorrectSolution => "The proposed solution is incorrect";

            public static string CorrectSolution => "The proposed solution is correct";

            public static string CompliesWithRules => "The input complies with Sudoku's rules.";

            public static string DoesNotComplyWithRules => "The input doesn't comply with Sudoku's rules.";

        }
    }
}
