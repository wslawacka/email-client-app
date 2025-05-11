using Avalonia.Controls;
using Avalonia.Interactivity;

namespace EmailClient;

public partial class EditMessage : Window
{

    public EditMessage()
    {
        InitializeComponent();
    }
    public EditMessage(ViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
    
}