namespace BlockchainProcessor.Domain;

public record Wallet(string Address, IEnumerable<Token> Tokens);