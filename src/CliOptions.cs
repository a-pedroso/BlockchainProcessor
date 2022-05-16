using CommandLine;

namespace BlockchainProcessor;

class Options
{
    [Option("read-inline",
        SetName = "read-inline",
        Required = false,
        HelpText = "Reads either a single json element, or an array of json elements representing transactions as an argument.")]
    public string? ReadInline { get; set; }

    [Option("read-file",
        SetName = "read-file",
        Required = false,
        HelpText = "Reads either a single json element, or an array of json elements representing transactions from the file in the specified location.")]
    public string? ReadFile { get; set; }

    [Option("nft",
        SetName = "nft",
        Required = false,
        HelpText = "Returns ownership information for the nft with the given id.")]
    public string? Nft { get; set; }

    [Option("wallet",
        SetName = "wallet",
        Required = false,
        HelpText = "Lists all NFTs currently owned by the wallet of the given address.")]
    public string? Wallet { get; set; }

    [Option("reset",
        SetName = "reset",
        Default = false,
        HelpText = "Deletes all data previously processed by the program.")]
    public bool Reset { get; set; }
}