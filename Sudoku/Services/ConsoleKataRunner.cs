namespace Sudoku.Services
{
    using Sudoku.Enums;
    using Sudoku.Interfaces;
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage(Justification = "static Console class couldn't be tested")]
    public class ConsoleKataRunner(ISudokuChecker sudokuChecker, ISudokuSolver sudokuSolver, ISudokuGenerator sudokuGenerator, IFileReader fileReader, IFileWriter fileWriter) : IKataRunner
    {
        private const string _rootPath = @"..\..\..\..\data_samples";

        public void Run()
        {

            var options = Enum.GetNames(typeof(KataActions));
            int selectedIndex = 0;
            Console.CursorVisible = false;

            while (true)
            {
                ShowMenu(options, selectedIndex);

                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex <= 0 ? options.Length : selectedIndex) - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = selectedIndex >= options.Length - 1 ? 0 : selectedIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        Console.WriteLine($"Selected: {options[selectedIndex]}");
                        KataActions action;
                        Enum.TryParse(options[selectedIndex], out action);

                        ExecuteAction(action);

                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        private void ShowMenu(string[] options, int selectedIndex)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("System:");
            Console.ResetColor();
            Console.WriteLine(" Choose something (use arrow keys and enter).");
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("-> ");
                }
                else
                {
                    Console.Write("   ");
                }
                Console.WriteLine(options[i]);
                Console.ResetColor();
            }
        }

        private void ExecuteAction(KataActions action)
        {
            switch (action)
            {
                case KataActions.Level_0:
                    var level0FilePath = $"{_rootPath}\\lvl0\\Sudoku_test.csv";

                    if (sudokuChecker.IsApplicable(fileReader.ReadMatrix(level0FilePath)))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(Consts.Responses.CompliesWithRules);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(Consts.Responses.DoesNotComplyWithRules);
                    }
                    Console.ResetColor();
                    Console.WriteLine();
                    break;

                case KataActions.Level_1:
                    var level1InitFilePath = $"{_rootPath}\\lvl1\\init.csv";
                    var level1SolutionFilePath = $"{_rootPath}\\lvl1\\solution.csv";

                    if (sudokuChecker.IsCorrectSolution(fileReader.ReadMatrix(level1InitFilePath), fileReader.ReadMatrix(level1SolutionFilePath)))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(Consts.Responses.CorrectSolution);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(Consts.Responses.CorrectSolution);
                    }
                    Console.ResetColor();
                    Console.WriteLine();
                    break;

                case KataActions.Level_2:
                    var level2InitFilePath = $"{_rootPath}\\lvl2\\init.csv";
                    var matrix = fileReader.ReadMatrix(level2InitFilePath);
                    if (sudokuSolver.IsSolvable(ref matrix))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(Consts.Responses.SudokuSolved);
                        Console.ResetColor();
                        PrintMatrix(matrix);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(Consts.Responses.SudokuNotSolved);
                    }

                    Console.ResetColor();
                    Console.WriteLine();

                    break;
                case KataActions.Level_3:
                    var level3FilePath = $"{_rootPath}\\lvl3\\init.csv";
                    Console.Write("Sudoku dimention: ");
                    var dimentionStr = Console.ReadLine();
                    Console.Write("Empty cells: ");
                    var emptyCellsStr = Console.ReadLine();
                    if (!int.TryParse(dimentionStr, out var dimention) || !int.TryParse(emptyCellsStr, out var emptyCells))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("It seems to be too complicated for you, ask someone to help and try again later.");
                        Console.ResetColor();
                        Environment.Exit(-1);
                    }
                    else
                    {
                        var result = sudokuGenerator.GenerateSudoku(dimention, emptyCells);

                        PrintMatrix(result);
                        fileWriter.WriteMatrix(level3FilePath, result);

                        Console.ResetColor();
                        Console.WriteLine();
                    }
                    break;

                case KataActions.Exit:
                    Environment.Exit(0);
                    break;
            }
        }

        private void PrintMatrix(Matrix matrix)
        {
            foreach (var row in matrix)
            {
                foreach (var element in row)
                    Console.Write($"{element} ");
                Console.WriteLine();
            }
        }
    }
}
