using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dat18_razor_pages.Pages
{
    public class ConfirmModel : PageModel
    {
        public OutputModel Output { get; set; } = new();
        public class OutputModel
        {
            public string? Jmeno { get; set; }
            public string? Prijmeni { get; set; }
            public string? Email { get; set; }
            public string? Kurz {  get; set; }

        }
        public void OnGet()
        {
            Output.Jmeno = TempData["Jmeno"] as string;
            Output.Prijmeni = TempData["Prijmeni"] as string;
            Output.Email = TempData["Email"] as string;
            Output.Kurz = TempData["Kurz"] as string;
        }
    }
}
