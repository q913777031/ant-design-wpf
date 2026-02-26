using System.Windows.Automation.Peers;
using AntDesign.WPF.Controls;

namespace AntDesign.WPF.Automation;

/// <summary>
/// Provides a <see cref="AutomationPeer"/> implementation for the <see cref="Drawer"/> control,
/// exposing it to UI Automation as a pane element.
/// </summary>
public class DrawerAutomationPeer : FrameworkElementAutomationPeer
{
    /// <summary>
    /// Initializes a new instance of <see cref="DrawerAutomationPeer"/> for the given <paramref name="owner"/>.
    /// </summary>
    /// <param name="owner">The <see cref="Drawer"/> control this peer represents.</param>
    public DrawerAutomationPeer(Drawer owner) : base(owner)
    {
    }

    // -------------------------------------------------------------------------
    // Core overrides
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    protected override string GetClassNameCore() => nameof(Drawer);

    /// <inheritdoc/>
    protected override AutomationControlType GetAutomationControlTypeCore()
        => AutomationControlType.Pane;

    /// <inheritdoc/>
    protected override string GetNameCore()
    {
        var drawer = (Drawer)Owner;
        return !string.IsNullOrEmpty(drawer.Title) ? drawer.Title : nameof(Drawer);
    }
}
