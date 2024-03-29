namespace NetBank;

internal static class Config
{
    internal const int MinDelay = 1_000, MaxDelay = 4_000;

    internal const string
        Transaction = "/transaction",
        Balance = "/balance",
        JsonContentType = "application/json",
        Localhost = "localhost",
        TopicName = nameof(NetBank),
        ProducerName = "producer",
        GroupId = "group";
}