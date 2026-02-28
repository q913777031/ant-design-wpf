using System.Windows;
using System.Windows.Controls;
using AntDesign.WPF.Controls;
using AntDesign.WPF.Demo.Helpers;

namespace AntDesign.WPF.Demo.Pages;

public partial class MessagePage : UserControl
{
    public MessagePage()
    {
        InitializeComponent();
    }

    private void MessageSuccess_Click(object sender, RoutedEventArgs e)
    {
        MessageService.Success(LanguageHelper.GetString("Message.SuccessText"));
    }

    private void MessageInfo_Click(object sender, RoutedEventArgs e)
    {
        MessageService.Info(LanguageHelper.GetString("Message.InfoText"));
    }

    private void MessageWarning_Click(object sender, RoutedEventArgs e)
    {
        MessageService.Warning(LanguageHelper.GetString("Message.WarningText"));
    }

    private void MessageError_Click(object sender, RoutedEventArgs e)
    {
        MessageService.Error(LanguageHelper.GetString("Message.ErrorText"));
    }

    private void MessageLoading_Click(object sender, RoutedEventArgs e)
    {
        MessageService.Loading(LanguageHelper.GetString("Message.LoadingText"), duration: 2);
    }

    private void NotifySuccess_Click(object sender, RoutedEventArgs e)
    {
        NotificationService.Success(
            LanguageHelper.GetString("Message.NotifySuccessTitle"),
            LanguageHelper.GetString("Message.NotifySuccessDesc"));
    }

    private void NotifyInfo_Click(object sender, RoutedEventArgs e)
    {
        NotificationService.Info(
            LanguageHelper.GetString("Message.NotifyInfoTitle"),
            LanguageHelper.GetString("Message.NotifyInfoDesc"));
    }

    private void NotifyWarning_Click(object sender, RoutedEventArgs e)
    {
        NotificationService.Warning(
            LanguageHelper.GetString("Message.NotifyWarningTitle"),
            LanguageHelper.GetString("Message.NotifyWarningDesc"));
    }

    private void NotifyError_Click(object sender, RoutedEventArgs e)
    {
        NotificationService.Error(
            LanguageHelper.GetString("Message.NotifyErrorTitle"),
            LanguageHelper.GetString("Message.NotifyErrorDesc"));
    }

    private void MessageShort_Click(object sender, RoutedEventArgs e)
    {
        MessageService.Info(LanguageHelper.GetString("Message.ShortText"), duration: 1);
    }

    private void MessageLong_Click(object sender, RoutedEventArgs e)
    {
        MessageService.Info(LanguageHelper.GetString("Message.LongText"), duration: 10);
    }
}
