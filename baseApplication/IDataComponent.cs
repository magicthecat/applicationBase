public interface IDataComponent
{
    string Content { get; set; }
    event EventHandler ContentChanged;
}