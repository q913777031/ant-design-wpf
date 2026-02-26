using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using AntDesign.WPF.Controls;

namespace AntDesign.WPF.Automation;

/// <summary>
/// Provides a <see cref="AutomationPeer"/> implementation for the <see cref="Rate"/> control,
/// exposing it to UI Automation as a slider that supports the RangeValue pattern.
/// </summary>
public class RateAutomationPeer : FrameworkElementAutomationPeer, IRangeValueProvider
{
    /// <summary>
    /// Initializes a new instance of <see cref="RateAutomationPeer"/> for the given <paramref name="owner"/>.
    /// </summary>
    /// <param name="owner">The <see cref="Rate"/> control this peer represents.</param>
    public RateAutomationPeer(Rate owner) : base(owner)
    {
    }

    // -------------------------------------------------------------------------
    // Core overrides
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    protected override string GetClassNameCore() => nameof(Rate);

    /// <inheritdoc/>
    protected override AutomationControlType GetAutomationControlTypeCore()
        => AutomationControlType.Slider;

    /// <inheritdoc/>
    protected override string GetNameCore()
    {
        var rate = (Rate)Owner;
        return $"Rating: {rate.Value} of {rate.Count}";
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
    public double Value => ((Rate)Owner).Value;

    /// <inheritdoc/>
    public double Minimum => 0d;

    /// <inheritdoc/>
    public double Maximum => ((Rate)Owner).Count;

    /// <inheritdoc/>
    public double SmallChange => ((Rate)Owner).AllowHalf ? 0.5d : 1d;

    /// <inheritdoc/>
    public double LargeChange => 1d;

    /// <inheritdoc/>
    public bool IsReadOnly => ((Rate)Owner).Disabled;

    /// <inheritdoc/>
    public void SetValue(double value)
    {
        var rate = (Rate)Owner;
        if (!rate.Disabled)
            rate.Value = value;
    }
}
