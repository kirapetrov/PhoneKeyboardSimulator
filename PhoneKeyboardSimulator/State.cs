namespace PhoneKeyboardSimulator;

public class State
{
    public bool HasKey => Key != char.MinValue;
    public char Key { get; private set; } = char.MinValue;
    public uint PressCount { get; private set; } = 1;

    /// <summary>
    /// Key processing
    /// </summary>
    /// <param name="key">Key</param>
    /// <returns>returns true if key changed, otherwise false</returns>
    public bool PressKey(char key)
    {
        var isKeyChanged = Key != key;
        if (isKeyChanged)
        {
            PressCount = 1;
            Key = key;
        }
        else
        {
            PressCount++;
        }

        return isKeyChanged;
    }

    public void Reset()
    {
        Key = char.MinValue;
        PressCount = 1;
    }
}