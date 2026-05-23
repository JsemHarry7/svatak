using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace dat18_razor_pages.Pages
{
    public class RegisterModel : PageModel
    {
        public List<string> Kurzy { get; set; } = new();

        [BindProperty(SupportsGet = true), Required]
        public string? Jmeno { get; set; } = "";

        [BindProperty(SupportsGet = true), Required]
        public string? Prijmeni { get; set; } = "";

        [BindProperty(SupportsGet = true), Required]
        public string? Kurz { get; set; } = "";

        [BindProperty(SupportsGet = true), Required]
        public string? Email { get; set; }
        public void OnGet()
        {
            Kurzy = new List<string>
            {
                "Plavání", "Cyklistika", "Bìh"
            };
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Kurzy = new List<string>
                {
                    "Plavání", "Cyklistika", "Bìh"
                };
                return Page();
            }

            TempData["Jmeno"] = Jmeno;
            TempData["Prijmeni"] = Prijmeni;
            TempData["Email"] = Email;
            TempData["Kurz"] = Kurz;

            return RedirectToPage("Confirm");
        }
    }
}
