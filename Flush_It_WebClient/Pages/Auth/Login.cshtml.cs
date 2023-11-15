using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using Flush_It_WebClient.Models;

namespace Flush_It_WebClient.Pages.Auth
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public UserLoginDto User { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var apiUrl = "https://localhost:7080/api/auth/login";

                var userJson = JsonSerializer.Serialize(User);
                var content = new StringContent(userJson, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login failed. Please check your credentials and try again.");
                    return Page();
                }
            }
        }
    }
}
