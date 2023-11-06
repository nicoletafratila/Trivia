using System.Globalization;
using CsvHelper;
using Game.Blazor.Interfaces;

namespace Game.Blazor.Services
{
    public class CsvWriterService : IWriterService
    {
        public MemoryStream GetUserRecords<T>(IEnumerable<T> records)
        {
            var memoryStream = new MemoryStream();

            using var writer = new StreamWriter(memoryStream);
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csvWriter.WriteRecords(records);

            return memoryStream;
        }
    }
}