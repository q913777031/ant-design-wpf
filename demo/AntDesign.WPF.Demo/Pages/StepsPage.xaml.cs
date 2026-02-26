using System.Windows;
using System.Windows.Controls;
using AntDesign.WPF.Controls;

namespace AntDesign.WPF.Demo.Pages;

public partial class StepsPage : UserControl
{
    private int _currentStep = 1;
    private const int TotalSteps = 3;

    public StepsPage()
    {
        InitializeComponent();
    }

    private void PreviousStep_Click(object sender, RoutedEventArgs e)
    {
        if (_currentStep > 0)
        {
            _currentStep--;
            UpdateSteps();
        }
    }

    private void NextStep_Click(object sender, RoutedEventArgs e)
    {
        if (_currentStep < TotalSteps - 1)
        {
            _currentStep++;
            UpdateSteps();
        }
    }

    private void UpdateSteps()
    {
        // Find the Steps control in the visual tree
        if (FindStepsControl() is Steps steps)
        {
            steps.Current = _currentStep;
        }
    }

    private Steps? FindStepsControl()
    {
        // Walk the visual tree to find the first Steps control
        return FindChildOfType<Steps>(this);
    }

    private static T? FindChildOfType<T>(System.Windows.DependencyObject parent) where T : System.Windows.DependencyObject
    {
        int count = System.Windows.Media.VisualTreeHelper.GetChildrenCount(parent);
        for (int i = 0; i < count; i++)
        {
            var child = System.Windows.Media.VisualTreeHelper.GetChild(parent, i);
            if (child is T typedChild)
                return typedChild;
            var found = FindChildOfType<T>(child);
            if (found != null) return found;
        }
        return null;
    }
}
