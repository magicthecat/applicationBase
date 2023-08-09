using System;
using Xunit;
using baseApplication;  

public class ProcessingHandlersTests
{
    [Fact]
    public void ExtractValue_WithArrayString_ReturnsArrayVariable()
    {
        // Arrange
        var input = "[value1, value2, value3]";
        
        // Act
        var result = ProcessingHandlers.ExtractValue(input);

        // Assert
        Assert.Equal(GlobalVariableType.ArrayVariable, result.Type);
        Assert.Contains("value1", result.Value as string[]);
        Assert.Contains("value2", result.Value as string[]);
        Assert.Contains("value3", result.Value as string[]);
    }

    [Fact]
    public void ExtractValue_WithString_ReturnsStringVariable()
    {
        // Arrange
        var input = "sampleString";

        // Act
        var result = ProcessingHandlers.ExtractValue(input);

        // Assert
        Assert.Equal(GlobalVariableType.StringVariable, result.Type);
        Assert.Equal("sampleString", result.Value.ToString());
    }

    [Fact]
    public void InsertValue_WithStringVariable_ReplacesCorrectly()
    {
        // Arrange
        var text = "Hello {name}!";
        var variable = new GlobalVariable
        {
            Type = GlobalVariableType.StringVariable,
            Value = "John"
        };

        // Act
        var result = ProcessingHandlers.InsertValue(text, "name", variable);

        // Assert
        Assert.Equal("Hello John!", result);
    }

    [Fact]
    public void InsertValue_WithArrayVariable_ReplacesCorrectly()
    {
        // Arrange
        var text = "Numbers: {numbers}";
        var variable = new GlobalVariable
        {
            Type = GlobalVariableType.ArrayVariable,
            Value = new string[] { "one", "two", "three" }
        };

        // Act
        var result = ProcessingHandlers.InsertValue(text, "numbers", variable);

        // Assert
        Assert.Equal("Numbers: one, two, three", result);
    }

    [Fact]
    public void InsertValue_WithArrayVariableAndIndex_ReplacesCorrectly()
    {
        // Arrange
        var text = "First: {numbers0}, Second: {numbers1}";
        var variable = new GlobalVariable
        {
            Type = GlobalVariableType.ArrayVariable,
            Value = new string[] { "one", "two", "three" }
        };

        // Act
        var result = ProcessingHandlers.InsertValue(text, "numbers", variable);

        // Assert
        Assert.Equal("First: one, Second: two", result);
    }

}
