namespace ApplicationCore.Interfaces
{
    public interface IExportService
    {
        byte[] ToDocx(string content);
        string ToCSV(string content);
    }
}