using System.Text.RegularExpressions;
using System;
using System.Linq;

public enum GlobalVariableType
{
    StringVariable,
    ArrayVariable,
}

public class GlobalVariable
{
    public GlobalVariableType Type { get; set; }
    public object Value { get; set; }
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