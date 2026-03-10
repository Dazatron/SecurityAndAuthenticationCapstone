using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecurityAndAuthenticationCapstone.Services;
using System.ComponentModel.DataAnnotations;

public class RegisterModel : PageModel
{
    private readonly AuthService _authService;

    public RegisterModel(AuthService authService)
    {
        _authService = authService;
    }

    [BindProperty]
    [Required]
    [MinLength(2)]
    public string Username { get; set; }

    [BindProperty]
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [BindProperty]
    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [BindProperty]
    public string Role { get; set; } = "user";

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        string cleanUsername = InputSanitizer.SanitizeUsername(Username);
        string cleanEmail = InputSanitizer.SanitizeEmail(Email);
        string cleanPassword = InputSanitizer.SanitizePassword(Password);
        string cleanRole = InputSanitizer.SanitizeRole(Role);

        bool success = _authService.RegisterUser(cleanUsername, cleanEmail, cleanPassword, cleanRole);

        if (success)
        {
            TempData["Message"] = "Registration successful! You can now log in.";
            return RedirectToPage("/Login");
        }

        ModelState.AddModelError("", "Registration failed. Username or email may already exist.");
        return Page();
    }
}