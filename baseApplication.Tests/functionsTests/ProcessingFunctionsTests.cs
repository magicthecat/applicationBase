using Xunit;
using baseApplication;
using System.Collections.Generic;

public class ProcessingFunctionsTests
{
    [Fact]
    public void ExtractGlobals_CorrectlyExtractsGlobals()
    {
        var input = @"/* GLOBALS
                      testOne = value1
                      testTwo = [value2a, value2b]
                      */
                      Some other text here";

        var result = ProcessingFunctions.ExtractGlobals(input);

        Assert.Equal(2, result.Count);
        Assert.Equal(GlobalVariableType.StringVariable, result["testOne"].Type);
        Assert.Equal("value1", result["testOne"].Value);
        Assert.Equal(GlobalVariableType.ArrayVariable, result["testTwo"].Type);
        Assert.Equal(new string[] { "value2a", "value2b" }, result["testTwo"].Value);
    }

    [Fact]
    public void InsertGlobalsIntoString_CorrectlyInsertsGlobals()
    {
        var input = @"Here's a string with a {testOne} and an array value {testTwo}. Array first item: {testTwo0}";

        var globals = new Dictionary<string, GlobalVariable>
        {
            { "testOne", new GlobalVariable { Type = GlobalVariableType.StringVariable, Value = "value1" } },
            { "testTwo", new GlobalVariable { Type = GlobalVariableType.ArrayVariable, Value = new string[] { "value2a", "value2b" } } }
        };

        var result = ProcessingFunctions.InsertGlobalsIntoString(input, globals);

        Assert.Equal("Here's a string with a value1 and an array value value2a, value2b. Array first item: value2a", result);
    }

    [Fact]
    public void ExtractAndInsertGlobals_CorrectlyProcessesString()
    {
        var input = @"Some text here
                      /* GLOBALS
                      testOne = value1
                      testTwo = [value2a, value2b]
                      */
                      Here's a string with a {testOne} and an array value {testTwo}. Array first item: {testTwo0}";

        var result = ProcessingFunctions.ExtractAndInsertGlobals(input);

        Assert.Contains("Here's a string with a value1 and an array value value2a, value2b. Array first item: value2a", result);
    }
}