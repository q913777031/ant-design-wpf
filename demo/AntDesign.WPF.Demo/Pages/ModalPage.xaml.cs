using System.Windows;
using System.Windows.Controls;

namespace AntDesign.WPF.Demo.Pages;

public partial class ModalPage : UserControl
{
    public ModalPage()
    {
        InitializeComponent();
    }

    private void OpenModal_Click(object sender, RoutedEventArgs e)
    {
        DemoModal.Title = "Basic Modal";
        DemoModal.DialogContent = "Some contents in the modal dialog.\nThis is a demonstration of the Modal control.";
        DemoModal.DialogWidth = 520;
        DemoModal.IsOpen = true;
    }

    private void OpenConfirmModal_Click(object sender, RoutedEventArgs e)
    {
        DemoModal.Title = "Confirm Action";
        DemoModal.DialogContent = "Are you sure you want to perform this action?\nThis operation cannot be undone.";
        DemoModal.OkText = "Yes, Confirm";
        DemoModal.CancelText = "No, Cancel";
        DemoModal.DialogWidth = 520;
        DemoModal.IsOpen = true;
    }

    private void OpenWideModal_Click(object sender, RoutedEventArgs e)
    {
        DemoModal.Title = "Wide Modal";
        DemoModal.DialogContent = "This modal dialog is 720 pixels wide, useful for displaying more content.";
        DemoModal.OkText = "OK";
        DemoModal.CancelText = "Cancel";
        DemoModal.DialogWidth = 720;
        DemoModal.IsOpen = true;
    }
}
