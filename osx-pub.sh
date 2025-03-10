#!/bin/bash
APP_NAME="./SABS.app"
PUBLISH_OUTPUT_DIRECTORY="SteamAppBranchSwitcher.Desktop/bin/Release/net8.0/osx-x64/publish/."
# PUBLISH_OUTPUT_DIRECTORY should point to the output directory of your dotnet publish command.
# One example is /path/to/your/csproj/bin/Release/netcoreapp3.1/osx-x64/publish/.
# If you want to change output directories, add `--output /my/directory/path` to your `dotnet publish` command.
INFO_PLIST="Info.plist"
ICON_FILE="logo.icns"
ADDITIONAL_DYLIB_1="Steamworks.NET/OSX-Linux-x64/steam_api.bundle/Contents/MacOS/libsteam_api.dylib"

if [ -d "$APP_NAME" ]
then
    rm -rf "$APP_NAME"
fi

mkdir "$APP_NAME"

mkdir "$APP_NAME/Contents"
mkdir "$APP_NAME/Contents/MacOS"
mkdir "$APP_NAME/Contents/Resources"
# mkdir "$APP_NAME/Contents/PlugIns"

cp "$INFO_PLIST" "$APP_NAME/Contents/Info.plist"
cp "$ICON_FILE" "$APP_NAME/Contents/Resources/$ICON_FILE"
cp "$ADDITIONAL_DYLIB_1" "$APP_NAME/Contents/MacOS/."
cp -a "$PUBLISH_OUTPUT_DIRECTORY" "$APP_NAME/Contents/MacOS"