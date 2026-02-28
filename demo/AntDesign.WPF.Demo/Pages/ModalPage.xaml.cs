using System.Windows;
using System.Windows.Controls;
using AntDesign.WPF.Demo.Helpers;

namespace AntDesign.WPF.Demo.Pages;

public partial class ModalPage : UserControl
{
    public ModalPage()
    {
        InitializeComponent();
    }

    private void OpenModal_Click(object sender, RoutedEventArgs e)
    {
        DemoModal.Title = LanguageHelper.GetString("Modal.BasicModal");
        DemoModal.DialogContent = LanguageHelper.GetString("Modal.BasicContent");
        DemoModal.OkText = LanguageHelper.GetString("Modal.OK");
        DemoModal.CancelText = LanguageHelper.GetString("Modal.Cancel");
        DemoModal.DialogWidth = 520;
        DemoModal.IsOpen = true;
    }

    private void OpenConfirmModal_Click(object sender, RoutedEventArgs e)
    {
        DemoModal.Title = LanguageHelper.GetString("Modal.ConfirmTitle");
        DemoModal.DialogContent = LanguageHelper.GetString("Modal.ConfirmContent");
        DemoModal.OkText = LanguageHelper.GetString("Modal.YesConfirm");
        DemoModal.CancelText = LanguageHelper.GetString("Modal.NoCancel");
        DemoModal.DialogWidth = 520;
        DemoModal.IsOpen = true;
    }

    private void OpenWideModal_Click(object sender, RoutedEventArgs e)
    {
        DemoModal.Title = LanguageHelper.GetString("Modal.WideTitle");
        DemoModal.DialogContent = LanguageHelper.GetString("Modal.WideContent");
        DemoModal.OkText = LanguageHelper.GetString("Modal.OK");
        DemoModal.CancelText = LanguageHelper.GetString("Modal.Cancel");
        DemoModal.DialogWidth = 720;
        DemoModal.IsOpen = true;
    }
}
