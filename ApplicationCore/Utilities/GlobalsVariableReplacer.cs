namespace ApplicationCore.Utilities
{


    public static class GlobalVariablesReplacer
    {
        public static string ReplaceGlobalsInText(string text, Dictionary<string, string> globals)
        {
            foreach (var global in globals)
            {
                text = text.Replace($"{{{global.Key}}}", global.Value);
            }

            return text;
        }
    }

}