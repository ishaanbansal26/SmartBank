using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SmartBank.MVC.Services
{
    public class AccountServices
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountServices(HttpClient httpClient, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClient;
            _httpClientFactory = httpClientFactory;
            //if we wanna use the same service for multiple api then the httpclientfactory is best
            // it cretaes a sepearate names client like httpclient keeping its configs seperated
        }

        // GET ALL ACCOUNTS
        public async Task<List<AccountDto>> GetAccounts(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            //this particular line adds the Authentication Header
            //it calls the GET api/accounts
            var accounts = await _httpClient.GetFromJsonAsync<List<AccountDto>>("");
            //deserializes the jsonlist into List<AccountDto>

            // Ensure we never return null to callers
            return accounts ?? new List<AccountDto>();
        }

        // CREATE ACCOUNT
        public async Task<bool> CreateAccount(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync("", null);
            return response.IsSuccessStatusCode;

            //adds the bearer token and calls the post api/accounts with null body
        }

        // DELETE ACCOUNT
        public async Task<bool> DeleteAccount(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.DeleteAsync($"{id}");
            return response.IsSuccessStatusCode;
        }

        // DEPOSIT
        public async Task<bool> Deposit(int accountId, decimal amount, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            //it sets the authorization header on the httpClient basically it attaches the token so the backend api knows which user 
            //is requesting

            var dto = new TransactionDto
            {
                AccountId = accountId,
                Amount = amount,
                Type = "Deposit",
                Description = "Deposit from Account",
                Date = DateTime.Now
            };
            //creates a transaction dto
            //sends a post request to api/accounts/deposit
            var response = await _httpClient.PostAsJsonAsync("deposit", dto);
            // the dto object is serialized into json and sent into the request body

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Account API deposit failed: {response.StatusCode}, {error}");
            }

            return true;
        }

        // WITHDRAW
        public async Task<bool> Withdraw(int accountId, decimal amount, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var dto = new TransactionDto
            {
                AccountId = accountId,
                Amount = amount,
                Type = "Withdraw",
                Description = "Withdraw from MVC",
                Date = DateTime.Now
            };

            var response = await _httpClient.PostAsJsonAsync("withdraw", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Account API withdraw failed: {response.StatusCode}, {error}");
            }
            
            return true;
        }

        public async Task<List<TransactionDto>> GetTransactions(int accountId, string token)
        {
            // Use a dedicated Transactions client registered in Program.cs
            // the httpclientfactory is used for creating a named client
            var client = _httpClientFactory.CreateClient("TransactionsClient");

            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //if JWT provided it sends the authorization header

            try
            {
                // sends a get request to the api/transactions/accoun/id and the response json is deseialized
                // Request via API gateway: the gateway matches paths starting with /Transactions
                // and forwards to /api/Transactions on the TransactionService.
                var transactions = await client.GetFromJsonAsync<List<TransactionDto>>($"Transactions/account/{accountId}");
                return transactions ?? new List<TransactionDto>();
            }
            catch (HttpRequestException)
            {
                return new List<TransactionDto>();
            }
        }
    }

    public class AccountDto
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
    }

    public class TransactionDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}

//http client is a class for sending the Http Requests and receiving response