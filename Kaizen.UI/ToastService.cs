using System.Timers;

namespace Kaizen.UI;
public enum ToastLevel
{
    Info,
    Success,
    Warning,
    Error
}

public class ToastService : IDisposable
{
    public event Action<string, ToastLevel> OnShow = (message, level) => { };
    public event Action OnHide = () => { };
    private System.Timers.Timer? Countdown;

    public void ShowToast(string message, ToastLevel level)
    {
        OnShow?.Invoke(message, level);
        StartCountdown();
    }

    private void StartCountdown()
    {
        if(Countdown != null)
        {
            Countdown.Stop();
            Countdown.Dispose();
        }

        Countdown = new System.Timers.Timer(5000);
        Countdown.Elapsed += HideToast;
        Countdown.AutoReset = false;
        Countdown.Start();
    }

    private void HideToast(object? source, ElapsedEventArgs args)
    {
        OnHide?.Invoke();
    }

    public void Dispose()
    {
        Countdown?.Dispose();
    }
}