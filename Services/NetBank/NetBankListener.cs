using System.Net;
using System.Text;
using System.Text.Json;

namespace NetBank;

internal static class NetBankListener
{
    internal static async Task StartListener(int port, CancellationToken token)
    {
        using var listener = new HttpListener
        { Prefixes = { $"{NetBankConfig.LocalhostUrl}:{port}/" } };

        NetBank.WriteLineColored(
            $"Starting {nameof(HttpListener)} " +
            $"on: {NetBankConfig.LocalhostUrl}:{port}",
            ConsoleColor.Cyan);
        listener.Start();

        while (!token.IsCancellationRequested)
            await ProcessRequest(listener, token);

        NetBank.WriteLineColored(
            $"Disposing of {nameof(HttpListener)}",
            ConsoleColor.Cyan);
    }

    private static async Task ProcessRequest(
        HttpListener listener, CancellationToken token)
    {
        HttpListenerContext context;

        try
        {
            context = await listener.GetContextAsync().WaitAsync(token);
        }
        catch (OperationCanceledException)
        {
            return;
        }

        object response;

        switch (context.Request.Url?.AbsolutePath)
        {
            case NetBankConfig.Transaction:
                NetBank.WriteLineColored(
                    "Received new transaction!", ConsoleColor.Magenta);
                response = new { executed = NetBank.RandBool() };
                break;
            case NetBankConfig.Balance:
                NetBank.WriteLineColored(
                    "Received new balance request!", ConsoleColor.Magenta);
                response = new { balance = NetBank.RandomDouble() };
                break;
            default:
                NetBank.WriteLineColored(
                    "Received new invalid endpoint request!",
                    ConsoleColor.Red);
                response = new { error = "Invalid endpoint" };
                context.Response.StatusCode = 404;
                break;
        }

        await NetBank.RandDelay(token);

        var json = JsonSerializer.Serialize(response);
        var content = new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(json));

        context.Response.ContentType = NetBankConfig.JsonContentType;
        context.Response.ContentLength64 = content.Length;

        try
        {
            await context.Response.OutputStream.WriteAsync(content, token);
            NetBank.WriteLineColored(
                "Sending request response!", ConsoleColor.Magenta);
        }
        finally
        {
            context.Response.Close();
        }
    }

}