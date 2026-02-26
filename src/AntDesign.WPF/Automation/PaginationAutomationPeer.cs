using System.Windows.Automation.Peers;
using AntDesign.WPF.Controls;

namespace AntDesign.WPF.Automation;

/// <summary>
/// Provides a <see cref="AutomationPeer"/> implementation for the <see cref="Pagination"/> control,
/// exposing it to UI Automation as a list element that describes the current page position.
/// </summary>
public class PaginationAutomationPeer : FrameworkElementAutomationPeer
{
    /// <summary>
    /// Initializes a new instance of <see cref="PaginationAutomationPeer"/> for the given <paramref name="owner"/>.
    /// </summary>
    /// <param name="owner">The <see cref="Pagination"/> control this peer represents.</param>
    public PaginationAutomationPeer(Pagination owner) : base(owner)
    {
    }

    // -------------------------------------------------------------------------
    // Core overrides
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    protected override string GetClassNameCore() => nameof(Pagination);

    /// <inheritdoc/>
    protected override AutomationControlType GetAutomationControlTypeCore()
        => AutomationControlType.List;

    /// <inheritdoc/>
    protected override string GetNameCore()
    {
        var pagination = (Pagination)Owner;
        int pageCount = pagination.PageCount;

        if (pageCount > 0)
            return $"Page {pagination.Current} of {pageCount}";

        return $"Page {pagination.Current}";
    }
}
