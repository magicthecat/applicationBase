using Spire.Doc;
using Spire.Doc.Documents;
using System.Windows.Forms;
public class CsvExporter : ExporterBase
{
    public override void Export(string content, string savePath)
    {
        CsvProcessor csvProcessor = new CsvProcessor();
        string csvContent = csvProcessor.ProcessToCsv(content);
        File.WriteAllText(savePath, csvContent);
    }
}

public class DocExporter : ExporterBase
{
    public override void Export(string content, string savePath)
    {
        DocumentProcessor docProcessor = new DocumentProcessor();
        Document document = docProcessor.ProcessToDoc(content);
        document.SaveToFile(savePath, FileFormat.Docx);
    }
}