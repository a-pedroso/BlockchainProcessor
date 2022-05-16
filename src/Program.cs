using BlockchainProcessor;
using BlockchainProcessor.Application.Commands.Nft;
using BlockchainProcessor.Application.Commands.ReadFile;
using BlockchainProcessor.Application.Commands.ReadInline;
using BlockchainProcessor.Application.Commands.Reset;
using BlockchainProcessor.Application.Commands.Wallet;
using BlockchainProcessor.Application.Common;
using CommandLine;

var result = CommandLine.Parser.Default.ParseArguments<Options>(args)
                                       .MapResult(opts => RunOptionsAndReturnExitCode(opts), //in case parser success
                                                  errs => HandleParseError(errs)); //in case parser fail

static int RunOptionsAndReturnExitCode(Options o)
{
    var exitCode = 0;

    if (!string.IsNullOrEmpty(o.ReadFile))
    {
        Result<int> result = ReadFileCommand.Process(o.ReadFile);
        if (result.IsSuccess)
            Console.WriteLine($"Read {result.Data} transaction(s)");
        else
            Console.WriteLine($"Error on Read transaction(s).\n {string.Join("\n", result.Errors)}");

        return exitCode;
    }

    if (!string.IsNullOrEmpty(o.ReadInline))
    {
        Result<int> result = ReadInlineCommand.Process(o.ReadInline);
        if (result.IsSuccess)
            Console.WriteLine($"Read {result.Data} transaction(s)");
        else
            Console.WriteLine($"Error on Read transaction(s).\n {string.Join("\n", result.Errors)}");

        return exitCode;
    }

    if (!string.IsNullOrEmpty(o.Nft))
    {
        Result<string> result = NftCommand.Process(o.Nft);
        if (result.IsSuccess)
            Console.WriteLine($"Token {o.Nft} is owned by {result.Data}");
        else
            Console.WriteLine($"Token {o.Nft} is not owned by any wallet");

        return exitCode;
    }

    if (!string.IsNullOrEmpty(o.Wallet))
    {
        Result<WalletDTO> result = WalletCommand.Process(o.Wallet);

        if (result.IsSuccess && result.Data.Tokens.Any())
        {
            Console.WriteLine($"Wallet {o.Wallet} holds {result.Data.Tokens.Count()} Tokens:\n{string.Join("\n", result.Data.Tokens)}");
        }
        else
        {
            Console.WriteLine($"Wallet {o.Wallet} holds no Tokens");
        }

        return exitCode;
    }

    if (o.Reset)
    {
        Result result = ResetCommand.Process();
        if (result.IsSuccess)
            Console.WriteLine("Program was reset");
        else
            Console.WriteLine("Reset Error");

        return exitCode;
    }

    return exitCode;
}

//in case of errors or --help or --version
static int HandleParseError(IEnumerable<Error> errs)
{
    var result = -2;
    Console.WriteLine("errors {0}", errs.Count());
    if (errs.Any(x => x is HelpRequestedError || x is VersionRequestedError))
        result = -1;
    Console.WriteLine("Exit code {0}", result);
    return result;
}