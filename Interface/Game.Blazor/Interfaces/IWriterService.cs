namespace Game.Blazor.Interfaces
{
    public interface IWriterService
    {
        MemoryStream GetUserRecords<T>(IEnumerable<T> records);
    }
}