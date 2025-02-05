# Gold Price Alert Function

An Azure Durable Function that fetches gold prices and sends email notifications using SendGrid.

## Prerequisites

- .NET 6.0 or later
- Azure Functions Core Tools
- Azure Storage Emulator
- SendGrid Account
- Gold API Account

## Configuration

Copy `local.settings.example.json` to `local.settings.json` and update with your values:

- `GoldApiKey`: Your Gold API key
- `SendGrid:ApiKey`: Your SendGrid API key
- `SendGrid:FromEmail`: Verified sender email
- `SendGrid:FromName`: Sender name
- `SendGrid:ToEmail`: Recipient email address

## Local Development

1. Clone the repository
2. Copy `local.settings.example.json` to `local.settings.json`
3. Update settings with your values
4. Run `func start` or debug from Visual Studio

## Deployment

1. Create an Azure Function App
2. Deploy using Visual Studio or Azure CLI
3. Configure application settings in Azure Portal

## Architecture

- HTTP-triggered durable function orchestrator
- Activity function to fetch gold prices
- Activity function to send email notifications
- Logic App for scheduled triggers

## Security Notes

- Never commit sensitive information to source control
- Use Key Vault in production
- Implement proper authentication for the HTTP trigger 