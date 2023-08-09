public class DefaultFileService : IFileService
{
    public string ReadFile(string path)
    {
        return File.ReadAllText(path);
    }

    public void WriteFile(string path, string content)
    {
        File.WriteAllText(path, content);
    }
}

public class DefaultDialogService : IDialogService
{
    public string ShowOpenFileDialog()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            return openFileDialog.FileName;
        }
        return null;
    }

    public string ShowSaveFileDialog()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            return saveFileDialog.FileName;
        }
        return null;
    }
}
