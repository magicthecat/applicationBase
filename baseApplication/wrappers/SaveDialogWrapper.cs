public class SaveFileDialogWrapper : ISaveFileDialogWrapper
{
    private readonly ISaveFileDialogFactory _dialogFactory;

    public SaveFileDialogWrapper(ISaveFileDialogFactory dialogFactory)
    {
        _dialogFactory = dialogFactory;
    }

    public string GetSavePath(string filter, string title)
    {
        ISaveFileDialog saveFileDialog = _dialogFactory.Create();
        saveFileDialog.Filter = filter;
        saveFileDialog.Title = title;

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            return saveFileDialog.FileName;
        }
        return null;
    }
}
