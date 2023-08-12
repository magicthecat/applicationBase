using ApplicationCore.Interfaces;
using Spire.Doc;
using Spire.Doc.Documents;
using System.IO;

namespace ApplicationCore.Services
{
    public class ExportService : IExportService
    {
        public string ToDocx(string content)
        {
            // Create a new Word document using Spire.Doc
            Document document = new Document();

            // Add a section to the document
            Section section = document.AddSection();

            // Add a paragraph to the section and set its content
            section.AddParagraph().AppendText(content);

            // Define a path for the generated DOCX file
            string docxFilePath = Path.Combine(Path.GetTempPath(), $"Note_{System.Guid.NewGuid()}.docx");

            // Save the document to the specified path
            document.SaveToFile(docxFilePath, FileFormat.Docx);

            return docxFilePath;
        }

        public string ToCSV(string content)
        {
            string csvFilePath = Path.Combine(Path.GetTempPath(), $"Note_{System.Guid.NewGuid()}.csv");

            File.WriteAllText(csvFilePath, content);

            return csvFilePath;
        }
    }
}