using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmailClient;

public class ViewModel : INotifyPropertyChanged
{
    public ViewModel()
    {
        
        Messages.Add(new Email(
            DateTime.Now, 
            Email.ImportanceLevel.High,
            "sender1@wp.pl",
            ["rec1@gmail.com", "rec2@wp.pl"], 
            "Question", "Dear Team,\n\nI hope this message finds you well. I would like to request a meeting to discuss the upcoming project developments. Can we schedule a session for next week?\n\nPlease let me know your availability.\n\nBest regards,\nJohn Doe", 
            [],
            true)
        );
        Messages.Add(new Email(
            DateTime.Now - TimeSpan.FromDays(1), 
            Email.ImportanceLevel.Low,
            "sender18789@wp.pl",
            ["rec5661@gmail.com", "rec78902@wp.pl"], 
            "Question2", "Hello,\n\nI wanted to inform you of some important changes that will be happening this month. We are restructuring the project timelines and will be introducing new milestones. I will provide more detailed information during our next meeting.\n\nPlease feel free to reach out if you have any immediate questions.\n\nBest regards,\nJane Smith", 
            ["myattachment"],
            false)
        );
        
    }
    
    
    public ObservableCollection<Email> Messages
    {
        get { return _messages; }
        set
        {
            _messages = value; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Messages"));
        }
    }

    public Email? SelectedMessage
    {
        get { return _selectedMessage; }
        set { _selectedMessage = value; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedMessage")); }
    }
   
    private Email? _selectedMessage = null;
    private ObservableCollection<Email> _messages = new ObservableCollection<Email>();
    public event PropertyChangedEventHandler? PropertyChanged;
}