namespace Sudoku.Services
{
    using Sudoku.Interfaces;
    public class CsvWriter : IFileWriter
    {
        public void WriteMatrix(string filepath, Matrix matrix, char separator = ',')
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filepath)!);

            var writer = File.CreateText(filepath);
            writer.NewLine = "\n";
            foreach (var row in matrix)
            {
                writer.WriteLine(string.Join(separator, row));
                
            }

            writer.Close();
        }
    }
}
