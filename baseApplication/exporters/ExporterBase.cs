public abstract class ExporterBase : IExporter, IContentPreparer
{
    public abstract void Export(string content, string savePath);

    public string PrepareContent(IDataComponent dataComponent)
    {
        string globals = "/* GLOBALS\n" + dataComponent.GlobalsContent + "\n*/\n";
        return globals + dataComponent.MainContent;
    }
}