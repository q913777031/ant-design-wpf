using System.Windows;
using System.Windows.Threading;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Static service that displays transient floating toast messages at the top of the active window.
/// Follows the Ant Design Message specification.
/// </summary>
/// <remarks>
/// All methods are thread-safe and dispatch to the UI thread automatically.
/// Internally the service resolves (or creates) a <see cref="MessageContainer"/> in the
/// adorner layer of <see cref="Application.MainWindow"/>.
/// </remarks>
public static class MessageService
{
    // -------------------------------------------------------------------------
    // Configuration
    // -------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the default display duration in seconds applied to every new message.
    /// Individual calls can override this via the optional <c>duration</c> parameter.
    /// Defaults to 3 seconds.
    /// </summary>
    public static double DefaultDuration { get; set; } = 3d;

    // -------------------------------------------------------------------------
    // Public API
    // -------------------------------------------------------------------------

    /// <summary>Shows a success toast message.</summary>
    /// <param name="content">The message text.</param>
    /// <param name="duration">
    /// Optional display duration in seconds. Uses <see cref="DefaultDuration"/> when not specified.
    /// </param>
    public static void Success(string content, double? duration = null)
        => Post(content, MessageType.Success, duration);

    /// <summary>Shows an error toast message.</summary>
    /// <param name="content">The message text.</param>
    /// <param name="duration">Optional display duration in seconds.</param>
    public static void Error(string content, double? duration = null)
        => Post(content, MessageType.Error, duration);

    /// <summary>Shows a warning toast message.</summary>
    /// <param name="content">The message text.</param>
    /// <param name="duration">Optional display duration in seconds.</param>
    public static void Warning(string content, double? duration = null)
        => Post(content, MessageType.Warning, duration);

    /// <summary>Shows an informational toast message.</summary>
    /// <param name="content">The message text.</param>
    /// <param name="duration">Optional display duration in seconds.</param>
    public static void Info(string content, double? duration = null)
        => Post(content, MessageType.Info, duration);

    /// <summary>Shows a loading toast message.</summary>
    /// <param name="content">The message text.</param>
    /// <param name="duration">Optional display duration in seconds.</param>
    public static void Loading(string content, double? duration = null)
        => Post(content, MessageType.Loading, duration);

    // -------------------------------------------------------------------------
    // Private helpers
    // -------------------------------------------------------------------------

    /// <summary>
    /// Dispatches the creation and registration of a <see cref="MessageItem"/> to
    /// the UI thread, then hands it to the resolved <see cref="MessageContainer"/>.
    /// </summary>
    private static void Post(string content, MessageType type, double? duration)
    {
        var app = Application.Current;
        if (app is null) return;

        // Ensure we run on the UI dispatcher
        if (!app.Dispatcher.CheckAccess())
        {
            app.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                new Action(() => Post(content, type, duration)));
            return;
        }

        var window = app.MainWindow;
        if (window is null || !window.IsLoaded) return;

        try
        {
            var container = MessageContainer.EnsureContainer(window);
            var item = new MessageItem
            {
                Content  = content ?? string.Empty,
                Type     = type,
                Duration = duration ?? DefaultDuration
            };
            container.Add(item);
        }
        catch (InvalidOperationException)
        {
            // Gracefully ignore if adorner layer is unavailable (e.g. during shutdown)
        }
    }
}
