using System.Collections.Generic;
using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Steamworks;
using System.Threading.Tasks;
using Avalonia;

namespace SteamAppBranchSwitcher.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        List<string> lBetaNames = [];
        if (App.IsSteamApiInitialized)
        {
            int nGotBetas = SteamApps.GetNumBetas(out _, out _);

            for (int i = 0; i < nGotBetas; ++i)
            {
                string name;
                string desc;
                if (SteamApps.GetBetaInfo(i, out _, out _, out name, 128, out desc, 1024))
                {
                    lBetaNames.Add(name);
                }
            }
        }

        //at very least we have public
        if (lBetaNames == new List<string>())
            lBetaNames.Add("nothing la");

        InitializeComponent();

        listbox.ItemsSource = lBetaNames.ToArray();
    }

    public void SwitchBranch(object sender, RoutedEventArgs args)
    {
        if (App.IsSteamApiInitialized && SteamApps.SetActiveBeta((string)listbox.SelectedItem))
        {
            if (!SteamApps.GetCurrentBetaName(out string s, 128)) s = "(null)";
            switch_message.Text = $"Switch branch OK.\nCurrent branch: {s}";
        }
    }
}
