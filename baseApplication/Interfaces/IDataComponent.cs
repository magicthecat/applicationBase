public interface IDataComponent
{
    string MainContent { get; set; }
    string GlobalsContent { get; set; }
    event EventHandler ContentChanged;
}