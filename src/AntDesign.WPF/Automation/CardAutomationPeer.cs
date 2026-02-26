using System.Windows.Automation.Peers;
using AntDesign.WPF.Controls;

namespace AntDesign.WPF.Automation;

/// <summary>
/// Provides a <see cref="AutomationPeer"/> implementation for the <see cref="Card"/> control,
/// exposing it to UI Automation as a logical group container.
/// </summary>
public class CardAutomationPeer : FrameworkElementAutomationPeer
{
    /// <summary>
    /// Initializes a new instance of <see cref="CardAutomationPeer"/> for the given <paramref name="owner"/>.
    /// </summary>
    /// <param name="owner">The <see cref="Card"/> control this peer represents.</param>
    public CardAutomationPeer(Card owner) : base(owner)
    {
    }

    // -------------------------------------------------------------------------
    // Core overrides
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    protected override string GetClassNameCore() => nameof(Card);

    /// <inheritdoc/>
    protected override AutomationControlType GetAutomationControlTypeCore()
        => AutomationControlType.Group;

    /// <inheritdoc/>
    protected override string GetNameCore()
    {
        var card = (Card)Owner;
        return !string.IsNullOrEmpty(card.Title) ? card.Title : nameof(Card);
    }
}
