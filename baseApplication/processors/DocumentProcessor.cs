using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

public class DocumentProcessor
{
    private MarkdownParser parser = new MarkdownParser();

    public Document ProcessToDoc(string text)
    {
        // Format the markdown first
        text = MarkdownProcessor.CorrectMarkdown(text);

        string[] lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        Document document = new Document();
        Section section = document.AddSection();

        bool isGlobalSection = false;

        foreach (string line in lines)
        {
            var type = parser.GetLineType(line);

            if (isGlobalSection && type != MarkdownParser.LineType.GlobalsEnd)
                continue;  // Skip lines inside the GLOBALS section

            switch (type)
            {
                case MarkdownParser.LineType.GlobalsStart:
                    isGlobalSection = true;
                    break;

                case MarkdownParser.LineType.GlobalsEnd:
                    isGlobalSection = false;
                    break;

                case MarkdownParser.LineType.OrderedList:
                    var paragraph = section.AddParagraph();
                    paragraph.ListFormat.ApplyNumberedStyle();
                    paragraph.AppendText(line.Substring(line.IndexOf('.') + 2));
                    break;

                case MarkdownParser.LineType.Header1:
                    var titleParagraph = section.AddParagraph();
                    TextRange titleTextRange = titleParagraph.AppendText(line.Substring(2));
                    titleTextRange.CharacterFormat.Bold = true;
                    titleTextRange.CharacterFormat.FontSize = 20;
                    break;

                case MarkdownParser.LineType.Header2:
                    var subTitleParagraph = section.AddParagraph();
                    TextRange subTitleTextRange = subTitleParagraph.AppendText(line.Substring(3));
                    subTitleTextRange.CharacterFormat.Bold = true;
                    subTitleTextRange.CharacterFormat.FontSize = 15;
                    break;

                case MarkdownParser.LineType.Bullet:
                    var bulletParagraph = section.AddParagraph();
                    bulletParagraph.ListFormat.ApplyBulletStyle();
                    bulletParagraph.AppendText(line.Substring(2));
                    break;

                case MarkdownParser.LineType.Regular:
                    var regularParagraph = section.AddParagraph();
                    regularParagraph.AppendText(line);
                    break;
            }
        }

        return document;
    }
}