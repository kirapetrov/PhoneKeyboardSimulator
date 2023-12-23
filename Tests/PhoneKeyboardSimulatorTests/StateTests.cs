using Xunit;
using PhoneKeyboardSimulator;

namespace PhoneKeyboardSimulatorTests;

public class StateTests
{
    [Fact]
    public void State_CheckDefaultValues_KeyEqualsMinCharVlueAndPressCountEqualsOne()
    {
        var sut = new State();
        Assert.Equal(char.MinValue, sut.Key);
        Assert.Equal((uint) 1, sut.PressCount);
        Assert.False(sut.HasKey);
    }

    [Fact]
    public void PressKey_PressOneTime_KeyEqualsExpectedValueAndPressCountEqualsOne()
    {
        var expected = 'a';

        var sut = new State();
        sut.PressKey(expected);

        Assert.Equal(expected, sut.Key);
        Assert.Equal((uint) 1, sut.PressCount);
        Assert.True(sut.HasKey);
    }

    [Fact]
    public void PressKey_PressTwoTimes_KeyEqualsExpectedValueAndPressCountEqualsTwo()
    {
        var expected = 'a';

        var sut = new State();
        sut.PressKey(expected);
        sut.PressKey(expected);

        Assert.Equal(expected, sut.Key);
        Assert.Equal((uint) 2, sut.PressCount);
        Assert.True(sut.HasKey);
    }

    [Fact]
    public void PressKey_PressTwoDifferentKeys_KeyEqualsExpectedValueAndPressCountEqualsOne()
    {
        var expected = 'b';

        var sut = new State();
        sut.PressKey('a');
        sut.PressKey(expected);

        Assert.Equal(expected, sut.Key);
        Assert.Equal((uint) 1, sut.PressCount);
        Assert.True(sut.HasKey);
    }

    [Fact]
    public void Reset_CheckDefaultValues_KeyEqualsMinCharVlueAndPressCountEqualsOne()
    {
        var sut = new State();
        sut.PressKey('a');
        sut.Reset();

        Assert.Equal(char.MinValue, sut.Key);
        Assert.Equal((uint) 1, sut.PressCount);
        Assert.False(sut.HasKey);
    }
}