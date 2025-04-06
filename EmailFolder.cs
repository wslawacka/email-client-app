using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia.Media.Imaging;

namespace EmailClient;

public class EmailFolder : INotifyPropertyChanged
{
    private string _name = string.Empty;
    private string _icon = string.Empty;
    private ObservableCollection<Email> _emails = new ObservableCollection<Email>();
    
    public EmailFolder(string name, string icon, ObservableCollection<Email> emails)
    {
        Name = name;
        IconPath = icon;
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
    public string IconPath
    {
        get { return _icon; }
        set
        {
            _icon = value; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Icon"));
        }
    }
    
    // creates a new Bitmap object from the file path stored in IconPath
    public Bitmap Icon => new Bitmap(IconPath);
    
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