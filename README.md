# BlockchainProcessor

## Nuget Packages in use besides base SDK
https://github.com/commandlineparser/commandline

## Pre Requisites
- .NET 6 SDK installed -> https://dotnet.microsoft.com/en-us/download/dotnet/6.0

## Run App
in your shell, go to csproj file location

execute some cmds in this order to simulate the base example
```text
dotnet run --read-file ..\transactions.json
```
```text
dotnet run --nft 0xA000000000000000000000000000000000000000
```
```text
dotnet run --nft 0xB000000000000000000000000000000000000000
```
```text
dotnet run --nft 0xC000000000000000000000000000000000000000
```
```text
dotnet run --nft 0xD000000000000000000000000000000000000000
```
```text
dotnet run --read-file ..\transactions2.json
```
```text
dotnet run --nft 0xD000000000000000000000000000000000000000
```
```text
dotnet run --wallet 0x3000000000000000000000000000000000000000
```
```text
dotnet run --reset
```
```text
dotnet run --wallet 0x3000000000000000000000000000000000000000
```

## Future Improvements

### Finish base example
- --read-inline is not working

### Current Storage is not thread safe
- need to apply DI, to leverage repository pattern, and use a proper database

### Setup as a cli tool
https://www.youtube.com/watch?v=JNDgcBDZPkU

### Add DI to console app in order to apply a proper clean architecture with all proper indirections in order to imrpove testability as well
https://siderite.dev/blog/creating-console-app-with-dependency-injection-in-/
https://andrewlock.net/using-dependency-injection-in-a-net-core-console-application/

### Storage like blockchain (currently we are losing all historic transfers)
https://towardsdatascience.com/blockchain-explained-using-c-implementation-fb60f29b9f07