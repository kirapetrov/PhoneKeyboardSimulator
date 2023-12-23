namespace PhoneKeyboardSimulator;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Hello");

        var keyHandlerService = new KeysHandlerService();
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.KeyChar == '.')
            {
                break;
            }

            keyHandlerService.HandleKey(key.KeyChar);
        }

        Console.WriteLine();
        Console.Write("Bye");

        keyHandlerService.Dispose();
        await Task.Delay(1000).ConfigureAwait(false);
    }
}