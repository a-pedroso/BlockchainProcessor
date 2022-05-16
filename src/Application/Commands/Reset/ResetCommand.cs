namespace BlockchainProcessor.Application.Commands.Reset;

using BlockchainProcessor.Application.Common;
using BlockchainProcessor.Infrastructure;

public static class ResetCommand
{
    public static Result Process()
    {
        try
        {
            Storage storage = new();
            storage.Reset();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            List<string> errors = new()
            {
                ex.Message
            };
            return Result.Fail(errors);
        }
    }
}