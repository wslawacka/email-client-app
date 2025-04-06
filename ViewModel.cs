using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MsBox.Avalonia;
using Avalonia.Controls;

namespace EmailClient;

public class ViewModel : INotifyPropertyChanged
{
    public ViewModel()
    {
        
        HandleDoubleClickOnMessage = new Command(MessageListBox_OnDoubleTapped);
        
        // Messages.Add(new Email(
        //     DateTime.Now, 
        //     Email.ImportanceLevel.High,
        //     "sender1@wp.pl",
        //     ["rec1@gmail.com", "rec2@wp.pl"], 
        //     "Question", "Dear Team,\n\nI hope this message finds you well. I would like to request a meeting to discuss the upcoming project developments. Can we schedule a session for next week?\n\nPlease let me know your availability.\n\nBest regards,\nJohn Doe", 
        //     [],
        //     true)
        // );
        // Messages.Add(new Email(
        //     DateTime.Now - TimeSpan.FromDays(1), 
        //     Email.ImportanceLevel.Low,
        //     "sender18789@wp.pl",
        //     ["rec5661@gmail.com", "rec78902@wp.pl"], 
        //     "Question2", "Hello,\n\nI wanted to inform you of some important changes that will be happening this month. We are restructuring the project timelines and will be introducing new milestones. I will provide more detailed information during our next meeting.\n\nPlease feel free to reach out if you have any immediate questions.\n\nBest regards,\nJane Smith", 
        //     ["myattachment"],
        //     false)
        // );
        //
        // Mailboxes.Add(new Mailbox(
        //     "mailbox1@gmail.com",
        //     []
        //     ));
        // Mailboxes.Add(new Mailbox(
        //     "mailbox2@gmail.com",
        //     []
        // ));
        //
        //
        // Folders.Add(new EmailFolder(
        //     "mailbox1@gmail.com",
        //     [new Email(
        //         DateTime.Now, 
        //         Email.ImportanceLevel.High,
        //         "sender1@wp.pl",
        //         ["rec1@gmail.com", "rec2@wp.pl"], 
        //         "Question", "Dear Team,\n\nI hope this message finds you well. I would like to request a meeting to discuss the upcoming project developments. Can we schedule a session for next week?\n\nPlease let me know your availability.\n\nBest regards,\nJohn Doe", 
        //         [],
        //         true)]
        // ));
        // Folders.Add(new EmailFolder(
        //     "mailbox2@gmail.com",
        //     [new Email(
        //         DateTime.Now, 
        //         Email.ImportanceLevel.High,
        //         "sender2@wp.pl",
        //         ["rec2@gmail.com", "rec2355@wp.pl"], 
        //         "Question2222", "Dear Team2222,\n\nI hope this message finds you well. I would like to request a meeting to discuss the upcoming project developments. Can we schedule a session for next week?\n\nPlease let me know your availability.\n\nBest regards,\nJohn Doe", 
        //         [],
        //         true)]
        // ));
            Mailboxes = new ObservableCollection<Mailbox>
            {
                new Mailbox("Personal", new ObservableCollection<EmailFolder>
                {
                    new EmailFolder("Inbox", "assets/spam_icon.png",new ObservableCollection<Email>
                    {
                        new Email(DateTime.Now, Email.ImportanceLevel.High, "alice@example.com", 
                            new[] { "bob@example.com" }, "Meeting Reminder", "Don't forget our meeting tomorrow.", 
                            Array.Empty<string>(), false),
                        new Email(DateTime.Now.AddDays(-1), Email.ImportanceLevel.Normal, "bob@example.com", 
                            new[] { "alice@example.com" }, "Project Update", "The project is on track.", 
                            Array.Empty<string>(), true)
                    }),
                    new EmailFolder("Sent", "assets/sent_icon.png",new ObservableCollection<Email>
                    {
                        new Email(DateTime.Now.AddHours(-2), Email.ImportanceLevel.Low, "you@example.com", 
                            new[] { "charlie@example.com" }, "Thanks!", "Thanks for your help.", 
                            Array.Empty<string>(), false)
                    })
                }),
                new Mailbox("Work", new ObservableCollection<EmailFolder>
                {
                    new EmailFolder("Inbox", "assets/star_icon.png",new ObservableCollection<Email>
                    {
                        new Email(DateTime.Now.AddDays(-3), Email.ImportanceLevel.High, "manager@work.com", 
                            new[] { "team@work.com" }, "Urgent: Deadline Approaching", 
                            "Please submit your reports by EOD.", Array.Empty<string>(), false)
                    })
                })
            };
        

        
        
    }
    
    public ICommand HandleDoubleClickOnMessage { get; private set; }
    
    public ObservableCollection<Email> Messages
    {
        get { return _messages; }
        set
        {
            _messages = value; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Messages"));
        }
    }

    public ObservableCollection<EmailFolder> Folders
    {
        get { return _folders; }
        set
        {
            _folders = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Folders"));
        }
    }

    public ObservableCollection<Mailbox> Mailboxes
    {
        get { return _mailboxes; }
        set
        {
            _mailboxes = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Mailboxes"));
        }
    }

    public Email? SelectedMessage
    {
        get { return _selectedMessage; }
        set { _selectedMessage = value; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedMessage")); }
    }

    public EmailFolder? SelectedFolder
    {
        get { return _selectedFolder; }
        set
        {
            _selectedFolder = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedFolder"));
        }
    }

    public Mailbox? SelectedMailbox
    {
        get { return _selectedMailbox;  }
        set
        {
            _selectedMailbox = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedMailbox"));
        }
    }

    
    
    public event PropertyChangedEventHandler? PropertyChanged;
    
    private Email? _selectedMessage = null;
    private EmailFolder? _selectedFolder = null;
    private Mailbox? _selectedMailbox = null;
    
    
    private ObservableCollection<EmailFolder> _folders = new ObservableCollection<EmailFolder>();
    private ObservableCollection<Email> _messages = new ObservableCollection<Email>();
    private ObservableCollection<Mailbox> _mailboxes = new ObservableCollection<Mailbox>();
    
    
    private async void MessageListBox_OnDoubleTapped(object? parameter)
    {
        if (parameter is Email email)
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Message Subject", $"You clicked on a message with the subject: {email.Subject}");

            await box.ShowAsync();
        }
    }
    
}