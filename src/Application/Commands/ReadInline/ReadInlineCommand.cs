namespace BlockchainProcessor.Application.Commands.ReadInline;

using BlockchainProcessor.Application.Common;
using BlockchainProcessor.Application.Services;

public static class ReadInlineCommand
{
    public static Result<int> Process(string json)
    {
        try
        {
            return TransactionsProcessor.Process(json);
        }
        catch (Exception ex)
        {
            List<string> errors = new()
            {
                ex.Message
            };
            return Result.Fail<int>(errors);
        }
    }
}