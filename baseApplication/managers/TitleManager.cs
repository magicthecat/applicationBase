namespace baseApplication
{

public class TitleManager
{


    private readonly Form _form;
    private readonly IFileManager _fileManager;
    private readonly ITitle _titleInterface;

public TitleManager(ITitle titleInterface, IFileManager fileManager)
{
    _titleInterface = titleInterface ?? throw new ArgumentNullException(nameof(titleInterface));
    _fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));

    _fileManager.FileSaved += (sender, e) => UpdateTitle();
    _fileManager.FileNew += (sender, e) => UpdateTitle();
    _fileManager.FileOpened += (sender, e) => UpdateTitle();
    _fileManager.FileUnsaved += (sender, e) => UpdateTitle();
}


    public void UpdateTitle()
    {
        if (string.IsNullOrEmpty(_fileManager.CurrentFileName))
        {
            _titleInterface.Title = "Untitled*";
        }
        else
        {
            _titleInterface.Title = _fileManager.IsSaved ? _fileManager.CurrentFileName : _fileManager.CurrentFileName + "*";
        }
    }
}


}
