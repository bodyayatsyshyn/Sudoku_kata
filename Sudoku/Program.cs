namespace Sudoku
{
    using Sudoku.Interfaces;
    using Sudoku.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        private enum Actions
        {
            Level_0 = 0,
            Level_1 = 1,
            Level_2 = 2,

            Exit = -1,
        }

        static async Task Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            builder.Services
                .AddTransient<IFileReader, CsvReader>()
                .AddTransient<ISudokuService, SudokuService>()
                .AddTransient<IMatrixService, MatrixService>();
            using IHost host = builder.Build();

            Execute(host.Services);

            await host.RunAsync();
        }

        public static void Execute(IServiceProvider hostProvider)
        {
            using IServiceScope serviceScope = hostProvider.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            var reader = provider.GetRequiredService<IFileReader>();
            var sudocuService = provider.GetRequiredService<ISudokuService>();

            var options = Enum.GetNames(typeof(Actions));
            int selectedIndex = 0;
            var pathToFiles = @"..\..\..\..\data_samples";
            Console.CursorVisible = false;

            while (true)
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
                        Actions action;
                        Enum.TryParse(options[selectedIndex], out action);

                        switch (action)
                        {
                            case Actions.Level_0:
                                var level0FilePath = $"{pathToFiles}\\lvl0\\Sudoku_test.csv";

                                if (sudocuService.IsApplicable(reader.ReadMatrix(level0FilePath)))
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

                            case Actions.Level_1:
                                var level1InitFilePath = $"{pathToFiles}\\lvl1\\init.csv";
                                var level1SolutionFilePath = $"{pathToFiles}\\lvl1\\solution.csv";

                                if (sudocuService.IsCorrectSolution(reader.ReadMatrix(level1InitFilePath), reader.ReadMatrix(level1SolutionFilePath)))
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

                            case Actions.Level_2:
                                var level2InitFilePath = $"{pathToFiles}\\lvl2\\init.csv";
                                var matrix = reader.ReadMatrix(level2InitFilePath);
                                if (sudocuService.IsSolvable(ref matrix))
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

                            case Actions.Exit:
                                return;

                        }

                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        public static void PrintMatrix(Matrix matrix)
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
