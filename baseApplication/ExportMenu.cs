using Spire.Doc;
using Spire.Doc.Documents;
using System.Windows.Forms;

public class ExportMenu
{
    private IDataComponent dataComponent;
    private readonly SaveFileDialogWrapper saveFileDialogWrapper;
    private ExporterService exporterService;

    public ExportMenu(IDataComponent dataComponent, SaveFileDialogWrapper saveFileDialogWrapper, ExporterService expService)
    {
        this.dataComponent = dataComponent;
        this.saveFileDialogWrapper = saveFileDialogWrapper;
        this.exporterService = expService; 
    }

    public ToolStripMenuItem GenerateMenu()
    {
        ToolStripMenuItem exportMenuItem = new ToolStripMenuItem("Export");

        AppendExportOption(exportMenuItem, "Export as DOC", new DocExporter(), "Word Document|*.docx", "Export as DOCX");
        AppendExportOption(exportMenuItem, "Export as CSV", new CsvExporter(), "CSV files (*.csv)|*.csv", "Export as CSV");

        return exportMenuItem;
    }

    private void AppendExportOption(ToolStripMenuItem menu, string menuItemText, IExporter exporter, string filter, string title)
    {
        ToolStripMenuItem menuItem = new ToolStripMenuItem(menuItemText);
        menuItem.Click += (sender, e) => exporterService.HandleExport(exporter, filter, title);
        menu.DropDownItems.Add(menuItem);
    }
}
