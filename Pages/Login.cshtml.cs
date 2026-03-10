using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecurityAndAuthenticationCapstone.Services;
using System.ComponentModel.DataAnnotations;

public class LoginModel : PageModel
{
    private readonly AuthService _authService;

    public LoginModel(AuthService authService)
    {
        _authService = authService;
    }

    [BindProperty]
    [Required]
    [MinLength(2)]
    public string Username { get; set; }

    [BindProperty]
    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        string cleanUsername = InputSanitizer.SanitizeUsername(Username);
        string cleanPassword = InputSanitizer.SanitizePassword(Password);

        if (_authService.AuthenticateUser(cleanUsername, cleanPassword, out string role))
        {
            HttpContext.Session.SetString("Username", Username);
            HttpContext.Session.SetString("Role", role);

            return RedirectToPage("/Index");
        }

        ModelState.AddModelError("", "Invalid login");
        return Page();
    }
}