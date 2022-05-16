using BlockchainProcessor.Application.Common;
using BlockchainProcessor.Infrastructure;

namespace BlockchainProcessor.Application.Commands.Wallet;

public static class WalletCommand
{
    public static Result<WalletDTO> Process(string address)
    {
        try
        {
            Storage storage = new();
            Domain.Wallet wallet = storage.GetWallet(address);

            if (wallet is null)
            {
                List<string> errors = new()
                {
                    "Invalid wallet address"
                };
                return Result.Fail<WalletDTO>(errors);
            }

            return Result.Ok(new WalletDTO(wallet.Address, wallet.Tokens.Select(s => s.Id)));
        }
        catch (Exception ex)
        {
            List<string> errors = new()
            {
                ex.Message
            };
            return Result.Fail<WalletDTO>(errors);
        }
    }
}