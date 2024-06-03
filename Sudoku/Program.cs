namespace Sudoku
{
    using Sudoku.Interfaces;
    using Sudoku.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage(Justification = "Entry point, no logic to test")]
    public class Program
    {
        static async Task Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            builder.Services
                .AddTransient<IKataRunner, ConsoleKataRunner>()
                .AddTransient<IFileReader, CsvReader>()
                .AddTransient<IFileWriter, CsvWriter>()
                .AddTransient<ISudokuService, SudokuService>()
                .AddTransient<ISudokuChecker, SudokuChecker>()
                .AddTransient<ISudokuSolver, SudokuSolver>()
                .AddTransient<ISudokuGenerator, SudokuGenerator>()
                .AddTransient<IMatrixService, MatrixService>();
            using IHost host = builder.Build();

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            
            var kataRunner = provider.GetRequiredService<IKataRunner>();

            kataRunner.Run();

            await host.RunAsync();
        }
    }
}
