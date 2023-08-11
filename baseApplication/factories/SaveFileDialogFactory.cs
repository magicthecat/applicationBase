public class SaveFileDialogFactory : ISaveFileDialogFactory
{
    public ISaveFileDialog Create()
    {
        return new SaveFileDialogAdapter();
    }
}
