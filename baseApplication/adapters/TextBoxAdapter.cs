public class TextBoxAdapter : IDataComponent, IZoomable
{
    private TextBox textBox;

    public TextBoxAdapter(TextBox textBox)
    {
        this.textBox = textBox;
        this.textBox.TextChanged += (sender, e) => ContentChanged?.Invoke(this, e);
    }

    public string Content
    {
        get { return textBox.Text; }
        set { textBox.Text = value; }
    }


    public void ZoomIn()
    {
        textBox.Font = new Font(textBox.Font.FontFamily, textBox.Font.Size + 1);
    }

    public void ZoomOut()
    {
        if (textBox.Font.Size > 1)
            textBox.Font = new Font(textBox.Font.FontFamily, textBox.Font.Size - 1);
    }

    public event EventHandler ContentChanged;
}