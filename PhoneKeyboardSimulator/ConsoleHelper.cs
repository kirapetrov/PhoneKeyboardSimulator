namespace PhoneKeyboardSimulator;

public static class ConsoleHelper
{
    public static void ForwardCursor() => Console.CursorLeft++;

    public static void AppendCharWithCursorBackward(char @char) => 
        Console.Write($"{@char}\b");

    public static void AppendNewLine(State state) => 
        Console.WriteLine();

    public static void AppendSpace(State state)
    {
        if (state.HasKey)
        {
            ForwardCursor();
        }

        Console.Write(' ');
    }

    public static void Backspace(State state)
    {
        if (state.HasKey)
        {
            Console.Write(" \b");
        }
        else
        {
            Console.Write("\b \b");
        }
    }
}