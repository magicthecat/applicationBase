namespace ApplicationCore.Interfaces
{
    public interface IExportService
    {
        string ToDocx(string content);
        string ToCSV(string content);
    }
}