using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flush_It_WebClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string LoginSuccessMessage { get; private set; }


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // Check for the presence of the query parameter
            if (Request.Query.ContainsKey("loginSuccess") && Request.Query["loginSuccess"] == "true")
            {
                // Set the success message
                LoginSuccessMessage = "Login successful. Please enjoy!";
            }
        }
    }
}