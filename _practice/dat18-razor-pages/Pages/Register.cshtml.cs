using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace dat18_razor_pages.Pages
{
    public class RegisterModel : PageModel
    {
        public List<string> Kurzy { get; set; } = new();
        [BindProperty]
        public InputModel Input { get; set; } = new();
        public class InputModel
        {
            [Required]
            public string Jmeno { get; set; } = "";
            [Required]
            public string Prijmeni { get; set; } = "";
            [Required]
            public string Kurz { get; set; } = "";
        }
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

            TempData["Jmeno"] = Input.Jmeno;
            TempData["Prijmeni"] = Input.Prijmeni;
            TempData["Email"] = Email;
            TempData["Kurz"] = Input.Kurz;

            return RedirectToPage("Confirm");
        }
    }
}
