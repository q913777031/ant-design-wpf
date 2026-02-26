using System.Windows.Automation.Peers;
using AntDesign.WPF.Controls;

namespace AntDesign.WPF.Automation;

/// <summary>
/// Provides a <see cref="AutomationPeer"/> implementation for the <see cref="Modal"/> control,
/// exposing it to UI Automation as a dialog window.
/// </summary>
public class ModalAutomationPeer : FrameworkElementAutomationPeer
{
    /// <summary>
    /// Initializes a new instance of <see cref="ModalAutomationPeer"/> for the given <paramref name="owner"/>.
    /// </summary>
    /// <param name="owner">The <see cref="Modal"/> control this peer represents.</param>
    public ModalAutomationPeer(Modal owner) : base(owner)
    {
    }

    // -------------------------------------------------------------------------
    // Core overrides
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    protected override string GetClassNameCore() => nameof(Modal);

    /// <inheritdoc/>
    protected override AutomationControlType GetAutomationControlTypeCore()
        => AutomationControlType.Window;

    /// <inheritdoc/>
    protected override string GetNameCore()
    {
        var modal = (Modal)Owner;
        return !string.IsNullOrEmpty(modal.Title) ? modal.Title : nameof(Modal);
    }
}
