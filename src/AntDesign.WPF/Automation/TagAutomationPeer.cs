using System.Windows.Automation.Peers;
using AntDesign.WPF.Controls;

namespace AntDesign.WPF.Automation;

/// <summary>
/// Provides a <see cref="AutomationPeer"/> implementation for the <see cref="Tag"/> control,
/// exposing it to UI Automation as a text element.
/// </summary>
public class TagAutomationPeer : FrameworkElementAutomationPeer
{
    /// <summary>
    /// Initializes a new instance of <see cref="TagAutomationPeer"/> for the given <paramref name="owner"/>.
    /// </summary>
    /// <param name="owner">The <see cref="Tag"/> control this peer represents.</param>
    public TagAutomationPeer(Tag owner) : base(owner)
    {
    }

    // -------------------------------------------------------------------------
    // Core overrides
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    protected override string GetClassNameCore() => nameof(Tag);

    /// <inheritdoc/>
    protected override AutomationControlType GetAutomationControlTypeCore()
        => AutomationControlType.Text;

    /// <inheritdoc/>
    protected override string GetNameCore()
    {
        var tag = (Tag)Owner;
        if (tag.Content is string text && !string.IsNullOrEmpty(text))
            return text;

        return tag.Content?.ToString() ?? nameof(Tag);
    }
}
