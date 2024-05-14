using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace RecipesUI.Services
{
    public class ApiService<T> : IApiService<T>
    {
        protected readonly HttpClient _httpClient;
        protected readonly ILogger<ApiService<T>> _logger;

        public ApiService(IConfiguration configuration, ILogger<ApiService<T>> logger)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(configuration["ApiBaseUrl"])
            };
            _logger = logger;
        }
        
        
    }
}
