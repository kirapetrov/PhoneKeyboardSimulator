namespace PhoneKeyboardSimulator;

public static class GenericHelper
{
    public static char GetChar(this char[] chars, uint keyPressCount)
    {
        if (chars.Length == 0)
        {
            return char.MinValue;
        }

        var index = keyPressCount > 0 ? keyPressCount - 1 : keyPressCount;
        var normalizeIndex = index < chars.Length
            ? index
            : index % chars.Length;

        return chars[normalizeIndex];
    }
}