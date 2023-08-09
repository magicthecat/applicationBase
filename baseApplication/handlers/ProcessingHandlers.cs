using System.Text.RegularExpressions;


public static class ProcessingHandlers
{
    public static GlobalVariable ExtractValue(string value)
    {
        if (value.StartsWith("[") && value.EndsWith("]"))
        {
            string[] arrayValues = value.Substring(1, value.Length - 2).Split(',').Select(v => v.Trim()).ToArray();
            return new GlobalVariable
            {
                Type = GlobalVariableType.ArrayVariable,
                Value = arrayValues
            };
        }
        else
        {
            return new GlobalVariable
            {
                Type = GlobalVariableType.StringVariable,
                Value = value
            };
        }
    }


    public static string InsertValue(string text, string key, GlobalVariable variable)
    {
        switch (variable.Type)
        {
            case GlobalVariableType.StringVariable:
                return text.Replace("{" + key + "}", variable.Value.ToString());

            case GlobalVariableType.ArrayVariable:
                if (variable.Value is string[] arrayValue)
                {
                    text = text.Replace("{" + key + "}", string.Join(", ", arrayValue));
                    // And then handle indices, etc.
                    text = InsertArrayValue(text, key, arrayValue);
                }
                return text;

            default:
                throw new NotSupportedException($"Unsupported GlobalVariableType: {variable.Type}");
        }
    }

    private static string InsertArrayValue(string text, string key, string[] arrayValue)
    {
        Regex arrayPattern = new Regex(@"(?<key>\w+)(?<index>\d+)");
        foreach (Match match in arrayPattern.Matches(text))
        {
            if (match.Groups["key"].Value == key)
            {
                int index = int.Parse(match.Groups["index"].Value);
                index = Math.Clamp(index, 0, arrayValue.Length - 1); 
                text = text.Replace("{" + match.Value + "}", arrayValue[index]);
            }
        }
        return text;
    }
}