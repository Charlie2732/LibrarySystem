Bibliotekssystem

Beskrivning
Ett konsolbaserat bibliotekssystem byggt i C# som hanterar böcker, medlemmar och utlåning.

Projektstruktur

Klasser (LibrarySystem/Models)
Book.cs** - Representerar en bok med ISBN, titel, författare och utgivningsår
Member.cs** - Representerar en biblioteksmedlem
Loan.cs** - Hanterar utlåning av böcker till medlemmar
Library.cs** - Huvudklass som samlar alla böcker, medlemmar och lån

Interface (LibrarySystem/Interfaces)
ISearchable.cs - Interface för sökfunktionalitet

Tester (LibrarySystem.Tests)
BookTests.cs 3 tester för Book-klassen
LoanTests.cs 4 tester för Loan-klassen
SearchTests.cs 8 tester för sökfunktionalitet
LibraryStatisticsTests.cs 5 tester för statistik och algoritmer

Totalt 20 enhetstester

Funktionalitet

Klasser och inkapsling
Book Member och Loan klasser med korrekt inkapsling
Properties med lämpliga åtkomstnivåer
ISBN kan endast sättas vid skapande av bok

Komposition
Library-klassen använder komposition med:
List<Book> för böcker
List<Member> för medlemmar
List<Loan> för lån

Interface och polymorfism
ISearchable interface implementerat i Book-klassen
Enhetlig sökning via Matches()-metoden

Algoritmer
Sökfunktion Sök böcker på titel, författare eller ISBN (case-insensitive)
Sortering Alfabetisk sortering av böcker, sortering efter utgivningsår
Statistik
Totalt antal böcker
Antal utlånade böcker
Mest aktiva låntagaren

Enhetstester
20 enhetstester med xUnit
(Arrange, Act, Assert)
Använder [Theory] och [InlineData]

Designbeslut
Jag valde komposition eftersom
Library-klassen samlar all funktionalitet på ett ställe
Enklare att underhålla och testa