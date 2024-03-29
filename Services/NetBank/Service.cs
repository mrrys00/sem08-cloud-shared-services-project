using System.Net;
using System.Text;
using System.Text.Json;

namespace NetBank;

internal static class Service
{
    private static Telemetry? _telemetry;

    internal static async Task StartListener(int port, int telemetryPort)
    {
        if (port <= 0)
            return;

        _telemetry = telemetryPort > 0
            ? new Telemetry(telemetryPort) : default;
        var serviceUrl = $"http://{Config.Localhost}:{port}";

        using var listener = new HttpListener
        { Prefixes = { $"{serviceUrl}/" } };

        Helper.WriteLineColored(
            $"Starting service on: {serviceUrl}",
            ConsoleColor.Cyan);
        listener.Start();

        while (true)
            await ProcessRequest(listener);
    }

    private static async Task ProcessRequest(HttpListener listener)
    {
        var context = await listener.GetContextAsync();
        object response;
        string telemetryKey;

        switch (context.Request.Url?.AbsolutePath)
        {
            case Config.Transaction:
                Helper.WriteLineColored(
                    "Received new transaction!", ConsoleColor.Magenta);
                response = new { executed = Helper.RandBool() };
                telemetryKey = $"transaction-{Guid.NewGuid()}";
                break;
            case Config.Balance:
                Helper.WriteLineColored(
                    "Received new balance request!", ConsoleColor.Magenta);
                response = new { balance = Helper.RandomDouble() };
                telemetryKey = $"balance-{Guid.NewGuid()}";
                break;
            default:
                Helper.WriteLineColored(
                    "Received new invalid endpoint request!",
                    ConsoleColor.Red);
                response = new { error = "Invalid endpoint" };
                telemetryKey = $"error-{Guid.NewGuid()}";
                context.Response.StatusCode = 404;
                break;
        }

        await Helper.RandDelay();

        var json = JsonSerializer.Serialize(response);
        var content = new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(json));

        context.Response.ContentType = Config.JsonContentType;
        context.Response.ContentLength64 = content.Length;

        try
        {
            await context.Response.OutputStream.WriteAsync(content);
            _telemetry?.Produce(telemetryKey, json);
            Helper.WriteLineColored(
                "Sending request response!", ConsoleColor.Magenta);
        }
        finally
        {
            context.Response.Close();
        }
    }

}