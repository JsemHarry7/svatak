using dat19_tag_helpers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dat19_tag_helpers.Pages;

public class IndexModel : PageModel
{
    private readonly SubscriptionStore _store;

    public IndexModel(SubscriptionStore store)
    {
        _store = store;
    }

    public IReadOnlyList<string> AvailableCourses => _store.AvailableCourses;

    [BindProperty]
    public string SelectedCourse { get; set; } = "";

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (string.IsNullOrEmpty(SelectedCourse))
        {
            ModelState.AddModelError(nameof(SelectedCourse), "Vyberte prosím kurz");
            return Page();
        }

        return RedirectToPage("Subscribe", new { course = SelectedCourse });
    }
}
