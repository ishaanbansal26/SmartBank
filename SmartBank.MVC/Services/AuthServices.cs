using SmartBank.AuthService.DTOs;
using System.Net.Http;
using System.Net.Http.Json;


namespace SmartBank.AuthService.Services
{

    public class AuthServices
    {
        private readonly HttpClient _httpClient;

        public AuthServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/register", dto);
            //this sends a post request to the api/auth/login with registration data serialized as json

            return response.IsSuccessStatusCode;
        }
        public async Task<string> LoginAsync(LoginDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", dto);
            //this sends a post request to the api/auth/login with login data serialized as json

            if (!response.IsSuccessStatusCode)
                return null;
            //if sucessful the api sends the token in json format
            // Read token case-insensitively because the API returns { "token": "..." }
            var result = await response.Content.ReadFromJsonAsync<TokenResponse>(new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result?.Token; //returns the token
        }
    }

    public class TokenResponse
    {
        public string Token { get; set; }
    }
}

