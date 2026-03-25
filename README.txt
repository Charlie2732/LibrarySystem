# Bibliotekssystem

## Beskrivning
Ett bibliotekssystem byggt i C# med Entity Framework Core och Blazor Server. Systemet hanterar böcker, medlemmar och utlåning med ett webbgränssnitt.

## Hur man kör projektet

### Förutsättningar
- .NET 8.0 SDK
- Entity Framework Core Tools (`dotnet tool install --global dotnet-ef`)

### Starta applikationen

cd LibrarySystem.Blazor
dotnet run

Öppna webbläsaren på den port som visas i terminalen. Databasen skapas och fylls med exempeldata automatiskt vid första körning.

### Köra tester

dotnet test

## Projektstruktur

LibrarySystem/
├── LibrarySystem.Core/          # Modeller och interfaces
│   ├── Book.cs
│   ├── Loan.cs
│   ├── Member.cs
│   └── Interfaces/
│       └── ISearchable.cs
├── LibrarySystem.Data/          # EF Core och dataåtkomst
│   ├── LibraryContext.cs
│   └── Migrations/
├── LibrarySystem.Blazor/        # Blazor Server-projekt
│   ├── Program.cs
│   └── Components/Pages/
│       ├── Home.razor
│       ├── Books.razor
│       ├── BookDetails.razor
│       ├── Members.razor
│       └── Loans.razor
└── LibrarySystem.Tests/         # Enhetstester
    ├── BookTests.cs
    ├── LibraryContextTests.cs
    ├── LoanTests.cs
    └── SearchTests.cs

## Databasmodell

SQLite-databas via Entity Framework Core.

### Books
| Kolumn        | Typ     | Beskrivning             |
|---------------|---------|-------------------------|
| Id            | INTEGER | Primärnyckel (auto)     |
| ISBN          | TEXT    | ISBN-nummer (required)  |
| Title         | TEXT    | Boktitel (required)     |
| Author        | TEXT    | Författare (required)   |
| PublishedYear | INTEGER | Utgivningsår            |
| IsAvailable   | INTEGER | Tillgänglig (bool)      |

### Members
| Kolumn      | Typ      | Beskrivning             |
|-------------|----------|-------------------------|
| Id          | INTEGER  | Primärnyckel (auto)     |
| MemberId    | TEXT     | Medlems-ID (required)   |
| Name        | TEXT     | Namn (required)         |
| Email       | TEXT     | Email (required)        |
| MemberSince | DATETIME | Registreringsdatum      |

### Loans
| Kolumn     | Typ      | Beskrivning                       |
|------------|----------|-----------------------------------|
| Id         | INTEGER  | Primärnyckel (auto)               |
| BookId     | INTEGER  | FK till Books                     |
| MemberId   | INTEGER  | FK till Members                   |
| LoanDate   | DATETIME | Lånedatum                         |
| DueDate    | DATETIME | Förfallodatum                     |
| ReturnDate | DATETIME | Returdatum (null = ej returnerad) |

### Relationer
- Book -> Loans (1:N)
- Member -> Loans (1:N)
- Loan tillhör en Book och en Member

## Blazor-sidor

- / – Översikt med statistik (antal böcker, medlemmar, aktiva lån, försenade lån)
- /books – Boklista med sök och sortering. Går att ta bort böcker som inte är utlånade
- /books/{id} – Detaljsida för en bok, visar eventuellt aktivt lån
- /members – Lista på alla medlemmar
- /loans – Skapa nya lån, se alla lån, returnera böcker. Försenade lån markeras

## Tester

14 enhetstester med xUnit:
- BookTests – konstruktor, tillgänglighet, formaterad info
- LibraryContextTests – spara bok/medlem, skapa lån, försenade lån (InMemory-databas)
- LoanTests – IsOverdue, IsReturned
- SearchTests – sökning på titel, författare, ISBN (Theory/InlineData)

## Teknik
- Entity Framework Core 8.0 med SQLite
- Blazor Server
- xUnit med InMemory-databas
- Seed data i Program.cs
