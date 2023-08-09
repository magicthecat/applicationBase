using Xunit;
using baseApplication;

public class MarkdownProcessorTests
{
    [Fact]
    public void CorrectMarkdown_CorrectsBulletFormatting()
    {
        var input = "*bulletItem";
        var expected = "* bulletItem";
        var actual = MarkdownProcessor.CorrectMarkdown(input);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CorrectMarkdown_CorrectsHeader1Formatting()
    {
        var input = "#header";
        var expected = "# header";
        var actual = MarkdownProcessor.CorrectMarkdown(input);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CorrectMarkdown_CorrectsHeader2Formatting()
    {
        var input = "##subHeader";
        var expected = "## subHeader";
        var actual = MarkdownProcessor.CorrectMarkdown(input);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CorrectMarkdown_CorrectsOrderedListDotFormatting()
    {
        var input = "1.item";
        var expected = "1. item";
        var actual = MarkdownProcessor.CorrectMarkdown(input);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CorrectMarkdown_CorrectsOrderedListParenthesisFormatting()
    {
        var input = "1)item";
        var expected = "1) item";
        var actual = MarkdownProcessor.CorrectMarkdown(input);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CorrectMarkdown_HandlesExtractAndInsertGlobals()
    {
        var input = 
@"/* GLOBALS
key1 = value1
key2 = value2
array = [cat, dog, pizza]
*/
This is a regular text with {key1} and {key2}. And a {array} as well as a {array0}, {array1}, {array2} and another {array3}.";

        var expected = 
@"/* GLOBALS
key1 = value1
key2 = value2
array = [cat, dog, pizza]
*/
This is a regular text with value1 and value2. And a cat, dog, pizza as well as a cat, dog, pizza and another pizza.";

        var actual = MarkdownProcessor.CorrectMarkdown(input);
        Assert.Equal(expected, actual);
    }

}
