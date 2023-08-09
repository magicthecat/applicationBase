using System.Text.RegularExpressions;
using System;
using System.Linq;

public enum GlobalVariableType
{
    StringVariable,
    ArrayVariable,
    // Add other variable types as needed in the future
}

public class GlobalVariable
{
    public GlobalVariableType Type { get; set; }
    public object Value { get; set; }
}



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


public static class ProcessingFunctions
{


    public static Dictionary<string, GlobalVariable> ExtractGlobals(string text)
    {
        Dictionary<string, GlobalVariable> globals = new Dictionary<string, GlobalVariable>();
        string[] lines = text.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Trim() == "/* GLOBALS")
            {
                while (lines[++i].Trim() != "*/")
                {
                    string[] parts = lines[i].Split(new[] { '=' }, 2);
                    if (parts.Length == 2)
                    {
                        string name = parts[0].Trim();
                        string value = parts[1].Trim();
                        globals[name] = ProcessingHandlers.ExtractValue(value);
                    }
                }

                if (i == lines.Length - 1 && lines[i].Trim() != "*/")
                {
                    throw new InvalidOperationException("No closing tag found for GLOBALS block.");
                }
            }
        }
        return globals;
    }

    public static string InsertGlobalsIntoString(string text, Dictionary<string, GlobalVariable> globals)
    {
        foreach (var global in globals)
        {
            text = ProcessingHandlers.InsertValue(text, global.Key, global.Value);
        }
        return text;
    }


    public static string ExtractAndInsertGlobals(string text)
    {
        var globals = ExtractGlobals(text);
        return InsertGlobalsIntoString(text, globals);
    }



}