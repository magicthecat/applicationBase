using System;
using System.IO;

namespace baseApplication
{
  public class FileManager : IFileManager
    {
        private readonly IFileService _fileService;
        private readonly IDialogService _dialogService;
        private string currentFilePath = null;

        public FileManager(IFileService fileService, IDialogService dialogService)
        {
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
        }

        public event EventHandler FileSaved;
        public event EventHandler FileUnsaved;
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

        public bool IsSaved { get; private set; }

        public virtual void OnFileNew()
        {
            FileNew?.Invoke(this, EventArgs.Empty);
        }

        public void WriteFile(string path, string content)
        {
            _fileService.WriteFile(path, content);
            MarkAsSaved();
        }
        public virtual void OnFileOpened()
        {
            FileOpened?.Invoke(this, EventArgs.Empty);
        }

        public string OpenFile()
        {
            string path = _dialogService.ShowOpenFileDialog();
            if (!string.IsNullOrEmpty(path))
            {
                CurrentFilePath = path;
                IsSaved = true;  
                return _fileService.ReadFile(path);
            }
            return null;
        }

        public bool SaveAs(string content)
        {
            string path = _dialogService.ShowSaveFileDialog();
            if (!string.IsNullOrEmpty(path))
            {
                _fileService.WriteFile(path, content);
                CurrentFilePath = path;
                MarkAsSaved();
                OnFileSaved();
                return true;
            }
            return false;
        }

        public void MarkAsSaved()
        {
            IsSaved = true;
            OnFileSaved();
        }

        public void MarkAsUnsaved()
        {
            IsSaved = false;
            OnFileUnsaved();
        }

        public virtual void OnFileSaved()
        {
            FileSaved?.Invoke(this, EventArgs.Empty);
        }

        public virtual void OnFileUnsaved()
        {
            FileUnsaved?.Invoke(this, EventArgs.Empty);
        }
    }
}