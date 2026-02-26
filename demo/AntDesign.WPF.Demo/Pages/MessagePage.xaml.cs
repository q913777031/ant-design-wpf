using System.Windows;
using System.Windows.Controls;
using AntDesign.WPF.Controls;

namespace AntDesign.WPF.Demo.Pages;

public partial class MessagePage : UserControl
{
    public MessagePage()
    {
        InitializeComponent();
    }

    private void MessageSuccess_Click(object sender, RoutedEventArgs e)
    {
        MessageService.Success("Operation completed successfully!");
    }

    private void MessageInfo_Click(object sender, RoutedEventArgs e)
    {
        MessageService.Info("This is an informational message.");
    }

    private void MessageWarning_Click(object sender, RoutedEventArgs e)
    {
        MessageService.Warning("Please review this warning message.");
    }

    private void MessageError_Click(object sender, RoutedEventArgs e)
    {
        MessageService.Error("An error has occurred. Please try again.");
    }

    private void MessageLoading_Click(object sender, RoutedEventArgs e)
    {
        MessageService.Loading("Loading data, please wait...", duration: 2);
    }

    private void NotifySuccess_Click(object sender, RoutedEventArgs e)
    {
        NotificationService.Success(
            "Task Completed",
            "Your export has finished. The file is ready to download.");
    }

    private void NotifyInfo_Click(object sender, RoutedEventArgs e)
    {
        NotificationService.Info(
            "System Update Available",
            "A new version (v2.0.0) is available. Click here to update.");
    }

    private void NotifyWarning_Click(object sender, RoutedEventArgs e)
    {
        NotificationService.Warning(
            "Low Disk Space",
            "You have less than 500 MB of free disk space remaining.");
    }

    private void NotifyError_Click(object sender, RoutedEventArgs e)
    {
        NotificationService.Error(
            "Connection Failed",
            "Unable to connect to the server. Please check your network connection.");
    }

    private void MessageShort_Click(object sender, RoutedEventArgs e)
    {
        MessageService.Info("This message disappears after 1 second.", duration: 1);
    }

    private void MessageLong_Click(object sender, RoutedEventArgs e)
    {
        MessageService.Info("This message stays for 10 seconds. Plenty of time to read it!", duration: 10);
    }
}
