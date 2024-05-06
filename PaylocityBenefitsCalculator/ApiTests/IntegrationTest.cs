using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ApiTests;

public class IntegrationTest : IDisposable
{
    private HttpClient? _httpClient;

    protected HttpClient HttpClient
    {
        get
        {
            if (_httpClient == default)
            {
                bool isRealServer = Environment.GetEnvironmentVariable("USE_REAL_SERVER") == "true";
                if (isRealServer)
                {
                    // real integration testing
                    _httpClient = new HttpClient
                    {
                        //task: update your port if necessary
                        BaseAddress = new Uri("https://localhost:7124")
                    };
                    _httpClient.DefaultRequestHeaders.Add("accept", "text/plain");
                }
                else
                {
                    // embedded MVC testing
                    WebApplicationFactory<Program> factory = new();
                    _httpClient = factory.CreateClient();
                    _httpClient.DefaultRequestHeaders.Add("accept", "text/plain");
                }
            }

            return _httpClient;
        }
    }

    public void Dispose()
    {
        HttpClient.Dispose();
    }
}

