/*string jmeno = "Harry";
int vek = 18;
double vyska = 1.80;
bool isStudent = true;
decimal coffeePrice = 67.89m;

Console.WriteLine($"Jmenuju se {jmeno}, je mi {vek}, měřím {vyska:F2}, student: {isStudent}, kafe stojí {coffeePrice}");

int jablka = 10;
int deti = 3;
double output = (double)jablka/deti;
Console.WriteLine($"Na každé ze {deti} dětí připadá {output:F2} jablek.");

bool enteredAge = false;
while (enteredAge == false) {
    Console.WriteLine("Kolik ti je let?");
    string userAge = Console.ReadLine();
    if (int.TryParse(userAge, out int age)) {
        if (age > 0) {
            if (age < 18) {
                Console.WriteLine("Nezletilý");
            } else if (age >= 18 && age <65){
                Console.WriteLine("Dospělý");
            } else if (age >= 65){ // tady by mohl byt jen else
                Console.WriteLine("Senior");
            }
            enteredAge = true;
        } else {
            Console.WriteLine("Číslo musí být kladné");
        }
    } else {
        Console.WriteLine("Toto není platné číslo");
    }
}
*/
/*
Console.WriteLine("Do kterého čísla N mám sečíst?");
bool cisloZapsano = false;
int numN;
int secteneCislo = 0;
const int pocatecniCislo = 1;
while (!cisloZapsano) {
    string userNum = Console.ReadLine();
    if (int.TryParse(userNum, out int num) && num > 0){
        
        }
        Console.WriteLine($"Součet {pocatecniCislo} až {numN} je {secteneCislo}");
    } else {
        Console.WriteLine("Zadej kladné, celé číslo větší než 0.");
    }
}
*/
/*
Console.WriteLine("Do kterého čísla N mám sečíst?");
bool cisloZapsano = false;

while (!cisloZapsano) {
    string userNum = Console.ReadLine();
    if (int.TryParse(userNum, out int num) && num > 0) {
        Console.WriteLine($"Součet 1 až {num} je {SectiDoN(num)}, lepe {SectiLipDoN(num)}");
        cisloZapsano = true;
    } else {
        Console.WriteLine("Zadej kladné, celé číslo větší než 0.");
    }
}*/

Console.Write("Kolik ti je let? ");
string input = Console.ReadLine();

if (TryParseVek(input, out int vek)) {
    Console.WriteLine($"Tvůj věk: {vek}");
} else {
    Console.WriteLine("Neplatný věk (musí být 0-120).");
}

bool TryParseVek(string input, out int vek) {
    if (int.TryParse(input, out int num) && num > 0 && num < 120) {
        vek = num;
        return true;
    } else {
        vek = 0;
        return false;
    }
}

/*
// O(n)
int SectiDoN(int a) {
    int secteneCislo = 0;
    for (int i = 1; i <= a; i++) {
        secteneCislo += i;
    }
    return secteneCislo;
}

// O(1)
int SectiLipDoN(int a) {
    int secteneCislo = a * (a + 1) / 2;
    return secteneCislo;
}
*/