using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Models;
using Xunit;

namespace LibrarySystem.Tests
{
    public class LibraryStatisticsTests
    {
        [Fact]
        public void GetTotalBooks_ShouldReturnCorrectCount()
        {
            // Arrange
            var library = new Library();
            library.Books.Add(new Book("1", "Bok 1", "Författare 1", 2020));
            library.Books.Add(new Book("2", "Bok 2", "Författare 2", 2021));
            library.Books.Add(new Book("3", "Bok 3", "Författare 3", 2022));

            // Act
            var count = library.GetTotalBooks();

            // Assert
            Assert.Equal(3, count);
        }

        [Fact]
        public void GetBorrowedBooksCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var library = new Library();
            var book1 = new Book("1", "Bok 1", "Författare 1", 2020);
            var book2 = new Book("2", "Bok 2", "Författare 2", 2021);
            var member = new Member("M001", "Test Person", "test@test.com");

            library.Books.Add(book1);
            library.Books.Add(book2);
            library.Members.Add(member);

            // Låna en bok
            var loan = new Loan(book1, member, DateTime.Now, DateTime.Now.AddDays(14));
            library.Loans.Add(loan);
            member.BorrowedBooks.Add(loan);

            // Act
            var borrowedCount = library.GetBorrowedBooksCount();

            // Assert
            Assert.Equal(1, borrowedCount);  // Bara 1 bok är utlånad
        }

        [Fact]
        public void GetMostActiveBorrower_ShouldReturnMemberWithMostLoans()
        {
            // Arrange
            var library = new Library();
            var book1 = new Book("1", "Bok 1", "Författare 1", 2020);
            var book2 = new Book("2", "Bok 2", "Författare 2", 2021);
            var book3 = new Book("3", "Bok 3", "Författare 3", 2022);

            var member1 = new Member("M001", "Anna", "anna@test.com");
            var member2 = new Member("M002", "Bertil", "bertil@test.com");

            library.Books.Add(book1);
            library.Books.Add(book2);
            library.Books.Add(book3);
            library.Members.Add(member1);
            library.Members.Add(member2);

            // Anna lånar 2 böcker
            var loan1 = new Loan(book1, member1, DateTime.Now, DateTime.Now.AddDays(14));
            var loan2 = new Loan(book2, member1, DateTime.Now, DateTime.Now.AddDays(14));
            member1.BorrowedBooks.Add(loan1);
            member1.BorrowedBooks.Add(loan2);

            // Bertil lånar 1 bok
            var loan3 = new Loan(book3, member2, DateTime.Now, DateTime.Now.AddDays(14));
            member2.BorrowedBooks.Add(loan3);

            // Act
            var mostActive = library.GetMostActiveBorrower();

            // Assert
            Assert.Equal("Anna", mostActive.Name);  // Anna har flest lån
        }

        [Fact]
        public void SortBooksByTitle_ShouldReturnAlphabeticalOrder()
        {
            // Arrange
            var library = new Library();
            library.Books.Add(new Book("1", "Zebra", "Författare 1", 2020));
            library.Books.Add(new Book("2", "Apan", "Författare 2", 2021));
            library.Books.Add(new Book("3", "Björn", "Författare 3", 2022));

            // Act
            var sortedBooks = library.SortBooksByTitle();

            // Assert
            Assert.Equal("Apan", sortedBooks[0].Title);
            Assert.Equal("Björn", sortedBooks[1].Title);
            Assert.Equal("Zebra", sortedBooks[2].Title);
        }

        [Fact]
        public void SortBooksByYear_ShouldReturnChronologicalOrder()
        {
            // Arrange
            var library = new Library();
            library.Books.Add(new Book("1", "Bok 1", "Författare 1", 2022));
            library.Books.Add(new Book("2", "Bok 2", "Författare 2", 2020));
            library.Books.Add(new Book("3", "Bok 3", "Författare 3", 2021));

            // Act
            var sortedBooks = library.SortBooksByYear();

            // Assert
            Assert.Equal(2020, sortedBooks[0].PublishedYear);
            Assert.Equal(2021, sortedBooks[1].PublishedYear);
            Assert.Equal(2022, sortedBooks[2].PublishedYear);
        }
    }
}