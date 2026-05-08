List<int> cisla = new List<int> { 12, 7, 138, 25, 90, 3, 256, 8 };

VypisNadpis("Sudá čísla a součty jejich cifer");
Zpracuj(cisla,x => x%2 == 0, SoucetCifer);

VypisNadpis("Čísla větší než 50 a jejich dvojnásobky");
Zpracuj(cisla,x => x > 50, x => x*2);

int SoucetCifer (int n) {
    if (n < 10){
        return n;
    }
    return n % 10 + SoucetCifer(n / 10); 
}

void Zpracuj(List<int> data, Func<int, bool> filtr, Func<int, int> operace) {
    List<int> filtered = new List<int>();
    foreach (int i in data) {
        if (filtr(i)) {
        filtered.Add(i);
        }
    }
    foreach (int i in filtered) {
        Console.WriteLine($"{i} -> {operace(i)}");
    }
}

void VypisNadpis(string text) {
    Console.WriteLine($"====== {text} ======");
}