using System;
using System.Windows.Forms;

namespace baseApplication
{
    public class MenuManager
    {
        private IDataComponent dataComponent;
        private FileManager fileManager;
        public event EventHandler FileSaved;


        public MenuManager(IDataComponent dataComponent, FileManager fileManager)
        {
            this.dataComponent = dataComponent;
            this.fileManager = fileManager;
        }

        public MenuStrip InitializeMenuStrip()
        {
            MenuStrip menuStrip = new MenuStrip();
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");

            ToolStripMenuItem newMenuItem = new ToolStripMenuItem("New");
            newMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newMenuItem.Click += NewMenuItem_Click;

            ToolStripMenuItem openMenuItem = new ToolStripMenuItem("Open");
            openMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openMenuItem.Click += OpenMenuItem_Click;

            ToolStripMenuItem saveMenuItem = new ToolStripMenuItem("Save");
            saveMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveMenuItem.Click += SaveMenuItem_Click;

            ToolStripMenuItem saveAsMenuItem = new ToolStripMenuItem("Save As");
            saveAsMenuItem.Click += SaveAsMenuItem_Click;

            fileMenu.DropDownItems.Add(newMenuItem);
            fileMenu.DropDownItems.Add(openMenuItem);
            fileMenu.DropDownItems.Add(saveMenuItem);
            fileMenu.DropDownItems.Add(saveAsMenuItem);

            menuStrip.Items.Add(fileMenu);

            return menuStrip;
        }
 private void OpenMenuItem_Click(object sender, EventArgs e)
{
    string content = fileManager.OpenFile();
    if (content != null)
    {
        dataComponent.MainContent = content;
        
        // Inform listeners that an open file action took place
        fileManager.OnFileOpened();
    }
}

private void SaveMenuItem_Click(object sender, EventArgs e)
{
    if (string.IsNullOrEmpty(fileManager.CurrentFilePath))
    {
        SaveAsMenuItem_Click(sender, e);
        return;
    }

    try
    {
        fileManager.WriteFile(fileManager.CurrentFilePath, dataComponent.MainContent);
        // After writing to the file, trigger the FileSaved event.
        fileManager.OnFileSaved();
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error saving file: " + ex.Message);
    }
}

private void SaveAsMenuItem_Click(object sender, EventArgs e)
{
    if (fileManager.SaveAs(dataComponent.MainContent))
    {
        MessageBox.Show("File saved successfully!");
    }
    else
    {
        MessageBox.Show("Error saving file. Please try again.");
    }
}

private void NewMenuItem_Click(object sender, EventArgs e)
{
    if (!string.IsNullOrEmpty(dataComponent.MainContent))
    {
        DialogResult result = MessageBox.Show("Do you want to save changes?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

        if (result == DialogResult.Yes)
        {
            SaveMenuItem_Click(sender, e);
        }
        else if (result == DialogResult.Cancel)
        {
            return;
        }
    }
    dataComponent.MainContent = string.Empty;
    fileManager.CurrentFilePath = null;
    fileManager.MarkAsUnsaved(); 

    fileManager.OnFileNew();
}
    }
}