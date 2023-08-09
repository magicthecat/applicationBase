using Xunit;
using Spire.Doc;
using Spire.Doc.Fields;

public class DocumentProcessorTests
{



    [Fact]
    public void ProcessToDoc_AddsHeader1()
    {
        var processor = new DocumentProcessor();
        var text = "# Header 1";
        var doc = processor.ProcessToDoc(text);

        Assert.Single(doc.Sections[0].Paragraphs);
        Assert.Equal("Header 1", doc.Sections[0].Paragraphs[0].Text.Trim());

        DocumentObject docObject = doc.Sections[0].Paragraphs[0].ChildObjects[0];
        if (docObject is TextRange textRange)
        {
            Assert.True(textRange.CharacterFormat.Bold);
            Assert.Equal(20, textRange.CharacterFormat.FontSize);
        }
        else
        {
            throw new Exception("Expected TextRange object not found.");
        }
    }

    [Fact]
    public void ProcessToDoc_AddsHeader2()
    {
        var processor = new DocumentProcessor();
        var text = "## Header 2";
        var doc = processor.ProcessToDoc(text);

        Assert.Single(doc.Sections[0].Paragraphs);
        Assert.Equal("Header 2", doc.Sections[0].Paragraphs[0].Text.Trim());

        DocumentObject docObject = doc.Sections[0].Paragraphs[0].ChildObjects[0];
        if (docObject is TextRange textRange)
        {
            Assert.True(textRange.CharacterFormat.Bold);
            Assert.Equal(15, textRange.CharacterFormat.FontSize);
        }
        else
        {
            throw new Exception("Expected TextRange object not found.");
        }
    }



}
