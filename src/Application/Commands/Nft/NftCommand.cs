namespace BlockchainProcessor.Application.Commands.Nft;

using BlockchainProcessor.Application.Common;
using BlockchainProcessor.Infrastructure;

public static class NftCommand
{
    public static Result<string> Process(string tokenId)
    {
        try
        {
            Storage storage = new();
            string owner = storage.GetOwner(tokenId);

            if (string.IsNullOrEmpty(owner))
            {
                List<string> errors = new()
                {
                    "unable to find wallet"
                };
                return Result.Fail<string>(errors);
            }

            return Result.Ok(owner);
        }
        catch (Exception ex)
        {
            List<string> errors = new()
            {
                ex.Message
            };
            return Result.Fail<string>(errors);
        }
    }
}