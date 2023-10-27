using System.Diagnostics;

namespace CoinTrack.Services;

public class HyperlinkService
{
    public void OpenLink(string url)
    {
        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
    }
}