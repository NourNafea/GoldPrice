namespace GoldPrice.Models;

public class SendGridOptions
{
    public string ApiKey { get; set; }
    public string FromEmail { get; set; }
    public string FromName { get; set; }
    public string ToEmail { get; set; }
}