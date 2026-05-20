using System;
using System.IO;

class Program
{
    static void Main()
    {
        // TODO Mikroúloha 1: rekurzivní procházení Data/
        List<string> soubory = new List<string>(Directory.GetFiles("Data", "*", SearchOption.AllDirectories).ToList());
        Console.WriteLine("== Soubory ==");
        foreach (var x in soubory)
        {
            Console.WriteLine(x);
        }
        List<string> slozky = new List<string>(Directory.GetDirectories("Data", "*", SearchOption.AllDirectories).ToList());
        Console.WriteLine("== Složky ==");
        foreach (var x in slozky)
        {
            Console.WriteLine(x);
        }
        // TODO Mikroúloha 2: statistiky (počty + velikost)
        Console.WriteLine($"Počet souborů: {soubory.Count}");
        Console.WriteLine($"Počet složek: {slozky.Count}");
        long velikost = 0;
        foreach (var x in soubory)
        {
            velikost += new FileInfo(x).Length;
        }
        Console.WriteLine($"Celková velikost: {velikost} B");
        // TODO Mikroúloha 3: vypsat obsah textových souborů
        var txtSoubory = Directory.GetFiles("Data", "*.txt", SearchOption.AllDirectories);
        foreach (var x in txtSoubory)
        {
            Console.WriteLine($"--- {Path.GetFileName(x)} ---");
            Console.WriteLine(File.ReadAllText(x));
        }
        // TODO Mikroúloha 4: zapsat jména do Output/jmena.txt
        if (Directory.Exists("Output"))
        {
            Directory.Delete("Output", recursive: true);
        }
        Directory.CreateDirectory("Output");
        List<string> jmena = new List<string>();
        foreach (var x in txtSoubory)
        {
            jmena.AddRange(File.ReadAllLines(x));
        }
        File.WriteAllLines("Output/jmena.txt", jmena);
        // TODO Mikroúloha 5: otevřít a vypsat Output/jmena.txt
        Console.WriteLine(File.ReadAllText("Output/jmena.txt"));
        // TODO Mikroúloha 6: vytvořit Output/byte.bin s 10 random bajty
        byte[] buffer = new byte[10];
        Random rnd = new Random();
        rnd.NextBytes(buffer);
        File.WriteAllBytes("Output/byte.bin", buffer);
        // TODO Mikroúloha 7: přečíst a vypsat byte.bin
        var bytes = File.ReadAllBytes("Output/byte.bin");
        foreach (var x in bytes)
        {
            Console.Write($"{x} ");
        }
    }
}
