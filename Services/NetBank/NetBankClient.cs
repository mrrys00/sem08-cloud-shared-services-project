using System.Text;
using System.Text.Json;

namespace NetBank;

internal static class NetBankClient
{
    internal static async Task StartClient(
        string[] urls, CancellationToken token)
    {
        using var client = new HttpClient();

        NetBank.WriteLineColored(
            $"Starting {nameof(HttpClient)}", ConsoleColor.Cyan);

        while (!token.IsCancellationRequested)
            foreach (var url in urls)
                await PostRequest(client, url, token);

        NetBank.WriteLineColored(
            $"Disposing of {nameof(HttpClient)}", ConsoleColor.Cyan);
    }

    private static async Task PostRequest(
        HttpClient client, string url, CancellationToken token)
    {
        await NetBank.RandDelay(token);

        var (endpoint, json) = GetRequest(url);
        var content = new StringContent(
            json, Encoding.UTF8, NetBankConfig.JsonContentType);

        try
        {
            var response = await client.PostAsync(endpoint, content, token);
            response.EnsureSuccessStatusCode();

            NetBank.WriteLineColored(
                $"Successfully sent request to {endpoint}",
                ConsoleColor.Green);
        }
        catch (Exception ex)
        {
            NetBank.WriteLineColored(
                $"Error sending request to {endpoint}: {ex.Message}",
                ConsoleColor.Red);
        }
    }

    private static (string endpoint, string json) GetRequest(in string url)
    {
        object request;
        string endpoint;

        if (NetBank.RandBool())
        {
            endpoint = $"{url}{NetBankConfig.Transaction}";
            request = new
            {
                fromAccount = Guid.NewGuid(),
                toAccount = Guid.NewGuid(),
                value = NetBank.RandomDouble()
            };
        }
        else
        {
            endpoint = $"{url}{NetBankConfig.Balance}";
            request = new { account = Guid.NewGuid() };
        }

        return (endpoint, JsonSerializer.Serialize(request));
    }
}