using System.ComponentModel;

Dictionary<string, int> menu = new Dictionary<string, int>()
{
    {"Burger", 150},
    {"Cola", 40},
    {"Hranolky", 150}
};

Queue<string> objednavky = new Queue<string>();
objednavky.Enqueue("Cola");
objednavky.Enqueue("Burger");
objednavky.Enqueue("Burger");
objednavky.Enqueue("Voda");

Stack<int> uctenky = new Stack<int>();

while (objednavky.Count > 0)
{
    string objednavka = objednavky.Dequeue();
    if (menu.TryGetValue(objednavka, out int cena))
    {
        Console.WriteLine($"Připravuji {objednavka} za {cena} Kč");
        uctenky.Push(cena);
    }
    else
    {
        Console.WriteLine($"Nemáme {objednavka}");
    }
}

Console.WriteLine($"Poslední účtenka byla na: {uctenky.Peek()}");
int trzba = 0;
foreach (int cena in uctenky)
{
    trzba += cena;
}

Console.WriteLine($"Celkova tržba: {trzba}");