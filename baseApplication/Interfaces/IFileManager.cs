public interface IFileManager
{
    event EventHandler FileSaved;
    event EventHandler FileUnsaved;
    event EventHandler FileOpened;
    event EventHandler FileNew;

    string CurrentFilePath { get; set; }
    string CurrentFileName { get; }
    bool IsSaved { get; }

    void OnFileNew();
    void WriteFile(string path, string content);
    void OnFileOpened();
    string OpenFile();
    bool SaveAs(string content);
    void MarkAsSaved();
    void MarkAsUnsaved();
    void OnFileSaved();
    void OnFileUnsaved();
}