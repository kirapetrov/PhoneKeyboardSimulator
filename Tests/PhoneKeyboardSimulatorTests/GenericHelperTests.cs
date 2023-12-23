using Xunit;
using PhoneKeyboardSimulator;

namespace PhoneKeyboardSimulatorTests;

public class GenericHelperTests
{
    [Fact]
    public void GetChar_PassEmptyCharsArray_ReturnsMinCharValue()
    {
        var actual = GenericHelper.GetChar([], 1);
        Assert.Equal(char.MinValue, actual);
    }

    [Fact]
    public void GetChar_PassZeroKeyPressCount_ReturnsFirstChar()
    {
        var testChars = GetTestChars();
        var actual = GenericHelper.GetChar(testChars, 0);
        Assert.Equal(testChars.First(), actual);
    }

    [Fact]
    public void GetChar_PassKeyPressCountLessThanLengthOfArray_ReturnsCharValue()
    {
        var testChars = GetTestChars();
        var actual = GenericHelper.GetChar(testChars, 2);
        Assert.Equal(testChars[1], actual);
    }

    [Fact]
    public void GetChar_PassKeyPressCountGreaterThanLengthOfArray_ReturnsCharValue()
    {
        var testChars = GetTestChars();
        var actual = GenericHelper.GetChar(testChars, 5);
        Assert.Equal(testChars[1], actual);
    }

    private char[] GetTestChars() => ['a', 'b', 'c'];
}