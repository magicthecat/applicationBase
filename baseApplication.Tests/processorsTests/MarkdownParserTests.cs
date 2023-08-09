using Xunit;
using baseApplication;

public class MarkdownParserTests
{
    private readonly MarkdownParser parser = new MarkdownParser();

    [Fact]
    public void GetLineType_Header1_ReturnsHeader1()
    {
        var lineType = parser.GetLineType("# This is a header");
        Assert.Equal(MarkdownParser.LineType.Header1, lineType);
    }

    [Fact]
    public void GetLineType_Header2_ReturnsHeader2()
    {
        var lineType = parser.GetLineType("## This is a sub-header");
        Assert.Equal(MarkdownParser.LineType.Header2, lineType);
    }

    [Fact]
    public void GetLineType_Bullet_ReturnsBullet()
    {
        var lineType = parser.GetLineType("* An item");
        Assert.Equal(MarkdownParser.LineType.Bullet, lineType);
    }

    [Fact]
    public void GetLineType_Divider_ReturnsDivider()
    {
        var lineType = parser.GetLineType("///");
        Assert.Equal(MarkdownParser.LineType.Divider, lineType);
    }

    [Fact]
    public void GetLineType_OrderedList_ReturnsOrderedList()
    {
        var lineType = parser.GetLineType("1. First item");
        Assert.Equal(MarkdownParser.LineType.OrderedList, lineType);
    }

    [Fact]
    public void GetLineType_GlobalsStart_ReturnsGlobalsStart()
    {
        var lineType = parser.GetLineType("/* GLOBALS");
        Assert.Equal(MarkdownParser.LineType.GlobalsStart, lineType);
    }

    [Fact]
    public void GetLineType_GlobalsEnd_ReturnsGlobalsEnd()
    {
        var lineType = parser.GetLineType("*/");
        Assert.Equal(MarkdownParser.LineType.GlobalsEnd, lineType);
    }

    [Fact]
    public void GetLineType_GlobalLine_ReturnsGlobalLine()
    {
        var lineType = parser.GetLineType("key: value");
        Assert.Equal(MarkdownParser.LineType.GlobalLine, lineType);
    }

    [Fact]
    public void GetLineType_Regular_ReturnsRegular()
    {
        var lineType = parser.GetLineType("This is a regular line.");
        Assert.Equal(MarkdownParser.LineType.Regular, lineType);
    }
}