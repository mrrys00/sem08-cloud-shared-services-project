namespace NetBank;

internal static class NetBankConfig
{
    internal const int MinDelay = 1_000, MaxDelay = 3_000;

    internal const string
        Transaction = "/transaction",
        Balance = "/balance",
        JsonContentType = "application/json",
        LocalhostUrl = "http://localhost";
}