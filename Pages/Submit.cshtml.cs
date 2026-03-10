using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecurityAndAuthenticationCapstone.Data;
using SecurityAndAuthenticationCapstone.Services;
using System.ComponentModel.DataAnnotations;

namespace SecurityAndAuthenticationCapstone.Pages
{
    public class SubmitModel : PageModel
    {
        private readonly UserRepository _repository;

        public SubmitModel(UserRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression("^[a-zA-Z0-9]{2,}$",
            ErrorMessage = "Username must be at least 2 alphanumeric characters")]
        public string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        public IActionResult OnPost()
        {
            string cleanUsername = InputSanitizer.SanitizeUsername(Username);
            string cleanEmail = InputSanitizer.SanitizeEmail(Email);

            _repository.AddUser(cleanUsername, cleanEmail);

            return Page();
        }
    }
}