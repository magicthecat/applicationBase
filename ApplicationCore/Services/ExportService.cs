using ApplicationCore.Interfaces;
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
            // TO DO - add parsing logic.
            return content;
        }
    }
}