using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;

namespace EmailClient;

public partial class AddMessage : Window
{
    
    public Email NewMessage { get; private set; }
    public MessageAction UserAction { get; private set; } = MessageAction.None;
    
    public AddMessage() 
    {
        InitializeComponent();
        NewMessage = new Email();
        DataContext = this;
    }

    private async void SendButton_Click(object sender, RoutedEventArgs e)
    {
        var result = await ValidateInputs(false);
        if (!result) return;
        UserAction = MessageAction.Send;
        Close(true);
    }

    private async void SaveDraftButton_Click(object sender, RoutedEventArgs e)
    {
        var result = await ValidateInputs(true);
        if (!result) return;
        UserAction = MessageAction.SaveDraft;
        Close(true);
    }

    private async Task<bool> ValidateInputs(bool allowEmptyRecipients = false)
    {
        var sender = SenderTextBox.Text?.Trim();
        var recipientsRaw = RecipientsTextBox.Text?.Trim();
        var subject = SubjectTextBox.Text?.Trim();
        var content = ContentTextBox.Text?.Trim();
        var comboBoxItem = ImportanceComboBox.SelectedItem as ComboBoxItem;
        var text = comboBoxItem?.Content?.ToString();
        var importance = Enum.TryParse<Email.ImportanceLevel>(text, out var parsedImportance);

        if (string.IsNullOrEmpty(sender))
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Invalid input", $"Sender is required!");
            await box.ShowAsync();
            return false;
        }

        if (!Regex.IsMatch(sender, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Invalid input", $"Invalid email address - sender! Valid format: username@domain.extension");
            await box.ShowAsync();
            return false;
        }
        
        if (!allowEmptyRecipients && string.IsNullOrEmpty(recipientsRaw))
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Invalid input", $"Recipient is required!");
            await box.ShowAsync();
            return false;
        }
        
        var recipientsList = recipientsRaw?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (!allowEmptyRecipients)
        {
            foreach (var recipient in recipientsList!)
            {
                if (!Regex.IsMatch(recipient, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    var box = MessageBoxManager
                        .GetMessageBoxStandard("Invalid input", $"Invalid email address - recipient! Valid format: username@domain.extension");
                    await box.ShowAsync();
                    return false;
                }
            }
        }

        if (string.IsNullOrEmpty(subject))
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Invalid input", $"Subject is required!");
            await box.ShowAsync();
            return false;
        }
        
        if (subject.Length > 100)
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Invalid input", $"Subject is too long! Please limit it to 100 characters.");
            await box.ShowAsync();
            return false;
        }

        
        if (string.IsNullOrEmpty(content))
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Invalid input", $"Content is required!");
            await box.ShowAsync();
            return false;
        }
        
        if (content.Length > 800) // You can set a sensible length limit for the content
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Invalid input", $"Content is too long! Please limit it to 800 characters.");
            await box.ShowAsync();
            return false;
        }

        NewMessage.Sender = sender;
        NewMessage.RecipientsString = recipientsRaw!;
        NewMessage.Subject = subject;
        NewMessage.Content = content;
        NewMessage.Importance = parsedImportance;
        return true;
    }
    
    private async void AddAttachment_Click(object sender, RoutedEventArgs e)
    {
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
                NewMessage.Attachments.Add(new Attachment(filePath));
            }
        }
    }

    private void RemoveAttachment_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Attachment attachment)
        {
            NewMessage.Attachments.Remove(attachment);
        }
    }


    public enum MessageAction
    {
        None, 
        Send, 
        SaveDraft
    }
}
