using ApplicationCore.Interfaces;
using ApplicationCore.Utilities;
using Spire.Doc;
using Spire.Doc.Documents;

namespace ApplicationCore.Services
{
    public class ExportService : IExportService
    {
        public byte[] ToDocx(string content)
        {
            Document document = new Document();
            Section section = document.AddSection();
            section.AddParagraph().AppendText(content);

            using (MemoryStream ms = new MemoryStream())
            {
                document.SaveToStream(ms, FileFormat.Docx);
                return ms.ToArray();
            }
        }

       public string ToCSV(string content)
    {
        CsvParser csvParser = new CsvParser();
        content = csvParser.ProcessToCsv(content); 
        return content;
    }
    }
}