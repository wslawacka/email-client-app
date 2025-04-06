using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace EmailClient;

public class Mailbox : INotifyPropertyChanged
{
    private string _name = string.Empty;    
    private ObservableCollection<EmailFolder> _folders = new ObservableCollection<EmailFolder>();

    public Mailbox(string name, ObservableCollection<EmailFolder> folders)
    {
        Name = name;
        Folders = folders;
    }
    
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
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
    
    public event PropertyChangedEventHandler? PropertyChanged = null;
}