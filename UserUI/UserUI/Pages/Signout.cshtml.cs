using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UserUI.Pages
{
    public class SignoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}
