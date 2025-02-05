using GoldPrice.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailActivity
{
    private readonly SendGridOptions _options;

    public EmailActivity(IOptions<SendGridOptions> options)
    {
        _options = options.Value;
    }

    [Function(nameof(SendGoldPriceEmail))]
    public async Task SendGoldPriceEmail([ActivityTrigger] GoldPriceModel goldPrice)
    {
        try
        {
            var client = new SendGridClient(_options.ApiKey);
            var from = new EmailAddress(_options.FromEmail, _options.FromName);
            var to = new EmailAddress(_options.ToEmail);
            var subject = "Gold Price Alert";
            var htmlContent = $@"
            <h2>Current Gold Price</h2>
            <p>Price: {goldPrice.Price:N2} {goldPrice.Currency}</p>
            <p>As of: {goldPrice.Timestamp:g} UTC</p>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
            await client.SendEmailAsync(msg);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
} 