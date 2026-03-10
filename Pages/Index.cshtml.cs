using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    public bool IsLoggedIn { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }

    public void OnGet()
    {
        Username = HttpContext.Session.GetString("Username");
        Role = HttpContext.Session.GetString("Role");

        IsLoggedIn = !string.IsNullOrEmpty(Username);
    }
}