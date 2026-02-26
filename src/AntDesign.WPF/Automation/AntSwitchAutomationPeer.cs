using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using AntDesign.WPF.Controls;

namespace AntDesign.WPF.Automation;

/// <summary>
/// Provides a <see cref="AutomationPeer"/> implementation for the <see cref="AntSwitch"/> control,
/// exposing it to UI Automation as a toggle button that supports the Toggle pattern.
/// </summary>
public class AntSwitchAutomationPeer : FrameworkElementAutomationPeer, IToggleProvider
{
    /// <summary>
    /// Initializes a new instance of <see cref="AntSwitchAutomationPeer"/> for the given <paramref name="owner"/>.
    /// </summary>
    /// <param name="owner">The <see cref="AntSwitch"/> control this peer represents.</param>
    public AntSwitchAutomationPeer(AntSwitch owner) : base(owner)
    {
    }

    // -------------------------------------------------------------------------
    // Core overrides
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    protected override string GetClassNameCore() => nameof(AntSwitch);

    /// <inheritdoc/>
    protected override AutomationControlType GetAutomationControlTypeCore()
        => AutomationControlType.Button;

    /// <inheritdoc/>
    protected override string GetNameCore()
    {
        var antSwitch = (AntSwitch)Owner;

        if (antSwitch.IsChecked)
        {
            if (antSwitch.CheckedContent is string checkedText && !string.IsNullOrEmpty(checkedText))
                return checkedText;
            return "On";
        }
        else
        {
            if (antSwitch.UncheckedContent is string uncheckedText && !string.IsNullOrEmpty(uncheckedText))
                return uncheckedText;
            return "Off";
        }
    }

    /// <inheritdoc/>
    public override object? GetPattern(PatternInterface patternInterface)
    {
        if (patternInterface == PatternInterface.Toggle)
            return this;

        return base.GetPattern(patternInterface);
    }

    // -------------------------------------------------------------------------
    // IToggleProvider
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public ToggleState ToggleState
    {
        get
        {
            var antSwitch = (AntSwitch)Owner;
            return antSwitch.IsChecked ? ToggleState.On : ToggleState.Off;
        }
    }

    /// <inheritdoc/>
    public void Toggle()
    {
        var antSwitch = (AntSwitch)Owner;
        if (antSwitch.IsEnabled && !antSwitch.Loading)
            antSwitch.Toggle();
    }
}
