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
    
}