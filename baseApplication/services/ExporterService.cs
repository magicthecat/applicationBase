public class ExporterService
{
    private readonly IDataComponent dataComponent;
    private readonly SaveFileDialogWrapper saveFileDialogWrapper;

    public ExporterService(IDataComponent dataComponent, SaveFileDialogWrapper saveFileDialogWrapper)
    {
        this.dataComponent = dataComponent;
        this.saveFileDialogWrapper = saveFileDialogWrapper;
    }

    public void HandleExport(IExporter exporter, string filter, string title)
    {
        string content;
        if (exporter is IContentPreparer preparer)
        {
            content = preparer.PrepareContent(dataComponent);
        }
        else
        {
            content = dataComponent.MainContent;
        }
        string savePath = saveFileDialogWrapper.GetSavePath(filter, title);
        if (!string.IsNullOrEmpty(savePath))
        {
            exporter.Export(content, savePath);
            MessageBox.Show($"{title.Split(' ')[2]} exported successfully!");
        }
    }
}
