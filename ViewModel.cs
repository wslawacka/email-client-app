using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Avalonia;
using MsBox.Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace EmailClient;

public class ViewModel : INotifyPropertyChanged
{
    public ViewModel()
    {
        // initialize commands
        HandleDoubleClickOnMessage = new Command(MessageListBox_OnDoubleTapped);
        HandleAddNewMailStatic = new Command(AddNewMailStatic, CanAddNewMail);
        HandleAddNewMail = new Command(OpenAddMessageCallback, CanAddNewMail);
        HandleDeleteMail = new Command(DeleteMail, CanDeleteMessage);
        HandleEditDraft = new Command(EditDraft, CanEditDraft);
        HandleEditMail = new Command(OpenEditMessageCallback, CanEditMail);
        
        // create and populate example mailbox data
         Mailboxes = new ObservableCollection<Mailbox>
        {
            // first mailbox
            new Mailbox("mail1@gmail.com", new ObservableCollection<EmailFolder>
            {
                new EmailFolder("Inbox", "assets/inbox_icon.png", new ObservableCollection<Email>
                {
                    new Email(DateTime.Now, Email.ImportanceLevel.High, "alice@example.com", 
                        new[] { "bob@example.com" }, "Meeting Reminder", 
                        "Just a quick reminder that we have a meeting scheduled for tomorrow at 10 AM. It will be a productive session to discuss upcoming projects. Make sure to bring your latest reports.", 
                        new ObservableCollection<Attachment>(), false),
                    new Email(DateTime.Now.AddDays(-1), Email.ImportanceLevel.Normal, "bob@example.com", 
                        new[] { "alice@example.com" }, "Project Update", 
                        "I wanted to update you on the progress of the ongoing project. We have made significant strides this week, and we are aiming to complete the next milestone by Friday. Let's discuss the details in our meeting tomorrow.", 
                        new ObservableCollection<Attachment>(), true)
                }),
                new EmailFolder("Starred", "assets/star_icon.png", new ObservableCollection<Email>
                {
                    new Email(DateTime.Now.AddDays(-3), Email.ImportanceLevel.High, "manager@example.com", 
                        new[] { "team@example.com" }, "Urgent Meeting", 
                        "We have an urgent meeting scheduled for tomorrow at 9 AM. It will be crucial to finalize the details for the new product launch. Your attendance is mandatory.", 
                        new ObservableCollection<Attachment>(), true),
                    new Email(DateTime.Now.AddDays(-2), Email.ImportanceLevel.High, "admin@example.com", 
                        new[] { "everyone@example.com" }, "System Maintenance", 
                        "There will be scheduled system maintenance tonight from 11 PM to 1 AM. Please ensure all critical work is saved before that time. The downtime will affect several services, including email and file sharing.", 
                        new ObservableCollection<Attachment>(), true)
                }),
                new EmailFolder("Sent", "assets/sent_icon.png", new ObservableCollection<Email>
                {
                    new Email(DateTime.Now.AddHours(-3), Email.ImportanceLevel.Low, "you@example.com", 
                        new[] { "charlie@example.com" }, "Thanks for your help!", 
                        "I just wanted to send a quick thank you for all the help you provided during the last project. Your insights were invaluable, and I look forward to working together again in the future.", 
                        new ObservableCollection<Attachment>(), false),
                    new Email(DateTime.Now.AddDays(-4), Email.ImportanceLevel.Normal, "you@example.com", 
                        new[] { "team@example.com" }, "Project Details", 
                        "Following up on the project we discussed last week, I wanted to confirm the action items and deliverables. Please refer to the attached document for a detailed breakdown of tasks and deadlines.", 
                        new ObservableCollection<Attachment>(), false)
                }),
                new EmailFolder("Draft", "assets/draft_icon.png", new ObservableCollection<Email>
                {
                    new Email(DateTime.Now.AddDays(-1), Email.ImportanceLevel.Normal, "you@example.com", 
                        new[] { "manager@example.com" }, "Proposal Update", 
                        "I have made updates to the proposal document as per your feedback. I've attached the revised version, and I'm awaiting your approval to send it to the client. Please review it at your earliest convenience.", 
                        new ObservableCollection<Attachment>(), true),
                    new Email(DateTime.Now.AddDays(-2), Email.ImportanceLevel.Normal, "you@example.com", 
                        new[] { "team@example.com" }, "Meeting Agenda", 
                        "I wanted to send over the proposed agenda for tomorrow's meeting. Please review the topics and let me know if there are any additional points we should include. The meeting is scheduled for 10 AM.", 
                        new ObservableCollection<Attachment>(), false)
                }),
                new EmailFolder("Spam", "assets/spam_icon.png", new ObservableCollection<Email>
                {
                    new Email(DateTime.Now.AddDays(-5), Email.ImportanceLevel.Low, "noreply@example.com", 
                        new[] { "you@example.com" }, "Congratulations! You've won a prize!", 
                        "You are the lucky winner of a $500 gift card! Click here to claim your prize now.", 
                        new ObservableCollection<Attachment>(), false),
                    new Email(DateTime.Now.AddDays(-6), Email.ImportanceLevel.Low, "offer@example.com", 
                        new[] { "you@example.com" }, "Exclusive Offer Just for You!", 
                        "Get 50% off on your next purchase. This exclusive offer is available only for a limited time. Don't miss out on this amazing deal.", 
                        new ObservableCollection<Attachment>(), false)
                }),
                new EmailFolder("Trash", "assets/trash_icon.png", new ObservableCollection<Email>
                {
                    new Email(DateTime.Now.AddDays(-7), Email.ImportanceLevel.Low, "you@example.com", 
                        new[] { "someone@example.com" }, "Old Email", 
                        "This is an old email that should have been deleted earlier. I'm moving it to the trash now.", 
                        new ObservableCollection<Attachment>(), true),
                    new Email(DateTime.Now.AddDays(-8), Email.ImportanceLevel.Low, "you@example.com", 
                        new[] { "someone@example.com" }, "Test Email", 
                        "Just testing the email system. This message should not be important.", 
                        new ObservableCollection<Attachment>(), true)
                })
            }),
            // second mailbox
            new Mailbox("mail2@gmail.com", new ObservableCollection<EmailFolder>
            {
                new EmailFolder("Inbox", "assets/inbox_icon.png", new ObservableCollection<Email>
                {
                    new Email(DateTime.Now, Email.ImportanceLevel.Normal, "john@example.com", 
                        new[] { "alice@example.com" }, "Update on the project", 
                        "Here is an update on the current status of the project. We are almost done with the first phase and are preparing for the next phase of the rollout. Please review the attached files for detailed insights.", 
                        new ObservableCollection<Attachment>(), false),
                    new Email(DateTime.Now.AddDays(-1), Email.ImportanceLevel.Low, "mike@example.com", 
                        new[] { "alice@example.com" }, "Upcoming Events", 
                        "There are several events coming up next month, and I wanted to make sure you're aware of them. Please check the attached calendar for more information. Let me know if you'd like to attend any of them.", 
                        new ObservableCollection<Attachment>(), true)
                }),
                new EmailFolder("Starred", "assets/star_icon.png", new ObservableCollection<Email>
                {
                    new Email(DateTime.Now.AddDays(-3), Email.ImportanceLevel.High, "admin@example.com", 
                        new[] { "everyone@example.com" }, "New System Update", 
                        "The system will undergo a major update tonight, and services will be unavailable for several hours. Please make sure to save all your work before the update begins. We apologize for any inconvenience caused.", 
                        new ObservableCollection<Attachment>(), true),
                    new Email(DateTime.Now.AddDays(-4), Email.ImportanceLevel.Normal, "manager@example.com", 
                        new[] { "team@example.com" }, "Team Meeting Scheduled", 
                        "We have scheduled a team meeting for tomorrow to discuss the next steps in the project. Please make sure you're prepared with updates for your respective sections. The meeting will start at 9 AM.", 
                        new ObservableCollection<Attachment>(), true)
                }),
                new EmailFolder("Sent", "assets/sent_icon.png", new ObservableCollection<Email>
                {
                    new Email(DateTime.Now.AddDays(-2), Email.ImportanceLevel.Normal, "you@example.com", 
                        new[] { "jane@example.com" }, "Project Feedback", 
                        "Thank you for your feedback on the recent project. We have reviewed your suggestions and will incorporate them in the next version. Looking forward to working with you again in the future.", 
                        new ObservableCollection<Attachment>(), false),
                    new Email(DateTime.Now.AddDays(-5), Email.ImportanceLevel.Low, "you@example.com", 
                        new[] { "john@example.com" }, "Follow-up on Previous Discussion", 
                        "Just following up on our previous conversation. I wanted to check in and see if you had any additional questions regarding the documents I sent over. Let me know if there's anything I can help with.", 
                        new ObservableCollection<Attachment>(), false)
                }),
                new EmailFolder("Draft", "assets/draft_icon.png", new ObservableCollection<Email>
                {
                    new Email(DateTime.Now.AddDays(-6), Email.ImportanceLevel.Normal, "you@example.com", 
                        new[] { "admin@example.com" }, "Budget Proposal", 
                        "I've updated the budget proposal document and made the necessary revisions based on the feedback we received. Please review it and let me know if anything else needs to be changed before we send it to the client.", 
                        new ObservableCollection<Attachment>(), true),
                    new Email(DateTime.Now.AddDays(-7), Email.ImportanceLevel.Normal, "you@example.com", 
                        new[] { "team@example.com" }, "Team Outing", 
                        "I wanted to send over some ideas for the team outing next month. Please check the attached document for some suggested locations and activities. Let me know your thoughts.", 
                        new ObservableCollection<Attachment>(), false)
                }),
                new EmailFolder("Spam", "assets/spam_icon.png", new ObservableCollection<Email>
                {
                    new Email(DateTime.Now.AddDays(-8), Email.ImportanceLevel.Low, "noreply@example.com", 
                        new[] { "you@example.com" }, "Free Gift Card Offer", 
                        "Congratulations, you've won a $500 gift card. Click here to claim your prize now. Limited time offer!", 
                        new ObservableCollection<Attachment>(), false),
                    new Email(DateTime.Now.AddDays(-9), Email.ImportanceLevel.Low, "ads@example.com", 
                        new[] { "you@example.com" }, "Limited Time Deal!", 
                        "Get 70% off your next purchase! Click here to claim your discount before it expires. Don't miss out on this amazing deal!", 
                        new ObservableCollection<Attachment>(), false)
                }),
                new EmailFolder("Trash", "assets/trash_icon.png", new ObservableCollection<Email>
                {
                    new Email(DateTime.Now.AddDays(-10), Email.ImportanceLevel.Low, "you@example.com", 
                        new[] { "someone@example.com" }, "Old Email", 
                        "This email is no longer needed. Moving it to trash.", 
                        new ObservableCollection<Attachment>(), true),
                    new Email(DateTime.Now.AddDays(-11), Email.ImportanceLevel.Low, "you@example.com", 
                        new[] { "someone@example.com" }, "Test Message", 
                        "This is a test message. Deleting it now.", 
                        new ObservableCollection<Attachment>(), true)
                })
            })
        };
    }
    
    // commands bound to UI actions
    public ICommand HandleDoubleClickOnMessage { get; private set; }
    public ICommand HandleAddNewMailStatic { get; private set; }
    public ICommand HandleAddNewMail { get; private set; }
    public ICommand HandleDeleteMail { get; private set; }
    public ICommand HandleEditDraft { get; private set; }
    public ICommand HandleEditMail { get; private set; }
    
    // observable collection for emails
    public ObservableCollection<Email> Messages
    {
        get { return _messages; }
        set
        {
            _messages = value; 
            _filteredMessages = new ObservableCollection<Email>(Messages);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Messages"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilteredMessages"));
        }
    }
    
    // observable collection for mailboxes
    public ObservableCollection<Mailbox> Mailboxes
    {
        get { return _mailboxes; }
        set
        {
            _mailboxes = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Mailboxes"));
        }
    }

    // currently selected message
    public Email? SelectedMessage
    {
        get { return _selectedMessage; }
        set { _selectedMessage = value; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedMessage")); }
    }
    
    // currently selected folder
    public EmailFolder? SelectedFolder
    {
        get { return _selectedFolder; }
        set
        {
            _selectedFolder = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedFolder"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEditable"));
            
            if (SelectedFolder != null)
            {
                Messages = SelectedFolder.Emails;
                FilteredMessages = new ObservableCollection<Email>(Messages);
            }
            else
            {
                Messages = new ObservableCollection<Email>();
                FilteredMessages = new ObservableCollection<Email>();
            }
        }
    }
    
    // event used to notify the UI when a property value changes
    public event PropertyChangedEventHandler? PropertyChanged;
    
    private Email? _selectedMessage = null;
    private EmailFolder? _selectedFolder = null;
    
    private ObservableCollection<Email> _messages = new ObservableCollection<Email>();
    private ObservableCollection<Mailbox> _mailboxes = new ObservableCollection<Mailbox>();
    
    
    // displays a message box with the subject when an email's preview is double-clicked
    private async void MessageListBox_OnDoubleTapped(object? parameter)
    {
        if (parameter is Email email)
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Message Subject", $"You clicked on a message with the subject: {email.Subject}");

            await box.ShowAsync();
        }
    }

    // adds a static email to the currently selected folder
    private void AddNewMailStatic(object? parameter)
    {
        if (SelectedFolder != null)
        {
            var newEmail = new Email(
                DateTime.Now,
                Email.ImportanceLevel.Normal,
                "static@example.com",
                new[] { "recipient@example.com" },
                "Static Email Subject",
                "This is a static email content created for demonstration purposes.",
                new ObservableCollection<Attachment>(),
                false
            );
            
            SelectedFolder.Emails.Add(newEmail);
            FilteredMessages.Add(newEmail);
        }
    }

    private async void OpenAddMessageCallback(object? obj)
    {
        var wnd = new AddMessage();
        var applicationLifetime = Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        var mainWindow = applicationLifetime?.MainWindow;
        if (mainWindow != null)
        {
            await wnd.ShowDialog(mainWindow);
            if (wnd.UserAction == AddMessage.MessageAction.Send)
            {
                SelectedFolder?.Emails.Add(wnd.NewMessage);
                FilteredMessages.Add(wnd.NewMessage);
            }
            else if (wnd.UserAction == AddMessage.MessageAction.SaveDraft)
            {
                if (SelectedFolder != null)
                {
                    // find the correct mailbox that contains the SelectedFolder
                    var selectedMailbox = Mailboxes.FirstOrDefault(mailbox =>
                        mailbox.Folders.Contains(SelectedFolder));

                    if (selectedMailbox != null)
                    {
                        // find the "Draft" folder in the selected mailbox
                        var draftFolder = selectedMailbox.Folders.FirstOrDefault(f => f.Name == "Draft");

                        if (draftFolder != null)
                        {
                            // add the new message to the draft folder
                            draftFolder.Emails.Add(wnd.NewMessage);
                            SelectedFolder = draftFolder; // update the selected folder to Draft
                        }
                    }
                }
                
            }
            SelectedMessage = wnd.NewMessage;
        }
    }

    private void OpenEditMessageCallback(object? obj)
    {
        if (_editWnd == null)
        {
            _editWnd = new EditMessage(this);
            var applicationLifetime = Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
            var mainWindow = applicationLifetime?.MainWindow;
            if (mainWindow != null)
            {
                _editWnd.Show(mainWindow);
                _editWnd.Closed += (sender, e) => _editWnd = null;
            }
        }
    }

    // deletes the currently selected message
    private void DeleteMail(object? parameter)
    {
        if (SelectedMessage != null && SelectedFolder != null)
        {
            SelectedFolder.Emails.Remove(SelectedMessage);
            SelectedMessage = null;
        }
    }
    
    // edits the currently selected draft message
    private void EditDraft(object? parameter)
    {
        if (SelectedMessage != null)
        {
            SelectedMessage.Subject = "Edited Draft Message";
            SelectedMessage.Content = "The content of the draft message has been edited.";
        }
    }
    
    // determines whether the selected message can be deleted
    private bool CanDeleteMessage(object? parameter)
    {
        return SelectedMessage != null;
    }

    // determines whether the selected message can be edited (only in Draft folder)
    private bool CanEditDraft(object? parameter)
    {
        return SelectedFolder != null && SelectedFolder.Name == "Draft" && SelectedMessage != null;
    }
    
    // determines whether a message is selected to be viewed or edited
    private bool CanEditMail(object? parameter)
    {
        return SelectedFolder != null && SelectedMessage != null;
    }

    // determines whether the new message can be added
    private bool CanAddNewMail(object? parameter)
    {
        return SelectedFolder != null;
    }
    
    public Email.ImportanceLevel[] ImportanceValues => Enum.GetValues<Email.ImportanceLevel>();

    private EditMessage? _editWnd = null;
    
    public bool IsEditable => SelectedFolder != null && SelectedFolder.Name == "Draft";
    
    
    
    
    public ObservableCollection<Email> FilteredMessages
    {
        get => _filteredMessages;
        set
        {
            _filteredMessages = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilteredMessages)));
        }
    }
    private ObservableCollection<Email> _filteredMessages = new ObservableCollection<Email>();

    
    
    
    public void FilterEmails(string query, string category)
    {
        var filtered = Messages.Where(email =>
            category.ToLower() switch
            {
                "subject" => email.Subject.Contains(query, StringComparison.OrdinalIgnoreCase),
                "sender" => email.Sender.Contains(query, StringComparison.OrdinalIgnoreCase),
                "recipient" => email.RecipientsString.Contains(query, StringComparison.OrdinalIgnoreCase),
                _ => true
            }).ToList();

        FilteredMessages.Clear();
        foreach (var email in filtered)
        {
            FilteredMessages.Add(email);
        }
    }

    public void ResetSearch()
    {
        FilteredMessages.Clear();
        foreach (var email in Messages)
        {
            FilteredMessages.Add(email);
        }
    }
    
    
    private string _currentQuery = "";
    public string CurrentQuery
    {
        get => _currentQuery;
        set
        {
            _currentQuery = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentQuery)));
            FilterEmails(_currentQuery, _currentCategory);
        }
    }

    private string _currentCategory = "subject"; // default category
    public string CurrentCategory
    {
        get => _currentCategory;
        set
        {
            _currentCategory = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentCategory)));
            FilterEmails(_currentQuery, _currentCategory);
        }
    }



}