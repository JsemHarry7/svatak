using dat19_tag_helpers.Models;
using dat19_tag_helpers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dat19_tag_helpers.Pages;

public class DetailModel : PageModel
{
    private readonly SubscriptionStore _store;

    public DetailModel(SubscriptionStore store)
    {
        _store = store;
    }

    public Subscription? Subscription { get; private set; }

    public IActionResult OnGet(int id)
    {
        Subscription = _store.GetById(id);
        if (Subscription == null)
        {
            return NotFound();
        }
        return Page();
    }
}
