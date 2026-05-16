using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace dat18_warmup_contact.Pages
{
    public class ContactModel : PageModel
    {
        // 1. Vlastnost (property)
        public List<string> Subjects { get; set; } = new();
        // 2. naplní property pøi naètení
        [BindProperty]
        public InputModel Input { get; set; } = new();
        public class InputModel
        {
            [Required(ErrorMessage = "Jméno je povinné")]
            public string Name { get; set; } = "";
            [Required, EmailAddress]
            public string Email { get; set; } = "";
            [Required]
            public string Subject { get; set; } = "";
            [Required, StringLength(500, MinimumLength = 10)]
            public string Message { get; set; } = "";
        }
        public void OnGet()
        {
            Subjects = new List<string>
            {
                "Obecný dotaz", "Reklamace", "Technická podpora"
            };
        }
        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                Subjects = new List<string>
                {
                    "Obecný dotaz", "Reklamace", "Technická podpora"
                };
                return Page();
            }

            TempData["Name"] = Input.Name;
            TempData["Email"] = Input.Email;
            TempData["Subject"] = Input.Subject;
            TempData["Message"] = Input.Message;

            return RedirectToPage("Thanks");
        }
    }
}
