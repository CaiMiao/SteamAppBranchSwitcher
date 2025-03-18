using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Steamworks;

namespace SteamAppBranchSwitcher.Views;

public partial class MainView : UserControl
{
    private static List<string> lBetaNames = [];
    private static List<string> lBetaNamesDescs = [];

    public MainView()
    {
        if (App.IsSteamApiInitialized)
        {
            int nGotBetas = SteamApps.GetNumBetas(out _, out _);
            for (int i = 0; i < nGotBetas; ++i)
            {
                if (SteamApps.GetBetaInfo(i, out _, out _, out string name, 128, out string desc, 1024))
                {
                    lBetaNames.Add(name);
                    if (desc != string.Empty)
                        lBetaNamesDescs.Add($"{name} - {desc}");
                    else
                        lBetaNamesDescs.Add(name);
                }
            }
        }

        // in normal circumstances we have public at least, but here we indicate nothing when nothing
        if (lBetaNames.Count == 0)
        {
            var listNothing = new List<string> { "nothing la" };
            lBetaNames = listNothing;
            lBetaNamesDescs = listNothing;
        }

        InitializeComponent();

        listbox.ItemsSource = lBetaNamesDescs.ToArray();
    }

    public void SwitchBranch(object sender, RoutedEventArgs args)
    {
        try
        {
            if (App.IsSteamApiInitialized && SteamApps.SetActiveBeta(lBetaNames[listbox.SelectedIndex]))
            {
                if (!SteamApps.GetCurrentBetaName(out string s, 128)) s = "(null)";
                switch_message.Text = $"Switch branch OK.\nCurrent branch: {s}";
            }
        }
        catch (Exception e)
        {
            _ = Common.AlertBoxAsync(e.Message);
        }
    }
}
