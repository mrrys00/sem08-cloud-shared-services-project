namespace NetBank;

internal class NetBank
{
    private static readonly Random Rand = new();

    internal static bool RandBool()
    {
        return Rand.Next(2) is 1;
    }

    internal static double RandomDouble()
    {
        return Math.Round(Rand.Next(-100_000, 100_000) + Rand.NextDouble(), 2);
    }

    internal static async Task RandDelay(CancellationToken token)
    {
        try
        {
            await Task.Delay(Rand.Next(
                NetBankConfig.MinDelay, NetBankConfig.MaxDelay), token);
        }
        catch
        {
            WriteLineColored("Stopping task . . .", ConsoleColor.Yellow);
        }
    }

    internal static void WriteLineColored(
        in string message, in ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.White;
    }

    internal static async Task Main(string[] args)
    {
        if (!Validate(args, out var port, out var urls))
        {
            WriteLineColored("""
            Invalid arguments!
            Expected port and at least one url: <port> <url 1> <url 2>
            Url must be valid and absolute eg: <scheme>://<authority>
            """, ConsoleColor.Red);
            return;
        }

        using var cancellationToken = new CancellationTokenSource();

        var listener = NetBankListener.StartListener(port, cancellationToken.Token);
        var client = NetBankClient.StartClient(urls, cancellationToken.Token);

        Console.WriteLine("Press any key to stop the service . . .");
        Console.ReadKey();
        Console.WriteLine();
        cancellationToken.Cancel();

        await Task.WhenAll(listener, client);
    }

    private static bool Validate(
        in string[] args, out int port, out string[] urls)
    {
        urls = args.Skip(1).ToArray();

        if (args.Length > 1 &&
            urls.All(url => Uri.TryCreate(url, UriKind.Absolute, out _)))
            return int.TryParse(args[0], out port);

        port = default;
        return false;
    }

}