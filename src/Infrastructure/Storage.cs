namespace BlockchainProcessor.Infrastructure;

using BlockchainProcessor.Domain;
using System.Text.Json;

public class Storage
{
    Dictionary<string, Token> _storage;
    private string _path = "storage.txt";

    public Storage()
    {
        Load();
    }

    public bool SaveTransactions(List<Transaction> transactions)
    {
        foreach (Transaction transaction in transactions)
        {
            SaveTransaction(transaction);
        }
        Save();
        return true;
    }

    public string GetOwner(string tokenId)
    {
        var token = _storage[tokenId];
        return token == null ? string.Empty : token.Wallet;
    }

    public Wallet GetWallet(string address)
    {
        var tokens = _storage.Where(w => w.Value.Wallet.Equals(address))
                             .Select(s => s.Value);

        return new Wallet(address, tokens);
    }

    public bool Reset()
    {
        File.Delete(_path);
        _storage = new Dictionary<string, Token>();
        return true;
    }

    private bool SaveTransaction(Transaction transaction)
    {
        return transaction.Type switch
        {
            TransactionType.Mint => ProcessMint(transaction),
            TransactionType.Burn => ProcessBurn(transaction),
            TransactionType.Transfer => ProcessTransfer(transaction),
            _ => throw new InvalidOperationException()
        };
    }

    private bool ProcessMint(Transaction transaction)
    {
        _storage.Add(transaction.TokenId, new Token(transaction.TokenId, transaction.Address));
        return true;
    }

    private bool ProcessBurn(Transaction transaction)
    {
        return _storage.Remove(transaction.TokenId);
    }

    private bool ProcessTransfer(Transaction transaction)
    {
        _storage.Remove(transaction.TokenId);
        _storage.Add(transaction.TokenId, new Token(transaction.TokenId, transaction.To));
        return true;
    }

    void Save()
    {
        var json = JsonSerializer.Serialize(_storage);

        File.WriteAllText(_path, json);
    }

    void Load()
    {
        if (!File.Exists(_path))
        {
            File.WriteAllText(_path, string.Empty);
            _storage = new Dictionary<string, Token>();
        }
        else
        {
            string json = File.ReadAllText(_path);

            if (string.IsNullOrEmpty(json))
                _storage = new Dictionary<string, Token>();
            else
                _storage = JsonSerializer.Deserialize<Dictionary<string, Token>>(json);
        }
    }
}