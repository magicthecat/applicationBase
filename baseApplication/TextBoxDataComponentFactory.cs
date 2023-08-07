public class TextBoxDataComponentFactory : IDataComponentFactory
{
    private Form form;

    public TextBoxDataComponentFactory(Form form)
    {
        this.form = form;
    }

    public IDataComponent CreateAndSetup()
    {
        // Create the SplitContainer
        SplitContainer splitContainer = new SplitContainer();
        splitContainer.Dock = DockStyle.Fill;
        splitContainer.Orientation = Orientation.Vertical; // This sets the split to be horizontal.
        form.Controls.Add(splitContainer);

        // Setup TextBox in the left side of the SplitContainer
        TextBox textBox1 = new TextBox();
        textBox1.Multiline = true;
        textBox1.Dock = DockStyle.Fill;
        splitContainer.Panel1.Controls.Add(textBox1);

        // Setup RichTextBox in the right side of the SplitContainer
        RichTextBox richTextBox1 = new RichTextBox();
        richTextBox1.Dock = DockStyle.Fill;
        richTextBox1.ReadOnly = true; // Makes the RichTextBox non-editable
        splitContainer.Panel2.Controls.Add(richTextBox1);

        // Event to update the RichTextBox as you type in the TextBox
textBox1.TextChanged += (sender, e) => 
{
    string correctedMarkdown = MarkdownProcessor.CorrectMarkdown(textBox1.Text);
    richTextBox1.Rtf = MarkdownProcessor.GetFormattedRtfFromMarkdown(correctedMarkdown);
};

        return new TextBoxAdapter(textBox1); // Assuming you still want the IDataComponent interface for other functionalities.
    }
}