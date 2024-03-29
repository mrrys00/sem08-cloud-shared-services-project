namespace NetBank;

internal static class Helper
{
    private static readonly Random Rand = new();

    internal static bool RandBool()
    {
        return Rand.Next(2) is 1;
    }

    internal static double RandomDouble()
    {
        return Math.Round(
            Rand.Next(-100_000, 100_000) + Rand.NextDouble(), 2);
    }

    internal static async Task RandDelay()
    {
        await Task.Delay(Rand.Next(Config.MinDelay, Config.MaxDelay));
    }

    internal static void WriteLineColored(
        in string message, in ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.White;
    }
}
