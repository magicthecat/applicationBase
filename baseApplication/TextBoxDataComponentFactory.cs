public class TextBoxDataComponentFactory : IDataComponentFactory
{
    private Form form;

    public TextBoxDataComponentFactory(Form form)
    {
        this.form = form;
    }

    public IDataComponent CreateAndSetup()
    {
        // Create the SplitContainer for Left and Right sides
        SplitContainer mainSplitContainer = new SplitContainer();
        mainSplitContainer.Dock = DockStyle.Fill;
        mainSplitContainer.Orientation = Orientation.Vertical;
        form.Controls.Add(mainSplitContainer);

        // Create the SplitContainer for the two TextBox controls on the Left
        SplitContainer leftSplitContainer = new SplitContainer();
        leftSplitContainer.Dock = DockStyle.Fill;
        leftSplitContainer.Orientation = Orientation.Horizontal;
        mainSplitContainer.Panel1.Controls.Add(leftSplitContainer);

        // Setup TextBox for GLOBALS on the top of the left side
        TextBox globalsTextBox = new TextBox();
        globalsTextBox.Multiline = true;
        globalsTextBox.Dock = DockStyle.Fill;
        leftSplitContainer.Panel1.Controls.Add(globalsTextBox);

        // Setup main TextBox on the bottom of the left side
        TextBox mainTextBox = new TextBox();
        mainTextBox.Multiline = true;
        mainTextBox.Dock = DockStyle.Fill;
        leftSplitContainer.Panel2.Controls.Add(mainTextBox);

        // Setup RichTextBox on the right side
        RichTextBox richTextBox1 = new RichTextBox();
        richTextBox1.Dock = DockStyle.Fill;
        richTextBox1.ReadOnly = true;
        mainSplitContainer.Panel2.Controls.Add(richTextBox1);

        // Event to update the RichTextBox when typing in the TextBox
        EventHandler updateRichTextBox = (sender, e) =>
        {
            string globals = "/* GLOBALS\n" + globalsTextBox.Text + "\n*/\n";
            string content = mainTextBox.Text;
            string combinedContent = globals + content;

            string correctedMarkdown = MarkdownProcessor.CorrectMarkdown(combinedContent);
            richTextBox1.Rtf = MarkdownProcessor.GetFormattedRtfFromMarkdown(correctedMarkdown);
        };

        mainTextBox.TextChanged += updateRichTextBox;
        globalsTextBox.TextChanged += updateRichTextBox;

        return new TextBoxAdapter(mainTextBox); // Assuming you still want the IDataComponent interface for other functionalities.
    }
}