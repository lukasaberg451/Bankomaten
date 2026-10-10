<img width="1220" height="620" alt="Bankomaten" src="https://github.com/user-attachments/assets/304aef09-c6d8-451e-90e5-3971e2eb6d88" />

## Systemöversikt

### Programflöde

1. **Program (Main)** visar välkomstmeddelandet och anropar Login.
2. **Login** frågar efter användarnamn och pinkod och returnerar en `User` om inloggningen lyckas, annars `null`. Efter 3 felaktiga försök visas en nedräkning (lockout) innan användaren får försöka igen.
3. Om en `User` returneras öppnas **Menu**.
4. När användaren väljer "Logga ut" sparas kontona till fil, menyn avslutas och programmet går tillbaka till inloggningen.

Main körs i en oändlig loop, så programmet stängs aldrig av sig självt, precis som en riktig bankomat.

### Klasser

| Klass | Ansvar |
|---|---|
| `Program` | Startpunkt. Styr ordningen mellan inloggning och meny. |
| `Login` | Kontrollerar inloggning mot användarlistan och hanterar lockout-klockan. |
| `Menu` | Visar menyval, läser in användarens val och anropar rätt funktion. Läser och sparar konton till `accounts.txt`. |
| `Utilities` | Hjälpmetoder för inmatning (`IntParse`, `DecimalParse`), visa konton, insättning, uttag och överföring. |
| `User` | En användare med id, användarnamn och pinkod. Innehåller även listan med de 5 användarna som skapas vid programstart. |
| `Account` | Ett bankkonto med id, ägare (`ownerId`), kontonummer, namn, saldo och kontotyp. Innehåller även standardkontona som skapas första gången programmet körs. |

### Hur klasserna hänger ihop

```
Program
  ├── Login ──> User (användarlistan)
  └── Menu ──> Account (alla konton, filtreras på ownerId)
         └── Utilities (inmatning, insättning, uttag, överföring)
```

### Datastrukturer

| Struktur | Används till | Varför |
|---|---|---|
| `string[]` | Menyalternativen | Fast antal alternativ som aldrig ändras, så en array räcker. |
| `List<User>` | Alla användare | Enkel att loopa igenom och söka i vid inloggning. |
| `List<Account>` | Alla konton, samt den inloggade användarens konton | Användare har olika antal konton, och en List kan växa efter behov. |
| `Dictionary<int, Account>` | Numrerar användarens konton 1, 2, 3... | Snabb uppslagning på det nummer användaren skriver in. |
| `enum accountType` | Kontotyper (Sparkonto, Lönekonto, Depåkonto) | Begränsar kontotyperna till giltiga värden och gör det enkelt att lägga till fler. |

### Datalagring

Kontona sparas i `accounts.txt`, en rad per konto med fälten separerade med `;`. Finns filen inte skapas den med standardvärden från `Account`.

# Bankomaten
En bankomat-app där användaren kan se konton, saldon, samt överföra pengar mellan konton. <br>
Vi skriver kommentarer på Svenska <br>
Variabler ska ha beskrivande namn, och skrivs i camelCase

# Arbetsfördelning
## Ammar
class User + constructor
### Fields
UserName <br>
Password <br>
list of accounts Accounts <br>
### Methods
ShowUserInfo() <br>
AddUser()

## Lukas
GitHub master branch<br>
class Main (just run menu)<br>
class Login / Main Menu

## Marco
class Bank <br>
### Methods
ShowAccounts <br>
ShowBalance <br>
TransferMoney 

## Mattias
class Account + constructor
### Fields
Id <br>
AccountNumber <br>
User <br>
AccountName <br>
AccountType <br>
Balance

# Data Structures
array <br>
list - <br>
stack - transaction history
# Övriga Anteckningar
"nice to have features" <br>
file handling, Class Library
