namespace BlockchainProcessor.Application.Commands.Wallet;

public record WalletDTO(string Address, IEnumerable<string> Tokens);