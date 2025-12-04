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
    public event Action<string, ToastLevel, int> OnShow = (message, level, durationMs) => { };
    public event Action OnHide = () => { };
    private System.Timers.Timer? Countdown;
    private readonly Queue<ToastMessage> _toastQueue = new();
    private bool _isDisplaying = false;

    /// <summary>
    /// Duration in milliseconds for how long each toast should be visible.
    /// Default is 3000ms (3 seconds).
    /// </summary>
    public int DurationMs { get; set; } = 3000;

    private class ToastMessage
    {
        public string Message { get; set; } = string.Empty;
        public ToastLevel Level { get; set; }
    }

    public void ShowToast(string message, ToastLevel level)
    {
        _toastQueue.Enqueue(new ToastMessage { Message = message, Level = level });

        if (!_isDisplaying)
        {
            ShowNextToast();
        }
    }

    private void ShowNextToast()
    {
        if (_toastQueue.Count == 0)
        {
            _isDisplaying = false;
            return;
        }

        _isDisplaying = true;
        var toast = _toastQueue.Dequeue();
        OnShow?.Invoke(toast.Message, toast.Level, DurationMs);
        StartCountdown();
    }

    private void StartCountdown()
    {
        if(Countdown != null)
        {
            Countdown.Stop();
            Countdown.Dispose();
        }

        Countdown = new System.Timers.Timer(DurationMs);
        Countdown.Elapsed += HideToast;
        Countdown.AutoReset = false;
        Countdown.Start();
    }

    private void HideToast(object? source, ElapsedEventArgs args)
    {
        OnHide?.Invoke();
        ShowNextToast();
    }

    public void Dispose()
    {
        Countdown?.Dispose();
    }
}
