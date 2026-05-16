using dat19_tag_helpers.Models;
using dat19_tag_helpers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dat19_tag_helpers.Pages;

public class ListModel : PageModel
{
    private readonly SubscriptionStore _store;

    public ListModel(SubscriptionStore store)
    {
        _store = store;
    }

    public IReadOnlyList<Subscription> Items { get; private set; } = Array.Empty<Subscription>();

    public void OnGet()
    {
        Items = _store.GetAll();
    }

    public IActionResult OnPostDelete(int id)
    {
        _store.Remove(id);
        return RedirectToPage();
    }

    public IActionResult OnPostDetail(int id)
    {
        return RedirectToPage("Detail", new { id });
    }
}
