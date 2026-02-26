using System.Windows;
using System.Windows.Threading;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Static service that displays persistent notification popups anchored to a corner of the active window.
/// Follows the Ant Design Notification specification.
/// </summary>
/// <remarks>
/// All methods are thread-safe and dispatch to the UI thread automatically.
/// Each unique <see cref="NotificationPlacement"/> value gets its own
/// <see cref="NotificationContainer"/> instance in the adorner layer.
/// </remarks>
public static class NotificationService
{
    // -------------------------------------------------------------------------
    // Configuration
    // -------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the default display duration in seconds applied when
    /// <see cref="NotificationConfig.Duration"/> is not explicitly set.
    /// Defaults to 4.5 seconds.
    /// </summary>
    public static double DefaultDuration { get; set; } = 4.5d;

    /// <summary>
    /// Gets or sets the default placement used when a <see cref="NotificationConfig"/>
    /// does not specify one. Defaults to <see cref="NotificationPlacement.TopRight"/>.
    /// </summary>
    public static NotificationPlacement DefaultPlacement { get; set; } = NotificationPlacement.TopRight;

    // -------------------------------------------------------------------------
    // Public API
    // -------------------------------------------------------------------------

    /// <summary>Shows a notification using the provided configuration.</summary>
    /// <param name="config">
    /// The <see cref="NotificationConfig"/> describing the notification content and options.
    /// </param>
    public static void Open(NotificationConfig config)
    {
        if (config is null) throw new ArgumentNullException(nameof(config));
        Post(config);
    }

    /// <summary>Shows a success notification.</summary>
    /// <param name="config">Notification configuration. The <see cref="NotificationConfig.Type"/> is overridden to <see cref="NotificationType.Success"/>.</param>
    public static void Success(NotificationConfig config)
    {
        if (config is null) throw new ArgumentNullException(nameof(config));
        config.Type = NotificationType.Success;
        Post(config);
    }

    /// <summary>Shows an error notification.</summary>
    /// <param name="config">Notification configuration. The <see cref="NotificationConfig.Type"/> is overridden to <see cref="NotificationType.Error"/>.</param>
    public static void Error(NotificationConfig config)
    {
        if (config is null) throw new ArgumentNullException(nameof(config));
        config.Type = NotificationType.Error;
        Post(config);
    }

    /// <summary>Shows a warning notification.</summary>
    /// <param name="config">Notification configuration. The <see cref="NotificationConfig.Type"/> is overridden to <see cref="NotificationType.Warning"/>.</param>
    public static void Warning(NotificationConfig config)
    {
        if (config is null) throw new ArgumentNullException(nameof(config));
        config.Type = NotificationType.Warning;
        Post(config);
    }

    /// <summary>Shows an informational notification.</summary>
    /// <param name="config">Notification configuration. The <see cref="NotificationConfig.Type"/> is overridden to <see cref="NotificationType.Info"/>.</param>
    public static void Info(NotificationConfig config)
    {
        if (config is null) throw new ArgumentNullException(nameof(config));
        config.Type = NotificationType.Info;
        Post(config);
    }

    // -------------------------------------------------------------------------
    // Convenience overloads
    // -------------------------------------------------------------------------

    /// <summary>Shows a success notification with a message and optional description.</summary>
    public static void Success(string message, string description = "", double? duration = null,
        NotificationPlacement? placement = null)
        => Post(BuildConfig(message, description, duration, placement, NotificationType.Success));

    /// <summary>Shows an error notification with a message and optional description.</summary>
    public static void Error(string message, string description = "", double? duration = null,
        NotificationPlacement? placement = null)
        => Post(BuildConfig(message, description, duration, placement, NotificationType.Error));

    /// <summary>Shows a warning notification with a message and optional description.</summary>
    public static void Warning(string message, string description = "", double? duration = null,
        NotificationPlacement? placement = null)
        => Post(BuildConfig(message, description, duration, placement, NotificationType.Warning));

    /// <summary>Shows an informational notification with a message and optional description.</summary>
    public static void Info(string message, string description = "", double? duration = null,
        NotificationPlacement? placement = null)
        => Post(BuildConfig(message, description, duration, placement, NotificationType.Info));

    /// <summary>Shows a notification with a message and optional description, using no specific type.</summary>
    public static void Open(string message, string description = "", double? duration = null,
        NotificationPlacement? placement = null)
        => Post(BuildConfig(message, description, duration, placement, NotificationType.None));

    // -------------------------------------------------------------------------
    // Private helpers
    // -------------------------------------------------------------------------

    private static NotificationConfig BuildConfig(
        string message,
        string description,
        double? duration,
        NotificationPlacement? placement,
        NotificationType type)
    {
        return new NotificationConfig
        {
            Message     = message ?? string.Empty,
            Description = description ?? string.Empty,
            Duration    = duration ?? DefaultDuration,
            Placement   = placement ?? DefaultPlacement,
            Type        = type
        };
    }

    private static void Post(NotificationConfig config)
    {
        var app = Application.Current;
        if (app is null) return;

        if (!app.Dispatcher.CheckAccess())
        {
            app.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                new Action(() => Post(config)));
            return;
        }

        var window = app.MainWindow;
        if (window is null || !window.IsLoaded) return;

        try
        {
            var container = NotificationContainer.EnsureContainer(window, config.Placement);
            container.Add(config);
        }
        catch (InvalidOperationException)
        {
            // Gracefully ignore if adorner layer is unavailable (e.g. during shutdown)
        }
    }
}
