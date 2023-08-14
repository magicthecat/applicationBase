using ApplicationCore.Interfaces;
using ApplicationCore.Utilities;
using Spire.Doc;
using Spire.Doc.Documents;

namespace ApplicationCore.Services
{
    public class TranslateService : ITranslateService
    {

      public string ToHtml(string content, string variables)
      {
        content = GlobalVariablesProcessor.ProcessGlobals("/* GLOBALS " + variables + " */ " + content);
        content = MarkdownParser.CorrectMarkdown(content);
        content = MarkdownParser.FormatMarkdowntoHtml(content);
        return content;
      }
    }
}