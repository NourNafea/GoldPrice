using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using GoldPrice.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.Extensions.Configuration;

public class GoldPriceActivity(HttpClient httpClient, IConfiguration configuration)
{

    [Function(nameof(GetGoldPrice))]
    public async Task<GoldPriceModel> GetGoldPrice([ActivityTrigger] TaskActivityContext context)
    {
        
        httpClient.DefaultRequestHeaders.Add("x-access-token", configuration["GoldApiKey"]);
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        var response = await httpClient.GetAsync("https://www.goldapi.io/api/XAU/EGP");
        var jsonString = await response.Content.ReadAsStringAsync();
        var goldApiResponse = JsonSerializer.Deserialize<GoldApiResponse>(jsonString);

        return new GoldPriceModel
        {
            Price = goldApiResponse.PriceGram24k,
            Currency = goldApiResponse.Currency,
            Timestamp = DateTimeOffset.FromUnixTimeSeconds(goldApiResponse.Timestamp).UtcDateTime
        };
    }
    
    public class GoldApiResponse
    {
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("price_gram_24k")]
        public decimal PriceGram24k { get; set; }

        [JsonPropertyName("price_gram_22k")]
        public decimal PriceGram22k { get; set; }

        [JsonPropertyName("price_gram_21k")]
        public decimal PriceGram21k { get; set; }

        [JsonPropertyName("price_gram_18k")]
        public decimal PriceGram18k { get; set; }
    }
} 