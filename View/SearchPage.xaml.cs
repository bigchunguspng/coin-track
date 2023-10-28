using System.Windows.Controls;
using System.Windows.Input;

namespace CoinTrack.View;

public partial class SearchPage : Page
{
    public SearchPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Is used to fix switching tabs when the searchbar is focused
    /// </summary>
    private void SearchBar_OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyboardDevice.Modifiers == ModifierKeys.Alt)
        {
            Scroll.Focus();
        }
    }
}