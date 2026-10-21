# Sharp Legend Bankomat

En konsolapplikation i C# (.NET 10) som simulerar en bankomat. Användaren loggar in med användarnamn och pinkod och kan sedan se sina konton, föra över pengar, sätta in och ta ut pengar samt öppna nya konton.

## Kom igång

1. Öppna lösningen i Visual Studio.
2. Kör projektet.
3. Logga in med någon av användarna nedan.

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
4. Användaren väljer bland menyvalen. Efter varje funktion trycker användaren Enter för att komma tillbaka till menyn. När användaren har gjort minst en transaktion visas de senaste transaktionerna efter varje menyval.
5. När användaren väljer "Logga ut" sparas alla konton till fil, menyn avslutas och programmet går tillbaka till inloggningen.

Main körs i en oändlig loop, så programmet stängs aldrig av sig självt.

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
| `Utilities` | Hjälpmetoder: visa konton, insättning, uttag (med pinkodskontroll), överföring, dold inmatning av pinkod, generisk inläsning av tal och "Tryck enter för att komma till huvudmenyn". |
| `User` | En användare med id, användarnamn och pinkod. Innehåller även listan med de 5 användarna som skapas vid programstart. |
| `Account` | Ett bankkonto med ägare (`ownerId`), kontonummer, namn, saldo och kontotyp. Ansvarar även för att läsa och skriva kontona till fil, skapa standardkonton och numrera en användares konton. |
| `Transactions` | En transaktion med konto, belopp, tecken (`+` för insättning, `-` för uttag) och tidpunkt. Visar transaktionshistoriken med `ShowTransactions`. |

### Hur klasserna hänger ihop

```
Program
  ├── Login ──> User (UserList med alla användare)
  │     └── Utilities.GetConsolePin (dold pinkod)
  └── Menu ──> Account (läser/sparar accounts.txt, numrerar konton)
         ├── Utilities (inmatning, visa konton, insättning, uttag, överföring)
         └── Transactions (skapas vid insättning, uttag och överföring, lagras i en Stack)
```

- `Login` returnerar den inloggade `User` till `Program`, som skickar den vidare till `Menu`.
- `Menu` filtrerar fram användarens konton med `ownerId == UserId`.
- `Account` och `User` är objekt. Övriga klasser innehåller logik som arbetar med dessa objekt.
- Varje gång pengar flyttas skapar `Utilities` ett `Transactions`-objekt och lägger det i den Stack som `Menu` skickar med.

### Datastrukturer

| Struktur | Används till | Varför |
|---|---|---|
| `string[]` | Menyalternativen | Fast antal alternativ som aldrig ändras, så en array räcker. |
| `List<User>` | Alla användare | Enkel att loopa igenom och söka i vid inloggning. |
| `List<Account>` | Alla konton, samt den inloggade användarens konton | Användare har olika antal konton och kan öppna nya, så listan behöver kunna växa. |
| `Dictionary<int, Account>` | Numrerar användarens konton 1, 2, 3... | Användaren väljer konto med ett enkelt nummer, och uppslagningen på nyckeln går snabbt. |
| `Stack<Transactions>` | Transaktionshistoriken under inloggningen | En Stack fungerar enligt "sist in, först ut", så den senaste transaktionen visas överst, precis som på ett kontoutdrag. |
| `enum accountType` | Kontotyper (Sparkonto, Lönekonto, Räkningskonto, Fondkonto, Depåkonto) | Begränsar kontotyperna till giltiga värden och gör det enkelt att lägga till fler. Används även för att bygga listan vid "Öppna konto". |

### Inmatning och validering

- **`GenericParse<T>`** är en generisk metod som läser in och kontrollerar tal av olika typer (`int`, `decimal` m.fl.) med samma kod. Den returnerar `true`/`false` och skickar tillbaka värdet via en `out`-parameter, på samma sätt som `int.TryParse`. Felmeddelandet kan anpassas, till exempel "Ogiltigt val" i huvudmenyn.
- **`GetConsolePin`** läser pinkoden tangent för tangent och visar `*` istället för siffrorna.
- **Belopp** kontrolleras så att de är större än noll och inte överstiger saldot.

### Datalagring

Kontona sparas i `accounts.txt`, en rad per konto i formatet:

```
ownerId;kontonummer;namn;saldo;kontotyp
```

Finns filen inte skapas den med standardvärden från `Account.AddAccountData`. Ändringar sparas när användaren loggar ut. Ta bort `accounts.txt` för att återställa alla saldon till startvärdena.

Transaktionshistoriken sparas inte till fil. Den finns bara kvar så länge användaren är inloggad.

### Extrafunktioner

- Sparar konton till fil mellan körningar
- Lockout-klocka med nedräkning efter 3 felaktiga inloggningsförsök
- Dold pinkod vid inmatning
- Insättning av pengar
- Öppna nytt konto med valfri kontotyp
- Transaktionshistorik som visar de senaste transaktionerna under inloggningen

# Reflektion
Gruppen har jobbat med täta kontakter (dagliga avstämningar), för att se till att alla har någonting att göra varje dag, samt att stämma av hur långt vi har kommit.
Vi har varit bra på att snabbt hitta lösningar på utmaningarna, men kanske svagare på att göra en robust planering och kanske våra dagliga möten väger upp den snabba uppstarten för att skapa en prototyp som vi sedan ändrade ganska tidigt när vi insåg att det blev för kompliserat arbetssätt. Där kom det fram en viss frustration över att "jobba i blindo" med koden, inte ha direkt insyn i övriga (eller ännu inte existerande) klasser, inte kunna testa sin kod. Detta speciellt i början av projektet, innan klasser eller grundläggande metoder var på plats. 

Delning av kod i GitHub har haft vissa utmaningar, där vi ibland har väntat på varandras commits / pushes, vilket har försvårat planeringen på arbetsdagen. Samt att vi har haft vissa problem med merges som sparar över arbete som någon har gjort, detta trots att vi försökt vara noga med konflikthantering och kommunicera när vi har gjort våra pushes. Vi känner att vi behöver öva mer på konflikthantering i GitHub, och/eller ett mer branch-orienterat arbetssätt där man skapar branches och stänger ner dem allt eftersom man jobbar, vilket vi tror underlättar när man gör merges till master branchen.
Projektet har varit en bra utmaning, och lärorikt, speciellt vad gäller samarbete och GitHub.

## Programmets uppbyggnad
Programmet är byggt kring grundtanken att menyn är den centrala delen av programmet. När användaren väl är inloggad så finns konton tillgängliga i programmets minne så att användaren kan utföra önskade funktioner, så som att ta ut pengar, överföra pengar, eller öppna nya konton. Menyn innehåller en stor del av kontrollerna vi gör på användarens inmatningar; vi använder en Generic metod för att kunna parse flera olika sorts data typer med en och samma metod.

Först när en godkänd inmatning görs kan en funktion köras, som finns samlade i en klass kallad "Utilities" där alla uträkningar och transaktioner sker. Även här görs kontroller på all inmatning så att inget ogiltligt värde kan användas; tex görs en kontroll på att det finns tillräckligt med pengar på kontot innan pengarna dras. När en funktion har körts klart, så kommer användaren tillbaka till menyn, och får välja valfri funktion igen. När användaren loggar ut, så sparas alla ändringar till fil.

Vi har klasserna User och Account som vardera innehåller Constructor och tillhörande metoder. I Account använder vi Constructor overloading för att tillgodose att kontona kan skapas antingen via en inbyggd metod, eller från lokal fil. Vi använder även en Enum för att benämna de olika kontotyperna som banken har. Vi har valt att använda datastrukturen Array till att skriva ut meny-alternativen, samt till sparfunktionen, där varje konto representeras av en string, som sedan blir uppdelad i sina korrekta variabler, så att programmet kan använda datan. Vi använder två olika List där vi håller isär alla konton i "databasen" samt den inloggade användarens konton. Vidare har vi en Dictionary som låter oss numrera och ger tillgång till kontona med ett index som vi själva valt. Sist men inte minst har vi en Stack som lagrar alla giltiga transaktioner och visar dem för användaren.

Funktionalitet som vi har övervägt, men valt bort av olika anledningar är ett system för att skapa nya användare, att flytta pengar mellan användares konton, och kontohistorik som sparas till fil. Vi har även tänkt på att använda oss av Class Library, men det skulle troligen kräva att stora delar av programmet skulle behövas skriva om från grunden.

## Arbetsfördelning

### Ammar

- User Class
- Code review
- Testing

### Lukas

- GitHub master
- Login Class
- Lockout timer feature
- Menu Class
- Documentation

### Marco

- Utilities Class
- Transactions features
- Pin-code feature

### Mattias

- Account Class
- Account persistence feature
- Transaction history feature
- Add account feature
