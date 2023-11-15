using Flush_It_WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;

namespace Flush_It_WebClient.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public UserRegisterDto User { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterModel(IHttpClientFactory httpClientFactory)
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
                var apiUrl = "https://localhost:7080/api/auth/register";

                var userJson = JsonSerializer.Serialize(User);
                var content = new StringContent(userJson, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["RegistrationSuccess"] = "Registration successful. Please login.";

                    // Registration successful, you might want to redirect to the login page
                    return RedirectToPage("/Auth/Login");
                }
                else
                {
                    // Registration failed
                    ModelState.AddModelError(string.Empty, "Registration failed. Please check your details and try again.");
                    return Page();
                }
            }
        }
    }
}