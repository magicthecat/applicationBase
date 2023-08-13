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
        if (line.Contains(":")) return LineType.GlobalLine; 
        return LineType.Regular;
    }

        public  string CorrectMarkdown(string text)
    {
        // Replace *string at the start of a line with * string
        text = Regex.Replace(text, @"(\n|^)\*(\w)", "$1* $2");

        // Replace #string and ##string at the start of a line with # string and ## string
        text = Regex.Replace(text, @"(\n|^)#(\w)", "$1# $2");
        text = Regex.Replace(text, @"(\n|^)##(\w)", "$1## $2");

        // Replace 1.string and 1)string at the start of a line with 1. string and 1) string
        text = Regex.Replace(text, @"(\n|^)(\d+\.)\s*(\w)", "$1$2 $3");
        text = Regex.Replace(text, @"(\n|^)(\d+\))\s*(\w)", "$1$2 $3");

    
        return text;
    }
}