# Build

Native AOT is recommended.

## win-x64

`dotnet publish -r win-x64 -c Release -p:PublishAot=true`

## osx-x64

`dotnet publish -r osx-x64 -c Release -p:PublishAot=true`

## osx-arm64

`dotnet publish -r osx-arm64 -c Release -p:PublishAot=true`
