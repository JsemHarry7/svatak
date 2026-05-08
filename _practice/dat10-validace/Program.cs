var seznam = new List<int>();
PridejValidni(seznam, "5", "abc", "12", "7x", "-3", "100");
Console.WriteLine($"Validních čísel: {seznam.Count}");

int kladnych = Spocitej(seznam, x => x > 0);
int sudych = Spocitej(seznam, x => x % 2 == 0);
Console.WriteLine($"Kladných: {kladnych}, sudých: {sudych}");

int Spocitej(List<int> seznam, Func<int, bool> filtr)
{
    int count = 0;
    foreach (var x in seznam)
    {
        if (filtr(x))
        {
            count++;
        }
    }
    return count;
}

bool TryNacti(string vstup, out int cislo)
{
    if (int.TryParse(vstup, out int num))
    {
        cislo = num;
        return true;
    }
    cislo = 0;
    return false;
}

void PridejValidni(List<int> seznam, params string [] vstupy)
{
    foreach (var x in vstupy)
    {
        if (TryNacti(x, out int cislo))
        {
            seznam.Add(cislo);
        }
    }
}