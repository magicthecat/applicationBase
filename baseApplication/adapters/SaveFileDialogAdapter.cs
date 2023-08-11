public class SaveFileDialogAdapter : ISaveFileDialog
{
    private readonly SaveFileDialog _dialog = new SaveFileDialog();

    public string FileName
    {
        get => _dialog.FileName;
        set => _dialog.FileName = value;
    }

    public string Filter
    {
        get => _dialog.Filter;
        set => _dialog.Filter = value;
    }

    public string Title
    {
        get => _dialog.Title;
        set => _dialog.Title = value;
    }

    public DialogResult ShowDialog()
    {
        return _dialog.ShowDialog();
    }
}
