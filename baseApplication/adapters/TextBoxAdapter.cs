public class TextBoxAdapter : IDataComponent, IZoomable
{
    private TextBox mainTextBox;
    private TextBox globalsTextBox;

    public TextBoxAdapter(TextBox mainTextBox, TextBox globalsTextBox)
    {
        this.mainTextBox = mainTextBox;
        this.globalsTextBox = globalsTextBox;

        this.mainTextBox.TextChanged += (sender, e) => ContentChanged?.Invoke(this, e);
        this.globalsTextBox.TextChanged += (sender, e) => ContentChanged?.Invoke(this, e);
    }

    public string MainContent
    {
        get { return mainTextBox.Text; }
        set { mainTextBox.Text = value; }
    }

    public string GlobalsContent
    {
        get { return globalsTextBox.Text; }
        set { globalsTextBox.Text = value; }
    }

public void ZoomIn()
{
    mainTextBox.Font = new Font(mainTextBox.Font.FontFamily, mainTextBox.Font.Size + 1);
    globalsTextBox.Font = new Font(globalsTextBox.Font.FontFamily, globalsTextBox.Font.Size + 1);
}

public void ZoomOut()
{
    if (mainTextBox.Font.Size > 1)
        mainTextBox.Font = new Font(mainTextBox.Font.FontFamily, mainTextBox.Font.Size - 1);

    if (globalsTextBox.Font.Size > 1)
        globalsTextBox.Font = new Font(globalsTextBox.Font.FontFamily, globalsTextBox.Font.Size - 1);
}

    public event EventHandler ContentChanged;
}