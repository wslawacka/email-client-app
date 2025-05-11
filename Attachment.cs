namespace EmailClient;

public class Attachment
{
    public string FilePath { get; set; }
    public string FileName => System.IO.Path.GetFileName(FilePath); // extract filename

    public Attachment(string filePath)
    {
        FilePath = filePath;
    }
}