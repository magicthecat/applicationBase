using System.Text;

public class CsvProcessor
{
    private MarkdownParser parser = new MarkdownParser();


public string ProcessToCsv(string markdownText)
{
    markdownText = MarkdownProcessor.CorrectMarkdown(markdownText);

    StringBuilder csvBuilder = new StringBuilder();

    // Split the text into lines
    string[] lines = markdownText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

    List<string> headers = new List<string>();
    List<string> currentRow = new List<string>();
    StringBuilder currentField = new StringBuilder();
    bool isReadingField = false;
    string lastHeader = "";

    foreach (string line in lines)
    {
        if (line.StartsWith("##")) // Detect the start of a new field
        {
            if (isReadingField) // If we were already reading a field, finish it and add to the row
            {
                currentRow.Add(EscapeCsvValue(currentField.ToString().Trim()));
                currentField.Clear();
            }
            lastHeader = line.Trim().Substring(2).Trim();
            headers.Add(lastHeader);  // Add the current header to the headers list
            isReadingField = true; // Set flag to true because we're reading field data now
        }
        else if (line.StartsWith("///")) // New row
        {
            // If we were reading a field, finish it and add to the row
            if (isReadingField)
            {
                currentRow.Add(EscapeCsvValue(currentField.ToString().Trim()));
                currentField.Clear();
                isReadingField = false;
            }

            // If it's our first row, then append headers first
            if (csvBuilder.Length == 0)
            {
                csvBuilder.AppendLine(string.Join(",", headers));
            }

            // Add current row to CSV
            csvBuilder.AppendLine(string.Join(",", currentRow));
            currentRow.Clear();
        }
        else if (isReadingField)
        {
            currentField.AppendLine(line); // Append the line to the current field being read
        }
    }

    // Add any remaining data
    if (isReadingField)
    {
        currentRow.Add(EscapeCsvValue(currentField.ToString().Trim()));
    }

    if (csvBuilder.Length == 0) // In case the headers were never added
    {
        csvBuilder.AppendLine(string.Join(",", headers));
    }

    if (currentRow.Count > 0)
    {
        csvBuilder.AppendLine(string.Join(",", currentRow));
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