using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Utilities
{
   
    public class CsvParser
    {

        public string ProcessToCsv(string text)
        {
            // Correct markdown and extract global variables
            text = MarkdownParser.CorrectMarkdown(text);
            var globals = GlobalVariablesExtractor.ExtractGlobals(text);

            // Replace global variables in the text
            text = GlobalVariablesReplacer.ReplaceGlobalsInText(text, globals);

          return ConvertTextToCsv(text);
           
        }

       public string ConvertTextToCsv(string text)
{
    // Split the text into lines
    string[] lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

    List<string> headers = new List<string>();
    List<List<string>> rows = new List<List<string>>();
    List<string> currentRow = new List<string>();
    StringBuilder currentField = new StringBuilder();
    bool isReadingField = false;
    bool newHeadersEncountered = false;

    foreach (string line in lines)
    {
        if (line.StartsWith("##")) // Detect the start of a new header
        {
            if (isReadingField) // If we were already reading a field, finish it and add to the current row
            {
                currentRow.Add(EscapeCsvValue(currentField.ToString().Trim()));
                currentField.Clear();
            }
            
            // Check if we are encountering new headers
            string potentialHeader = line.Trim().Substring(2).Trim();
            if(!headers.Contains(potentialHeader))
            {
                newHeadersEncountered = true;
                headers.Add(potentialHeader); 
            }

            isReadingField = true; // Set flag to true because we're reading field data now
        }
        else if (line.StartsWith("///")) // New row separator
        {
            if (isReadingField)
            {
                currentRow.Add(EscapeCsvValue(currentField.ToString().Trim()));
                currentField.Clear();
                isReadingField = false;
            }

            if (currentRow.Count > 0)
            {
                rows.Add(new List<string>(currentRow)); // Store the completed row
                currentRow.Clear();
            }

            // Reset headers if new ones were encountered in the previous segment
            if(newHeadersEncountered)
            {
                headers.Clear();
                newHeadersEncountered = false;
            }
        }
        else if (isReadingField)
        {
            currentField.AppendLine(line); // Append the line to the current field being read
        }
    }

    // Handle the last field if any
    if (isReadingField)
    {
        currentRow.Add(EscapeCsvValue(currentField.ToString().Trim()));
        rows.Add(new List<string>(currentRow));
    }

    // Construct the CSV string
    StringBuilder csvBuilder = new StringBuilder();
    csvBuilder.AppendLine(string.Join(",", headers)); // First line is headers

    // Add all rows to the CSV
    foreach (var row in rows)
    {
        csvBuilder.AppendLine(string.Join(",", row)); // Subsequent lines are row data
    }

    return csvBuilder.ToString();
}


private string EscapeCsvValue(string value)
{
    if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
    {
        // Replace a single double-quote with two double-quotes
        value = value.Replace("\"", "\"\"");
        // Wrap the entire value in double-quotes
        value = $"\"{value}\"";
    }
    return value;
}

    }
}
