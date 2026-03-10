using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class AdminModel : PageModel
{
    public IActionResult OnGet()
    {
        string role = HttpContext.Session.GetString("Role");

        if (role != "admin")
            return RedirectToPage("/Login");

        return Page();
    }
}