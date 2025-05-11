using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace EmailClient;

public partial class EditMessage : Window
{ 
    public EditMessage(ViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
    
    private void OKButton_Click(object? sender, RoutedEventArgs e)
    {
        Close(true);
    }

    private void RemoveAttachment_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Attachment attachment)
        {
            // access the ViewModel through the DataContext
            var vm = DataContext as ViewModel;

            if (!vm.IsEditable)
            {
                return;
            }

            if (vm != null && vm.SelectedMessage != null)
            {
                // check if the attachment exists and remove it
                if (vm.SelectedMessage.Attachments.Contains(attachment))
                {
                    vm.SelectedMessage.Attachments.Remove(attachment);
                }
            }
        }
    }

    private async void AddAttachment_Click(object? sender, RoutedEventArgs e)
    {
        
        var vm = DataContext as ViewModel;
        
        // create and configure the OpenFileDialog
        var dlg = new OpenFileDialog
        {
            AllowMultiple = true,  // allow multiple files to be selected
            Title = "Select files to attach"
        };

        // show the dialog and await the result
        var result = await dlg.ShowAsync(this);

        if (result != null)
        {
            foreach (var filePath in result)
            {
                // add the file as an Attachment object to the collection
                vm.SelectedMessage.Attachments.Add(new Attachment(filePath));
            }
        }
    }
    
}