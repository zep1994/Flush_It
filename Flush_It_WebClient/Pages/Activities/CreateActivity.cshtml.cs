using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using Flush_It_WebClient.Models;
using Flush_It_WebClient.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;

namespace Flush_It_WebClient.Pages.Activities
{
    public class CreateActivityModel : PageModel
    {
        private readonly UserService _userService;
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateActivityModel(UserService userService, IHttpClientFactory httpClientFactory)
        {
            _userService = userService;
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public ActivityCreateDto Activity { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userService.GetCurrentUserId();

            Activity.UserId = userId;


            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                try
                {
                    var apiUrl = "https://localhost:7080/api/activity/create";

                    // Split each food name individually
                    Activity.FoodNames = Activity.FoodNames
                        .SelectMany(names => names.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(name => name.Trim()))
                        .ToList();

                    var activityJson = JsonSerializer.Serialize(new
                    {
                        Activity.UserId,
                        Activity.FoodNames,
                        Activity.IsHealthyDay,
                        Activity.HadAttack,
                        Activity.Date
                    });
                    var content = new StringContent(activityJson, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(apiUrl, content);


                    return RedirectToPage("/Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Activity creation failed. {ex.Message}");
                    return Page();
                }
            }
        }
    }

}