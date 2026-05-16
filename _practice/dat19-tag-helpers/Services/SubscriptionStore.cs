using dat19_tag_helpers.Models;

namespace dat19_tag_helpers.Services;

// In-memory "databáze" — drží přihlášky v paměti za běhu aplikace.
// V reálné aplikaci by místo toho byl Entity Framework + SQL Server.
public class SubscriptionStore
{
    private readonly List<Subscription> _items = new();
    private int _nextId = 1;

    public IReadOnlyList<Subscription> GetAll() => _items;

    public Subscription? GetById(int id) => _items.FirstOrDefault(s => s.Id == id);

    public void Add(Subscription subscription)
    {
        subscription.Id = _nextId++;
        _items.Add(subscription);
    }

    public bool Remove(int id)
    {
        var item = GetById(id);
        if (item == null) return false;
        return _items.Remove(item);
    }

    // Seznam nabízených kurzů — v reálu by šly z DB
    public IReadOnlyList<string> AvailableCourses { get; } = new[]
    {
        "Webové aplikace",
        "Databáze",
        "Programování v C#",
        "Frontend (HTML/CSS/JS)",
        "Algoritmy a datové struktury"
    };
}
