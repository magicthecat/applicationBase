using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;


public class ExportMenu
{
    private IDataComponent dataComponent;
    private DocumentProcessor docProcessor = new DocumentProcessor();

    public ExportMenu(IDataComponent dataComponent)
    {
        this.dataComponent = dataComponent;
    }

    public ToolStripMenuItem GenerateMenu()
    {
        ToolStripMenuItem exportMenuItem = new ToolStripMenuItem("Export");
        
        ToolStripMenuItem exportAsDocMenuItem = new ToolStripMenuItem("Export as DOC");
        exportAsDocMenuItem.Click += ExportAsDocMenuItem_Click;
        exportMenuItem.DropDownItems.Add(exportAsDocMenuItem);

        return exportMenuItem;
    }

    private void ExportAsDocMenuItem_Click(object sender, EventArgs e)
    {
       
        string globals = "/* GLOBALS\n" + dataComponent.GlobalsContent + "\n*/\n";
        string mainContent = dataComponent.MainContent;
        string content = globals + mainContent;
        
        // Process the content to a Document using the DocumentProcessor
        Document document = docProcessor.ProcessToDoc(content);

        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            Filter = "Word Document|*.docx",
            Title = "Export as DOCX"
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            document.SaveToFile(saveFileDialog.FileName, Spire.Doc.FileFormat.Docx);
            MessageBox.Show("Document saved successfully!");
        }
    }
}
