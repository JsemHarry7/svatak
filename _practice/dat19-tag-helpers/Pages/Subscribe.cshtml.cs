using dat19_tag_helpers.Models;
using dat19_tag_helpers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.ComponentModel.DataAnnotations;

namespace dat19_tag_helpers.Pages;

public class SubscribeModel : PageModel
{
    private readonly SubscriptionStore _store;

    public SubscribeModel(SubscriptionStore store)
    {
        _store = store;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? Course { get; set; }

    public void OnGet()
    {
        if (!string.IsNullOrEmpty(Course))
        {
            Input.Course = Course;
        }
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _store.Add(new Subscription
        {
            Name = Input.Name,
            Email = Input.Email,
            Course = Input.Course
        });

        return RedirectToPage("List");
    }

    public class InputModel
    {
        // TODO: Doplnit validační atributy
        // - Name musí být povinný (Required)
        // - Email musí být povinný a musí být validní email
        // - Course musí být povinný
        [Required]
        public string Name { get; set; } = "";
        [Required, EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        public string Course { get; set; } = "";
    }
}
