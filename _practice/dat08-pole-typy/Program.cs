int[] pole = new int[10];
int sumPole = 0;
Console.Write("Pole:");
for (int x = 0; x < pole.Length; x++)
{
    pole[x] = x+1;
    Console.Write($" {pole[x]}");
    sumPole += pole[x];
}
Console.WriteLine();
Console.WriteLine($"Součet: {sumPole}");
Console.WriteLine($"Průměr: {(double)sumPole/pole.Length}");

int[,] matice = new int[3,3];
int count = 1;
int sumDiagonala = 0;
for (int x = 0; x < 3; x++)
{
    for (int y = 0; y < 3; y++)
    {
        matice[x,y] = count;
        count++;
        Console.Write($"{matice[x,y]} ");
    }
    sumDiagonala += matice[x,x];
    Console.WriteLine();
}
Console.WriteLine($"Hlavní diagonála: {sumDiagonala}");

int[][] trojuhelnik = new int[5][];
int countTrojuhelnik = 1;
for (int x = 0; x < 5; x++)
{
    trojuhelnik[x] = new int[x+1];
    for (int y = 0; y <= x; y++)
    {
        trojuhelnik[x][y] = countTrojuhelnik;
        Console.Write($"{trojuhelnik[x][y]} ");
        countTrojuhelnik++;
    }
    Console.WriteLine();
}
Den dnes = Den.Středa;
switch (dnes)
{
    case Den.Pondělí:
    case Den.Úterý:
    case Den.Středa:
    case Den.Čtvrtek:
    case Den.Pátek:
        Console.WriteLine($"{dnes}: pracovní den (hodnota {(int)dnes})");
        break;
    case Den.Sobota:
    case Den.Neděle:
        Console.WriteLine($"{dnes}: víkend. (hodnota {(int)dnes})");
        break;
}
enum Den {Pondělí = 1, Úterý, Středa, Čtvrtek, Pátek, Sobota, Neděle}