namespace BlockchainProcessor.Application.Commands.ReadFile;

using BlockchainProcessor.Application.Common;
using BlockchainProcessor.Application.Services;

public static class ReadFileCommand
{
    public static Result<int> Process(string location)
    {
        try
        {
            string json = File.ReadAllText(location);
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