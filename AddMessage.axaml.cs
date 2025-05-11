using System;
using System.Text.RegularExpressions;
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
        DataContext = NewMessage = new Email();
    }

    private void SendButton_Click(object sender, RoutedEventArgs e)
    {
        // validate user input!!!
        validateInputs(false);
        UserAction = MessageAction.Send;
        Close(true);
    }

    private void SaveDraftButton_Click(object sender, RoutedEventArgs e)
    {
        // validate user input!!!
        validateInputs(true);
        UserAction = MessageAction.SaveDraft;
        Close(true);
    }

    private async void validateInputs(bool allowEmptyRecipients = false)
    {
        var sender = SenderTextBox.Text?.Trim();
        var recipientsRaw = RecipientsTextBox.Text?.Trim();
        var subject = SubjectTextBox.Text?.Trim();
        var content = ContentTextBox.Text?.Trim();

        if (string.IsNullOrEmpty(sender))
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Invalid input", $"Sender is required!");
            await box.ShowAsync();
        }
        
        if (!allowEmptyRecipients && string.IsNullOrEmpty(recipientsRaw))
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Invalid input", $"Recipient is required!");
            await box.ShowAsync();
        }
        
        var recipientsList = recipientsRaw?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (!allowEmptyRecipients)
        {
            foreach (var recipient in recipientsList!)
            {
                if (!Regex.IsMatch(recipient, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    var box = MessageBoxManager
                        .GetMessageBoxStandard("Invalid input", $"Invalid email address!");
                    await box.ShowAsync();
                }
            }
        }

        if (string.IsNullOrEmpty(subject))
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Invalid input", $"Subject is required!");
            await box.ShowAsync();
        }
        
        if (string.IsNullOrEmpty(content))
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Invalid input", $"Content is required!");
            await box.ShowAsync();
        }

        NewMessage.Sender = sender;
        NewMessage.RecipientsString = recipientsRaw;
        NewMessage.Subject = subject;
        NewMessage.Content = content;
    }


    public enum MessageAction
    {
        None, 
        Send, 
        SaveDraft
    }
}
