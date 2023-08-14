using System.Text.RegularExpressions;
using System.Text;


public class MarkdownParser
{
    public enum LineType
    {
        Regular,
        Header1,
        Header2,
        Header3,
        Header4,
        Bullet,
        Divider,
        OrderedListStart,  
        OrderedListEnd,
        OrderedList,    
        UnorderedListStart, 
        UnorderedListEnd,  
        UnorderedList,
        GlobalsStart,
        GlobalsEnd,
        GlobalLine
    }

    public LineType GetLineType(string line)
    {
        if (line.StartsWith("# ")) return LineType.Header1;
        if (line.StartsWith("## ")) return LineType.Header2;
        if (line.StartsWith("### ")) return LineType.Header3;
        if (line.StartsWith("#### ")) return LineType.Header4;
        if (line.StartsWith("* ")) return LineType.Bullet;
        if (line.Trim() == "///") return LineType.Divider;
        if (Regex.IsMatch(line, @"^\d+\. ")) return LineType.OrderedList;
        if (line.Trim() == "/* GLOBALS") return LineType.GlobalsStart;
        if (line.Trim() == "*/") return LineType.GlobalsEnd;
        if (line.Contains(":")) return LineType.GlobalLine; 
        return LineType.Regular;
    }

        public static string CorrectMarkdown(string text)
    {
        // Replace *string at the start of a line with * string
        text = Regex.Replace(text, @"(\n|^)\*(\w)", "$1* $2");

        // Replace #string and ##string at the start of a line with # string and ## string
        text = Regex.Replace(text, @"(\n|^)#(\w)", "$1# $2");
        text = Regex.Replace(text, @"(\n|^)##(\w)", "$1## $2");
        text = Regex.Replace(text, @"(\n|^)###(\w)", "$1### $2");
        text = Regex.Replace(text, @"(\n|^)####(\w)", "$1#### $2");

        // Replace 1.string and 1)string at the start of a line with 1. string and 1) string
        text = Regex.Replace(text, @"(\n|^)(\d+\.)\s*(\w)", "$1$2 $3");
        text = Regex.Replace(text, @"(\n|^)(\d+\))\s*(\w)", "$1$2 $3");

        return text;
    }

public LineType GetLineType(string line, string prevLine, string nextLine)
{
    if (line.StartsWith("# ")) return LineType.Header1;
    if (line.StartsWith("## ")) return LineType.Header2;
    if (line.StartsWith("### ")) return LineType.Header3;
    if (line.StartsWith("#### ")) return LineType.Header4;
    if (line.Trim() == "///") return LineType.Divider;

    if (line.StartsWith("* "))
    {
        if (nextLine == null || !nextLine.StartsWith("* "))
            return LineType.UnorderedListEnd;
        if (prevLine == null || !prevLine.StartsWith("* "))
            return LineType.UnorderedListStart;

        return LineType.Bullet; 
    }

    if (Regex.IsMatch(line, @"^\d+\. "))
    {
        if (nextLine == null || !Regex.IsMatch(nextLine, @"^\d+\. "))
            return LineType.OrderedListEnd;
        if (prevLine == null || !Regex.IsMatch(prevLine, @"^\d+\. "))
            return LineType.OrderedListStart;

        return LineType.OrderedList; 
    }

    if (line.Trim() == "/* GLOBALS") return LineType.GlobalsStart;
    if (line.Trim() == "*/") return LineType.GlobalsEnd;
    if (line.Contains(":")) return LineType.GlobalLine; 

    return LineType.Regular;
}


    public static string FormatMarkdowntoHtml(string text)
{
    StringBuilder html = new StringBuilder();
    MarkdownParser parser = new MarkdownParser();

    string[] lines = text.Split('\n');
    bool isInGlobalsBlock = false;

  for (int i = 0; i < lines.Length; i++)
{
    string currentLine = lines[i];
    string prevLine = i > 0 ? lines[i - 1] : null;
    string nextLine = i < lines.Length - 1 ? lines[i + 1] : null;

    LineType type = parser.GetLineType(currentLine, prevLine, nextLine);

    switch (type)
    {
        case LineType.Header1:
            html.AppendLine($"<h1>{currentLine.Substring(2).Trim()}</h1>");
            break;

        case LineType.Header2:
            html.AppendLine($"<h2>{currentLine.Substring(3).Trim()}</h2>");
            break;
        
        case LineType.Header3:
            html.AppendLine($"<h3>{currentLine.Substring(3).Trim()}</h3>");
            break;
        
        case LineType.Header4:
            html.AppendLine($"<h4><b>{currentLine.Substring(3).Trim()}</b></h4>");
            break;

        case LineType.Divider:
            html.AppendLine("<hr />");
            break;

        case LineType.GlobalsStart:
            html.AppendLine("<div class='globals'>");
            isInGlobalsBlock = true;
            break;

        case LineType.GlobalsEnd:
            html.AppendLine("</div>");
            isInGlobalsBlock = false;
            break;

        case LineType.GlobalLine:
            if (isInGlobalsBlock)
            {
                var parts = currentLine.Split(':');
                html.AppendLine($"<p><strong>{parts[0].Trim()}:</strong> {parts[1].Trim()}</p>");
            }
            else
            {
                html.AppendLine(currentLine);
            }
            break;

        case LineType.OrderedListStart:
            html.AppendLine("<ol>");
            goto case LineType.OrderedList;

        case LineType.OrderedList:
            var orderedMatch = Regex.Match(currentLine, @"^(\d+)\.\s(.+)");
            if (orderedMatch.Success)
            {
                html.AppendLine($"<li>{orderedMatch.Groups[2].Value}</li>");
            }
            break;

        case LineType.OrderedListEnd:
            orderedMatch = Regex.Match(currentLine, @"^(\d+)\.\s(.+)");
            if (orderedMatch.Success)
            {
                html.AppendLine($"<li>{orderedMatch.Groups[2].Value}</li>");
            }
            html.AppendLine("</ol>");
            break;

        case LineType.UnorderedListStart:
            html.AppendLine("<ul>");
            goto case LineType.Bullet;

        case LineType.Bullet:
            html.AppendLine($"<li>{currentLine.Substring(2).Trim()}</li>");
            break;

        case LineType.UnorderedListEnd:
            html.AppendLine($"<li>{currentLine.Substring(2).Trim()}</li>");
            html.AppendLine("</ul>");
            break;

        default:
            html.AppendLine(currentLine);
            break;
    }
}


    return html.ToString();
}


}