using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace EmailClient;

public class EmailFolder : INotifyPropertyChanged
{
    private string _name = string.Empty;
    private string _icon = string.Empty;
    private ObservableCollection<Email> _emails = new ObservableCollection<Email>();
    
    public EmailFolder(string name, string icon, ObservableCollection<Email> emails)
    {
        Name = name;
        Icon = icon;
        Emails = emails;
    }
    
    public event PropertyChangedEventHandler? PropertyChanged = null;
    
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
        }
    }

    public string Icon
    {
        get { return _icon; }
        set
        {
            _icon = value; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Icon"));
        }
    }
    public ObservableCollection<Email> Emails
    {
        get { return _emails; }
        set
        {
            _emails = value; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Emails"));
        }
    }
}