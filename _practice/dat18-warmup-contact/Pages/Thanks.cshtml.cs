using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dat18_warmup_contact.Pages
{
    public class ThanksModel : PageModel
    {
        public OutputModel Output { get; set; } = new();
        public class OutputModel
        {
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? Subject { get; set; }
            public string? Message { get; set; }
        }
        public void OnGet()
        {
            Output.Name = (string?)(TempData["Name"]);
            Output.Email = (string?)(TempData["Email"]);
            Output.Subject = (string?)(TempData["Subject"]);
            Output.Message = (string?)(TempData["Message"]);
        }
    }
}
