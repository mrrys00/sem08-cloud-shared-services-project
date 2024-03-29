namespace NetBank;

internal class NetBank
{
    internal static async Task Main(string[] args)
    {
        if (!Validate(args, out var port, out var telemetryPort, out var urls))
        {
            Helper.WriteLineColored("""
            Invalid arguments!
            Expected port, telemetry port and optional urls:
                <port> <telemetry port> <url 1> <url 2>
            Url must be valid and absolute eg:
                <scheme>://<authority>
            """, ConsoleColor.Red);
            return;
        }

        var listener = Service.StartListener(port, telemetryPort);
        var client = Client.StartClient(urls);

        Console.WriteLine("Press CTRL+C to quit . . .");

        await Task.WhenAll(listener, client);
    }

    private static bool Validate(
        in string[] args,
        out int port, out int telemetryPort, out string[] urls)
    {
        urls = args.Skip(2).ToArray();
        port = default;
        telemetryPort = default;

        if (args.Length < 2)
            return false;
        if (!urls.All(url => Uri.TryCreate(url, UriKind.Absolute, out _)))
            return false;
        return int.TryParse(args[0], out port)
               && int.TryParse(args[1], out telemetryPort);
    }

}