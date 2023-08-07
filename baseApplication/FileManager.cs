using System;
using System.IO;
using System.Windows.Forms;

namespace baseApplication
{
    public class FileManager
    {
        private string currentFilePath = null;
        public event EventHandler FileSaved;
        public event EventHandler FileOpened;


    public event EventHandler FileNew;


        public string CurrentFilePath
        {
            get { return currentFilePath; }
            set { currentFilePath = value; }
        }


public string CurrentFileName 
{ 
    get
    {
        if (string.IsNullOrEmpty(CurrentFilePath))
            return null;
        return Path.GetFileName(CurrentFilePath);
    }
}

public virtual void OnFileNew()
{
    FileNew?.Invoke(this, EventArgs.Empty);
}


public virtual void OnFileOpened()
{
    FileOpened?.Invoke(this, EventArgs.Empty);
}
public bool IsSaved { get; private set; }
        public string ReadFile(string path)
        {
            return File.ReadAllText(path);
        }

   public void WriteFile(string path, string content)
{
    File.WriteAllText(path, content);
    MarkAsSaved();
}



           public void MarkAsSaved()
    {
        IsSaved = true;
    }

    public void MarkAsUnsaved()
    {
        IsSaved = false;
    }

public string OpenFile()
{
    OpenFileDialog openFileDialog = new OpenFileDialog();
    if (openFileDialog.ShowDialog() == DialogResult.OK)
    {
        CurrentFilePath = openFileDialog.FileName; // Ensure this line is present
        IsSaved = true;  
        return ReadFile(openFileDialog.FileName);
    }
    return null;
}
    public virtual void OnFileSaved()
{
    FileSaved?.Invoke(this, EventArgs.Empty);
}

public bool SaveAs(string content)
{
    SaveFileDialog saveFileDialog = new SaveFileDialog();
    if (saveFileDialog.ShowDialog() == DialogResult.OK)
    {
        WriteFile(saveFileDialog.FileName, content);
        CurrentFilePath = saveFileDialog.FileName;
        MarkAsSaved(); 
        OnFileSaved();
        return true;
    }
    return false;
}
    }
}
