using System.Text;
using System.Text.Json;

namespace NetBank;

internal static class Client
{
    internal static async Task StartClient(string[] urls)
    {
        if (!urls.Any())
            return;

        using var client = new HttpClient();

        Helper.WriteLineColored("Starting Client", ConsoleColor.Cyan);

        while (true)
            foreach (var url in urls)
                await PostRequest(client, url);
    }

    private static async Task PostRequest(HttpClient client, string url)
    {
        await Helper.RandDelay();

        var (endpoint, json) = GetRequest(url);
        var content = new StringContent(
            json, Encoding.UTF8, Config.JsonContentType);

        try
        {
            var response = await client.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            Helper.WriteLineColored(
                $"Successfully sent request to {endpoint}",
                ConsoleColor.Green);
        }
        catch (Exception ex)
        {
            Helper.WriteLineColored(
                $"Error sending request to {endpoint}: {ex.Message}",
                ConsoleColor.Red);
        }
    }

    private static (string endpoint, string json) GetRequest(in string url)
    {
        object request;
        string endpoint;

        if (Helper.RandBool())
        {
            endpoint = $"{url}{Config.Transaction}";
            request = new
            {
                fromAccount = Guid.NewGuid(),
                toAccount = Guid.NewGuid(),
                value = Helper.RandomDouble()
            };
        }
        else
        {
            endpoint = $"{url}{Config.Balance}";
            request = new { account = Guid.NewGuid() };
        }

        return (endpoint, JsonSerializer.Serialize(request));
    }
}