namespace ApplicationCore.Utilities
{

    public static class GlobalVariablesExtractor
    {
      public static Dictionary<string, string> ExtractGlobals(string text)
{
    Dictionary<string, string> globals = new Dictionary<string, string>();

    var globalsStart = text.IndexOf("/* GLOBALS");
    var globalsEnd = text.IndexOf("*/");

    if (globalsStart == -1 || globalsEnd == -1)
        return globals;  // No globals section

    var globalsSection = text.Substring(globalsStart + 10, globalsEnd - globalsStart - 10);

    var lines = globalsSection.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

    foreach (var line in lines)
    {
        var parts = line.Split('=');
        if (parts.Length == 2)
        {
            var key = parts[0].Trim();
            var value = parts[1].Trim();

            // Check if value represents an array
            if (value.StartsWith("[") && value.EndsWith("]"))
            {
                var arrayValues = value.Substring(1, value.Length - 2).Split(',');
                for (int i = 0; i < arrayValues.Length; i++)
                {
                    globals[$"{key}{i}"] = arrayValues[i].Trim();
                }
            }
            else
            {
                globals[key] = value;
            }
        }
    }

    return globals;
}

    }


}