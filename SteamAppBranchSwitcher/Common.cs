using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System.Threading.Tasks;
using Steamworks;
using System;

namespace SteamAppBranchSwitcher;

public class Common
{
    public static (bool,string) SteamApiInit()
    {
        try
        {
            if (!SteamAPI.Init())
            {
                return (false, "SteamAPI.Init() failed!");
            }
        }
        catch (DllNotFoundException e)
        { // We check this here as it will be the first instance of it.
            return (false, e.Message);
        }
        if (!Packsize.Test())
        {
            return (false, "You're using the wrong Steamworks.NET Assembly for this platform!");
        }

        if (!DllCheck.Test())
        {
            return (false, "You're using the wrong dlls for this platform!");
        }
        return (true, "Ok.");
    }

    public static async Task AlertBoxAsync(string text, string title = "Alert", ButtonEnum enu = ButtonEnum.Ok)
    {
        var box = MessageBoxManager
            .GetMessageBoxStandard(title, text, enu);

        var result = await box.ShowAsync();
    }
}