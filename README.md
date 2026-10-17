# Sharp Legend Bank - Bankomat

En konsolapplikation i C# (.NET 10) som simulerar en bankomat. Användaren loggar in med användarnamn och pinkod och kan sedan se sina konton, föra över pengar, sätta in och ta ut pengar samt öppna nya konton.

## Kom igång

1. Öppna lösningen i Visual Studio.
2. Kör projektet (F5).
3. Logga in med någon av testanvändarna nedan.

| Användarnamn | Pinkod |
|---|---|
| Ammar | 1234 |
| Mattias | 1234 |
| Lukas | 1234 |
| Marco | 1234 |
| Kalle | 1234 |

## Systemöversikt

### Programflöde

1. **Program (Main)** visar välkomstmeddelandet och anropar Login.
2. **Login** frågar efter användarnamn och pinkod (pinkoden visas som `*`) och returnerar en `User` om inloggningen lyckas, annars `null`. Efter 3 felaktiga försök visas en nedräkning på 3 minuter (lockout) innan användaren får försöka igen.
3. Om en `User` returneras öppnas **Menu**. Kontona läses in från `accounts.txt` och filtreras fram för den inloggade användaren.
4. Användaren väljer bland menyvalen. Efter varje funktion trycker användaren Enter för att komma tillbaka till menyn.
5. När användaren väljer "Logga ut" sparas alla konton till fil, menyn avslutas och programmet går tillbaka till inloggningen.

Main körs i en oändlig loop, så programmet stängs aldrig av sig självt, precis som en riktig bankomat.

### Menyval

1. Se dina konton och saldo
2. Överföring mellan konton
3. Sätt in pengar
4. Ta ut pengar (kräver pinkod för att bekräfta, max 3 försök)
5. Öppna konto (välj namn och kontotyp, startsaldo 0 kr)
6. Logga ut

### Klasser

| Klass | Ansvar |
|---|---|
| `Program` | Startpunkt. Styr ordningen mellan inloggning och meny. |
| `Login` | Kontrollerar inloggning mot användarlistan i `User`, räknar felaktiga försök och visar lockout-klockan. |
| `Menu` | Visar menyval, läser in användarens val och anropar rätt funktion. Laddar kontona vid inloggning och sparar dem vid utloggning. |
| `Utilities` | Hjälpmetoder: visa konton, insättning, uttag (med pinkodskontroll), överföring, dold inmatning av pinkod, generisk inläsning av tal och "tryck Enter"-paus. |
| `User` | En användare med id, användarnamn och pinkod. Innehåller även listan med de 5 användarna som skapas vid programstart. |
| `Account` | Ett bankkonto med ägare (`ownerId`), kontonummer, namn, saldo och kontotyp. Ansvarar även för att läsa och skriva kontona till fil, skapa standardkonton och numrera en användares konton. |

### Hur klasserna hänger ihop

```
Program
  ├── Login ──> User (UserList med alla användare)
  │     └── Utilities.GetConsolePassword (dold pinkod)
  └── Menu ──> Account (läser/sparar accounts.txt, numrerar konton)
         └── Utilities (inmatning, visa konton, insättning, uttag, överföring)
```

- `Login` returnerar den inloggade `User` till `Program`, som skickar den vidare till `Menu`.
- `Menu` filtrerar fram användarens konton med `ownerId == UserId`.
- `Account` och `User` är objekt. Övriga klasser innehåller logik som arbetar med dessa objekt.

### Datastrukturer

| Struktur | Används till | Varför |
|---|---|---|
| `string[]` | Menyalternativen | Fast antal alternativ som aldrig ändras, så en array räcker. |
| `List<User>` | Alla användare | Enkel att loopa igenom och söka i vid inloggning. |
| `List<Account>` | Alla konton, samt den inloggade användarens konton | Användare har olika antal konton och kan öppna nya, så listan behöver kunna växa. |
| `Dictionary<int, Account>` | Numrerar användarens konton 1, 2, 3... | Användaren väljer konto med ett enkelt nummer, och uppslagningen på nyckeln går snabbt. |
| `enum accountType` | Kontotyper (Sparkonto, Lönekonto, Räkningskonto, Fondkonto, Depåkonto) | Begränsar kontotyperna till giltiga värden och gör det enkelt att lägga till fler. Används även för att bygga listan vid "Öppna konto". |
| `StringBuilder` | Bygger pinkoden tecken för tecken | Effektivt sätt att bygga en sträng när varje tangent läses in separat. |

### Inmatning och validering

- **`GenericParse<T>`** är en generisk metod som läser in och kontrollerar tal av olika typer (`int`, `decimal` m.fl.) med samma kod. Den returnerar `true`/`false` och skickar tillbaka värdet via en `out`-parameter, på samma sätt som `int.TryParse`. Felmeddelandet kan anpassas, till exempel "Ogiltigt val" i huvudmenyn.
- **`GetConsolePassword`** läser pinkoden tangent för tangent och visar `*` istället för siffrorna.
- **Belopp** kontrolleras så att de är större än noll och inte överstiger saldot.

### Datalagring

Kontona sparas i `accounts.txt`, en rad per konto i formatet:

```
ownerId;kontonummer;namn;saldo;kontotyp
```

Finns filen inte skapas den med standardvärden från `Account.AddAccountData`. Ändringar sparas när användaren loggar ut. Ta bort `accounts.txt` för att återställa alla saldon till startvärdena.

### Extrafunktioner

- Sparar konton till fil mellan körningar
- Lockout-klocka med nedräkning efter 3 felaktiga inloggningsförsök
- Dold pinkod vid inmatning
- Insättning av pengar
- Öppna nytt konto med valfri kontotyp

## Reflektion

_TODO: Motivera hur ni byggt upp programmet, vilka andra lösningar ni övervägt, vad ni skulle göra annorlunda och vilka förbättringar ni ser men inte hunnit göra. Reflektera särskilt över valet av datastrukturer och klassindelningen._

## Arbetsfördelning

_TODO: Beskriv kort hur ni fördelat arbetet i gruppen._
