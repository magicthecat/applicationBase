using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

public class MarkdownProcessor
{
public static string GetFormattedRtfFromMarkdown(string markdownText)
{
    RichTextBox richTextBox = new RichTextBox();
    var lines = markdownText.Split('\n');
    List<string> orderedList = null;
    int orderedListLineNum = 1;
    bool isGlobalSection = false;
    var parser = new MarkdownParser();

    for (int i = 0; i < lines.Length; i++)
    {
        var line = lines[i];
        var type = parser.GetLineType(line);

        switch (type)
        {
            case MarkdownParser.LineType.GlobalsStart:
                isGlobalSection = true;

                // Check for the closing tag
                while (++i < lines.Length && lines[i].Trim() != "*/")
                {
                    // If you need to process the content inside the GLOBALS section, do so here.
                }

                // If we've exited the loop and didn't find the end of the GLOBALS block
                if (i >= lines.Length)
                {
                    throw new Exception("Unmatched /* GLOBALS block detected.");
                }
                continue;
                
            case MarkdownParser.LineType.GlobalsEnd:
                isGlobalSection = false;
                continue;

            case MarkdownParser.LineType.GlobalLine:
                continue;

            case MarkdownParser.LineType.OrderedList:
                if (orderedList == null) orderedList = new List<string>();
                orderedList.Add(line.Substring(line.IndexOf('.') + 2));
                break;

            case MarkdownParser.LineType.Header1:
                richTextBox.SelectionFont = new Font(richTextBox.Font, FontStyle.Bold);
                richTextBox.AppendText(line.Substring(2) + "\n");
                break;

            case MarkdownParser.LineType.Header2:
                richTextBox.SelectionFont = new Font(richTextBox.Font, FontStyle.Bold | FontStyle.Italic);
                richTextBox.AppendText(line.Substring(3) + "\n");
                break;

            case MarkdownParser.LineType.Bullet:
                richTextBox.SelectionBullet = true;
                richTextBox.AppendText(line.Substring(2) + "\n");
                richTextBox.SelectionBullet = false;
                break;

            case MarkdownParser.LineType.Divider:
                richTextBox.AppendText(new string('_', 30) + "\n");
                break;

            case MarkdownParser.LineType.Regular:
                richTextBox.AppendText(line + "\n");
                break;
        }

        if (type != MarkdownParser.LineType.OrderedList && orderedList != null)
        {
            foreach (var orderedLine in orderedList)
            {
                richTextBox.SelectionIndent = 10;
                richTextBox.AppendText(orderedListLineNum + ". " + orderedLine + "\n");
                orderedListLineNum++;
                richTextBox.SelectionIndent = 0;
            }
            orderedList = null;
            orderedListLineNum = 1;
        }
    }

    // Handle the case where the last line of the text is part of an ordered list
    if (orderedList != null)
    {
        foreach (var orderedLine in orderedList)
        {
            richTextBox.SelectionIndent = 10;
            richTextBox.AppendText(orderedListLineNum + ". " + orderedLine + "\n");
            orderedListLineNum++;
            richTextBox.SelectionIndent = 0;
        }
    }

    var result = richTextBox.Rtf;
    richTextBox.Dispose();
    return result;
}
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

        // Initialize dictionary to store global variables
        Dictionary<string, string> globals = new Dictionary<string, string>();

        // Split the text into lines
        string[] lines = text.Split('\n');

        // Process each line
        for (int i = 0; i < lines.Length; i++)
        {
            // Check for GLOBALS block start
            if (lines[i].Trim() == "/* GLOBALS")
            {
                // Process lines until end of GLOBALS block
                while (lines[++i].Trim() != "*/")
                {
                    // Split line into name and value
                    string[] parts = lines[i].Split(':', 2);
                    if (parts.Length == 2)
                    {
                        // Remove '=' from variable name and trim both name and value
                        string name = parts[0].Trim().TrimStart('=');
                        string value = parts[1].Trim();

                        // Add to globals dictionary
                        globals[name] = value;
                    }
                }
            }

            // Replace variables in line
            foreach (var global in globals)
            {
                lines[i] = lines[i].Replace("{" + global.Key + "}", global.Value);
            }
        }

        // Combine lines back into single string
        text = string.Join("\n", lines);

        return text;
    }

    }

