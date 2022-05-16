using BlockchainProcessor.Application.Common;
using BlockchainProcessor.Domain;
using BlockchainProcessor.Infrastructure;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlockchainProcessor.Application.Services;

public static class TransactionsProcessor
{
    public static Result<int> Process(string json)
    {
        List<Transaction> transactions = ParseJson(json);

        Storage storage = new();

        bool saved = storage.SaveTransactions(transactions);
        if (!saved)
        {
            List<string> errors = new()
            {
                "Error on storing transactions"
            };
            return Result.Fail<int>(errors);
        }

        return Result.Ok(transactions.Count);
    }

    private static List<Transaction> ParseJson(string json)
    {
        List<Transaction> transactions = new();

        using var jsonDoc = JsonDocument.Parse(json);

        JsonSerializerOptions options = new()
        {
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        if (jsonDoc.RootElement.ValueKind.Equals(JsonValueKind.Array))
        {
            transactions = jsonDoc.Deserialize<List<Transaction>>(options);
        }
        else
        {
            Transaction item = jsonDoc.Deserialize<Transaction>(options);
            transactions.Add(item);
        }

        return transactions;
    }
}