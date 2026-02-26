using System.Windows.Automation.Peers;
using AntDesign.WPF.Controls;

namespace AntDesign.WPF.Automation;

/// <summary>
/// Provides a <see cref="AutomationPeer"/> implementation for the <see cref="Alert"/> control,
/// exposing it to UI Automation as a status bar element that conveys feedback messages.
/// </summary>
public class AlertAutomationPeer : FrameworkElementAutomationPeer
{
    /// <summary>
    /// Initializes a new instance of <see cref="AlertAutomationPeer"/> for the given <paramref name="owner"/>.
    /// </summary>
    /// <param name="owner">The <see cref="Alert"/> control this peer represents.</param>
    public AlertAutomationPeer(Alert owner) : base(owner)
    {
    }

    // -------------------------------------------------------------------------
    // Core overrides
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    protected override string GetClassNameCore() => nameof(Alert);

    /// <inheritdoc/>
    protected override AutomationControlType GetAutomationControlTypeCore()
        => AutomationControlType.StatusBar;

    /// <inheritdoc/>
    protected override string GetNameCore()
    {
        var alert = (Alert)Owner;
        return !string.IsNullOrEmpty(alert.Message) ? alert.Message : nameof(Alert);
    }
}
