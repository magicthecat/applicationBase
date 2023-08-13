using System.Text.RegularExpressions;

public class MarkdownProcessor
{

    public static string CorrectMarkdown(string text)
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