using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using MsBox.Avalonia;

namespace EmailClient;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = new ViewModel();
    }

    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private async void MessageListBox_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (sender is ListBox tappedListBox 
            && tappedListBox.SelectedItem is ListBoxItem tappedListBoxItem 
            && tappedListBoxItem.Content is string messageSubject)
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Message Subject", $"You clicked on the message: {messageSubject}");

            await box.ShowAsync();
        }
    }
    
    
}