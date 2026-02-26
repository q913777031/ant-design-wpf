using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using AntDesign.WPF.Controls;

namespace AntDesign.WPF.Automation;

/// <summary>
/// Provides a <see cref="AutomationPeer"/> implementation for the <see cref="Progress"/> control,
/// exposing it to UI Automation as a progress bar that supports the RangeValue pattern.
/// </summary>
public class ProgressAutomationPeer : FrameworkElementAutomationPeer, IRangeValueProvider
{
    /// <summary>
    /// Initializes a new instance of <see cref="ProgressAutomationPeer"/> for the given <paramref name="owner"/>.
    /// </summary>
    /// <param name="owner">The <see cref="Progress"/> control this peer represents.</param>
    public ProgressAutomationPeer(Progress owner) : base(owner)
    {
    }

    // -------------------------------------------------------------------------
    // Core overrides
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    protected override string GetClassNameCore() => nameof(Progress);

    /// <inheritdoc/>
    protected override AutomationControlType GetAutomationControlTypeCore()
        => AutomationControlType.ProgressBar;

    /// <inheritdoc/>
    protected override string GetNameCore()
    {
        var progress = (Progress)Owner;
        return $"{progress.Percent:0}%";
    }

    /// <inheritdoc/>
    public override object? GetPattern(PatternInterface patternInterface)
    {
        if (patternInterface == PatternInterface.RangeValue)
            return this;

        return base.GetPattern(patternInterface);
    }

    // -------------------------------------------------------------------------
    // IRangeValueProvider
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public double Value => ((Progress)Owner).Percent;

    /// <inheritdoc/>
    public double Minimum => 0d;

    /// <inheritdoc/>
    public double Maximum => 100d;

    /// <inheritdoc/>
    public double SmallChange => 1d;

    /// <inheritdoc/>
    public double LargeChange => 10d;

    /// <inheritdoc/>
    public bool IsReadOnly => true;

    /// <inheritdoc/>
    public void SetValue(double value)
    {
        // Progress is a read-only display; setting value is not supported.
    }
}
