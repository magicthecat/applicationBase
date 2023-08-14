using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions; 

namespace ApplicationCore.Utilities
{
   


    public class GlobalVariablesProcessor
    {

        private static string RemoveGlobalVariablesString (string text) {
            var globalsPattern = @"/\* GLOBALS.*?\*/";
            text = Regex.Replace(text, globalsPattern, string.Empty, RegexOptions.Singleline);
            return text;
        }

        public static string ProcessGlobals(string text) {
           var globals = GlobalVariablesExtractor.ExtractGlobals(text);
            text = GlobalVariablesReplacer.ReplaceGlobalsInText(text, globals);

            return RemoveGlobalVariablesString(text);

        }

    }
}
