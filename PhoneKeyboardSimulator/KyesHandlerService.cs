namespace PhoneKeyboardSimulator;

public class KeysHandlerService : IDisposable
{
    private readonly TimeSpan waitUserTimeout = TimeSpan.FromSeconds(1);

    private readonly State state = new();

    private readonly IReadOnlyDictionary<char, char[]> availableChars =
        new Dictionary<char, char[]>
        {
            {'2', ['a', 'b', 'c']},
            {'3', ['d', 'e', 'f']},
            {'4', ['g', 'h', 'i']},
            {'5', ['j', 'k', 'l']},
            {'6', ['m', 'n', 'o']},
            {'7', ['p', 'q', 'r', 's']},
            {'8', ['t', 'u', 'v']},
            {'9', ['w', 'x', 'y', 'z']}
        };

    private readonly IReadOnlyDictionary<char, Action<State>> specialChars =
        new Dictionary<char, Action<State>>
        {
            {'0', ConsoleHelper.AppendSpace},
            {'#', ConsoleHelper.AppendNewLine},
            {'*', ConsoleHelper.Backspace}
        };

    private readonly Timer waitUserTimer;

    private int usingResource = 0;
    private char[]? currentChars;

    public KeysHandlerService()
    {
        waitUserTimer = new Timer(
            WaitUserTimerHandler,
            state,
            Timeout.InfiniteTimeSpan,
            Timeout.InfiniteTimeSpan);
    }

    private void WaitUserTimerHandler(object? obj)
    {
        if (obj is State state &&
            0 == Interlocked.Exchange(ref usingResource, 1))
        {
            ConsoleHelper.ForwardCursor();
            state.Reset();
            Interlocked.Exchange(ref usingResource, 0);
        }
    }

    public void HandleKey(char keyChar)
    {
        if (0 != Interlocked.Exchange(ref usingResource, 1))
        {
            return;
        }

        if (availableChars.Keys.Contains(keyChar))
        {
            HandleAvailableKey(keyChar);
        }
        else if (specialChars.Keys.Contains(keyChar))
        {
            HandleSpecialKey(keyChar);
        }

        Interlocked.Exchange(ref usingResource, 0);
    }

    private void HandleAvailableKey(char key)
    {
        waitUserTimer.Change(waitUserTimeout, Timeout.InfiniteTimeSpan);
        var hasKey = state.HasKey;
        if (state.PressKey(key))
        {
            currentChars = availableChars[key];
            if (hasKey)
            {
                ConsoleHelper.ForwardCursor();
            }
        }

        if (currentChars is not null)
        {
            ConsoleHelper.AppendCharWithCursorBackward(
                currentChars.GetChar(state.PressCount));
        }
    }

    private void HandleSpecialKey(char key)
    {
        waitUserTimer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
        specialChars[key].Invoke(state);
        state.Reset();
    }

    public void Dispose() => waitUserTimer.Dispose();
}