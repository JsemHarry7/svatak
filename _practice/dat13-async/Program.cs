using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        // TODO Mikroúloha 1: vygenerovat velké pole náhodných čísel
        const int VELIKOST = 100_000_000;
        int[] cisla = new int[VELIKOST];
        Random rnd = new Random(67);
        for (var i = 0; i < VELIKOST; i++)
        {
            cisla[i] = rnd.Next();
        }
        Console.WriteLine($"Pole obsahuje {cisla.Length:N0} čísel.");
        Console.WriteLine($"Počet LOGICKÝCH jader CPU: {Environment.ProcessorCount}");
        // TODO Mikroúloha 2: sekvenční hledání maxima + měření času
        Stopwatch sw = Stopwatch.StartNew();
        int maxSekvencne = NajdiMaxSekvencne(cisla);
        sw.Stop();
        Console.WriteLine($"Sekvenčně: max = {maxSekvencne}, čas: {sw.ElapsedMilliseconds} ms");
        // TODO Mikroúloha 3: paralelní hledání maxima (Task.Run + WhenAll)
        Stopwatch sw2 = Stopwatch.StartNew();
        int maxParalelne = await NajdiMaxParalelne(cisla);
        sw2.Stop();
        Console.WriteLine($"Paralelně: max = {maxParalelne}, čas: {sw2.ElapsedMilliseconds} ms");
        // 3.1: Použití výchozí metody.
        Stopwatch sw3 = Stopwatch.StartNew();
        int maxVychozi = cisla.Max();
        sw3.Stop();
        Console.WriteLine($"Výchozí: max = {maxVychozi}, čas: {sw3.ElapsedMilliseconds} ms");
        // TODO Mikroúloha 4: porovnání časů + zrychlení
        double zrychleni = (double)sw.ElapsedMilliseconds/sw2.ElapsedMilliseconds;
        Console.WriteLine();
        Console.WriteLine($"Zrychlení paralelní verze: {zrychleni:F2}x");
        double zrychleniOprotiVychozi = (double)sw.ElapsedMilliseconds/sw3.ElapsedMilliseconds;
        Console.WriteLine($"Zrychlení paralelní verze: {zrychleniOprotiVychozi:F2}x");
        // TODO Mikroúloha 5 (bonus): Parallel.For varianta
    }

    static int NajdiMaxSekvencne(int[] pole)
    {
        int max = pole[0];
        foreach (var x in pole)
        {
            if (x > max)
            {
                max = x;
            }
        }
        return max;
    }
    
    static async Task<int> NajdiMaxParalelne(int[] pole)
    {
        int pocetJader = Environment.ProcessorCount;
        int velikostCasti = pole.Length / pocetJader;
        Task<int>[] tasky = new Task<int>[pocetJader];
        for (int j = 0; j < pocetJader; j++)
        {
            int start = j * velikostCasti;
            int end = (j == pocetJader - 1) ? pole.Length : start + velikostCasti;

            int lokalniStart = start;
            int lokalniEnd = end;

            tasky[j] = Task.Run(() => NajdiMaxVCasti(pole, lokalniStart, lokalniEnd));
        }

        int[] dilciMaxima = await Task.WhenAll(tasky);

        int celkove = dilciMaxima[0];
        foreach (int i in dilciMaxima)
        {
            if (i > celkove)
            {
                celkove = i;
            }
        }
        return celkove;

    }
    static int NajdiMaxVCasti(int[] pole, int start, int end)
    {
        int max = pole[start];
        for (int i = start + 1; i < end; i++)
        {
            if (pole[i] > max)
            {
                max = pole[i];
            }
        }
        return max;
    }
}
