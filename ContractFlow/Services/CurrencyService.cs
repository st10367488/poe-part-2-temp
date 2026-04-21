using System.Text.Json;

namespace ContractMS.Services
{
    public class CurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CurrencyService> _logger;

        public CurrencyService(HttpClient httpClient, IConfiguration configuration, ILogger<CurrencyService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<decimal> GetUsdToZarRateAsync()
        {
            try
            {
                var apiKey = _configuration["CurrencyApi:ApiKey"];
                if (string.IsNullOrEmpty(apiKey))
                {
                    _logger.LogWarning("Currency API key not found in configuration");
                    return 18.50m; // Default fallback rate
                }

                var url = $"https://v6.exchangerate-api.com/v6/latest/USD?apikey={apiKey}";
                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Currency API request failed with status: {StatusCode}", response.StatusCode);
                    return 18.50m; // Default fallback rate
                }

                var content = await response.Content.ReadAsStringAsync();
                var currencyData = JsonSerializer.Deserialize<CurrencyResponse>(content);
                
                if (currencyData?.conversion_rates?.ZAR == null)
                {
                    _logger.LogError("ZAR rate not found in API response");
                    return 18.50m; // Default fallback rate
                }

                return currencyData.conversion_rates.ZAR;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching currency conversion rate");
                return 18.50m; // Default fallback rate
            }
        }

        public decimal ConvertUsdToZar(decimal usdAmount, decimal exchangeRate)
        {
            return Math.Round(usdAmount * exchangeRate, 2);
        }
    }

    public class CurrencyResponse
    {
        public string result { get; set; } = string.Empty;
        public string documentation { get; set; } = string.Empty;
        public string terms_of_use { get; set; } = string.Empty;
        public int time_last_update_unix { get; set; }
        public string time_last_update_utc { get; set; } = string.Empty;
        public ConversionRates conversion_rates { get; set; } = new ConversionRates();
    }

    public class ConversionRates
    {
        public decimal ZAR { get; set; }
    }
}
