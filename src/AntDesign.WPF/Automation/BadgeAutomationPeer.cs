using System.Windows.Automation.Peers;
using AntDesign.WPF.Controls;

namespace AntDesign.WPF.Automation;

/// <summary>
/// Provides a <see cref="AutomationPeer"/> implementation for the <see cref="Badge"/> control,
/// exposing it to UI Automation as a text element carrying the notification count.
/// </summary>
public class BadgeAutomationPeer : FrameworkElementAutomationPeer
{
    /// <summary>
    /// Initializes a new instance of <see cref="BadgeAutomationPeer"/> for the given <paramref name="owner"/>.
    /// </summary>
    /// <param name="owner">The <see cref="Badge"/> control this peer represents.</param>
    public BadgeAutomationPeer(Badge owner) : base(owner)
    {
    }

    // -------------------------------------------------------------------------
    // Core overrides
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    protected override string GetClassNameCore() => nameof(Badge);

    /// <inheritdoc/>
    protected override AutomationControlType GetAutomationControlTypeCore()
        => AutomationControlType.Text;

    /// <inheritdoc/>
    protected override string GetNameCore()
    {
        var badge = (Badge)Owner;

        if (badge.Dot)
            return "Badge (dot)";

        if (badge.IsBadgeVisible && !string.IsNullOrEmpty(badge.DisplayCount))
            return $"Badge: {badge.DisplayCount}";

        return nameof(Badge);
    }
}
