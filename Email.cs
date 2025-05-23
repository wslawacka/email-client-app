using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Avalonia.Media.Imaging;

namespace EmailClient;

public class Email : INotifyPropertyChanged
{
    private DateTime _date = DateTime.Now;
    private ImportanceLevel _importance = ImportanceLevel.Normal;
    private string _sender = string.Empty;
    private string[] _recipients = Array.Empty<string>();
    private string _subject = string.Empty;
    private string _content = string.Empty;
    private ObservableCollection<Attachment> _attachments = new ObservableCollection<Attachment>();
    private bool _isStarred = false;

    public Email(){}

    public Email(DateTime date, ImportanceLevel importance, string sender, string[] recipients, string subject, 
        string content, ObservableCollection<Attachment> attachments, bool isStarred)
    {
        Date = date;
        Importance = importance;
        Sender = sender;
        Recipients = recipients;
        RecipientsString = string.Join(", ", recipients);
        Subject = subject;
        Content = content;
        Attachments = attachments;
        IsStarred = isStarred;
    }
    
    public event PropertyChangedEventHandler? PropertyChanged = null;

    public DateTime Date
    {
        get { return _date; }
        set
        {
            _date = value; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date"));
        }
    }
    public ImportanceLevel Importance
    {
        get { return _importance; }
        set
        {
            _importance = value; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Importance"));
        }
    }
    public string Sender
    {
        get { return _sender; }
        set
        {
            _sender = value; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Sender"));
        }
    }
    public string[] Recipients
    {
        get { return _recipients; }
        set
        {
            _recipients = value; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Recipients"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RecipientsString"));
        }
    }

    public string RecipientsString
    {
        get { return string.Join(", ", Recipients); }
        set
        {
            _recipients = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            
            Recipients = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(recipient => recipient.Trim())
                    .ToArray();
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RecipientsString"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Recipients"));
        }
    } 
    public string Subject
    {
        get { return _subject; }
        set
        {
            _subject = value; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Subject"));
        }
    }
    public string Content
    {
        get { return _content; }
        set
        {
            _content = value; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));
        }
    }
    public ObservableCollection<Attachment> Attachments
    {
        get { return _attachments; }
        set
        {
            _attachments = value; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Attachments"));
        }
    }
    public bool IsStarred
    {
        get { return _isStarred; }
        set
        {
            _isStarred = value; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsStarred"));
        }
    }
    
    //  returns a Bitmap object which is used to load and display the star icon image in the UI
    public Bitmap StarImage => new Bitmap("assets/star_icon.png");

    public override string ToString()
    {
        return Subject;
    }
    
    public void ToggleStar()
    {
        IsStarred = !IsStarred;
    }
    
    public enum ImportanceLevel
    {
        Low,
        Normal, 
        High
    }
}