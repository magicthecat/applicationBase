using System.Text.RegularExpressions;

public class MarkdownParser
{
    public enum LineType
    {
        Regular,
        Header1,
        Header2,
        Bullet,
        Divider,
        OrderedList,
        GlobalsStart,
        GlobalsEnd,
        GlobalLine
    }

    public LineType GetLineType(string line)
    {
        if (line.StartsWith("# ")) return LineType.Header1;
        if (line.StartsWith("## ")) return LineType.Header2;
        if (line.StartsWith("* ")) return LineType.Bullet;
        if (line.Trim() == "///") return LineType.Divider;
        if (Regex.IsMatch(line, @"^\d+\. ")) return LineType.OrderedList;
        if (line.Trim() == "/* GLOBALS") return LineType.GlobalsStart;
        if (line.Trim() == "*/") return LineType.GlobalsEnd;
        if (line.Contains(":")) return LineType.GlobalLine; // Simplified condition for brevity
        return LineType.Regular;
    }
}