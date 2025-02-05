using System.Net;
using GoldPrice.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;

public class GoldPriceOrchestrator
{
    [Function(nameof(StartGoldPriceCheck))]
    public async Task<HttpResponseData> StartGoldPriceCheck(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
        [DurableClient] DurableTaskClient client)
    {
        string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(nameof(RunGoldPriceOrchestration));
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(new { instanceId });
        return response;
    }

    [Function(nameof(RunGoldPriceOrchestration))]
    public async Task RunGoldPriceOrchestration([OrchestrationTrigger] TaskOrchestrationContext context)
    {
        var goldPrice = await context.CallActivityAsync<GoldPriceModel>(nameof(GoldPriceActivity.GetGoldPrice), null);
        await context.CallActivityAsync(nameof(EmailActivity.SendGoldPriceEmail), goldPrice);
    }
} 