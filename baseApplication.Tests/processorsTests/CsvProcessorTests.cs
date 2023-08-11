using Xunit;

public class CsvProcessorTests
{
    private readonly CsvProcessor _csvProcessor;

    public CsvProcessorTests()
    {
        _csvProcessor = new CsvProcessor();
    }

    [Fact]
    public void TestBasicCsvConversion()
    {
        // Arrange
        string markdownInput = "## Type \nCat \n\n## Number\n13\n\n##Acceptance Criteria\n\n*1\n*2\n*3\n\n///\n\n## Type\n\nDog\n\n## Number\n\n33\n\n## Acceptance Criteria\n\n1. pizza\n3. pizza2\n5. cake";
        string expectedOutput = "Type,Number,Acceptance Criteria\r\nCat,13,\"* 1\r\n* 2\r\n* 3\"\r\nDog,33,\"1. pizza\r\n3. pizza2\r\n5. cake\"\r\n";

        // Act
        string csvOutput = _csvProcessor.ProcessToCsv(markdownInput);

        // Assert
        Assert.Equal(expectedOutput, csvOutput);
    }

    [Fact]
    public void TestCsvConversionWithCommasInValue()
    {
        // Arrange
        string markdownInput = "## Value \nSome, value with a comma\n\n///";
        string expectedOutput = "Value\r\n\"Some, value with a comma\"\r\n";

        // Act
        string csvOutput = _csvProcessor.ProcessToCsv(markdownInput);

        // Assert
        Assert.Equal(expectedOutput, csvOutput);
    }

    [Fact]
    public void TestCsvConversionWithQuotesInValue()
    {
        // Arrange
        string markdownInput = "## Value \nA \"quoted\" value\n\n///";
        string expectedOutput = "Value\r\n\"A \"\"quoted\"\" value\"\r\n";

        // Act
        string csvOutput = _csvProcessor.ProcessToCsv(markdownInput);

        // Assert
        Assert.Equal(expectedOutput, csvOutput);
    }

    // Additional tests can be added to cover more edge cases and variations
}