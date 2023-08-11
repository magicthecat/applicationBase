using Spire.Doc;
using Spire.Doc.Documents;
using System.Windows.Forms;

public class ExportMenu
{
    private IDataComponent dataComponent;
    private DocumentProcessor docProcessor = new DocumentProcessor();
    private readonly SaveFileDialogWrapper saveFileDialogWrapper;

    public ExportMenu(IDataComponent dataComponent, SaveFileDialogWrapper saveFileDialogWrapper)
    {
        this.dataComponent = dataComponent;
        this.saveFileDialogWrapper = saveFileDialogWrapper;
    }

    public ToolStripMenuItem GenerateMenu()
    {
        ToolStripMenuItem exportMenuItem = new ToolStripMenuItem("Export");
        
        ToolStripMenuItem exportAsDocMenuItem = new ToolStripMenuItem("Export as DOC");
        exportAsDocMenuItem.Click += ExportAsDocMenuItem_Click;
        exportMenuItem.DropDownItems.Add(exportAsDocMenuItem);

        ToolStripMenuItem exportAsCsvMenuItem = new ToolStripMenuItem("Export as CSV");
        exportAsCsvMenuItem.Click += ExportAsCsvMenuItem_Click;
        exportMenuItem.DropDownItems.Add(exportAsCsvMenuItem);

        return exportMenuItem;
    }

    private void ExportAsCsvMenuItem_Click(object sender, EventArgs e)
    {
        string savePath = saveFileDialogWrapper.GetSavePath("CSV files (*.csv)|*.csv", "Export as CSV");
        string globals = "/* GLOBALS\n" + dataComponent.GlobalsContent + "\n*/\n";
        string mainContent = dataComponent.MainContent;
        string content = globals + mainContent;

        if (!string.IsNullOrEmpty(savePath))
        {
        CsvProcessor csvProcessor = new CsvProcessor();
        string csvContent = csvProcessor.ProcessToCsv(content);
        File.WriteAllText(savePath, csvContent);
        MessageBox.Show("CSV exported successfully!");
        }
    }
    private void ExportAsDocMenuItem_Click(object sender, EventArgs e)
    {
        string globals = "/* GLOBALS\n" + dataComponent.GlobalsContent + "\n*/\n";
        string mainContent = dataComponent.MainContent;
        string content = globals + mainContent;
        
        // Process the content to a Document using the DocumentProcessor
        Document document = docProcessor.ProcessToDoc(content);

        string savePath = saveFileDialogWrapper.GetSavePath("Word Document|*.docx", "Export as DOCX");

        if (!string.IsNullOrEmpty(savePath))
        {
            document.SaveToFile(savePath, FileFormat.Docx);
            MessageBox.Show("Document saved successfully!");
        }
    }
}
