using LibrarySystem.Core.Models;
using LibrarySystem.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LibrarySystem.Tests
{
    public class LibraryContextTests
    {
        private LibraryContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new LibraryContext(options);
        }

        [Fact]
        public async Task AddBook_ShouldSaveBookToDatabase()
        {
            // Arrange
            using var context = CreateContext();

            var book = new Book("123", "It", "Stephen King", 1986);

            // Act
            context.Books.Add(book);
            await context.SaveChangesAsync();

            // Assert
            Assert.Equal(1, context.Books.Count());
        }

        [Fact]
        public async Task AddMember_ShouldSaveMemberToDatabase()
        {
            // Arrange
            using var context = CreateContext();

            var member = new Member("M001", "Charlie Samuel", "Charlie.Samuel@email.com");

            // Act
            context.Members.Add(member);
            await context.SaveChangesAsync();

            // Assert
            Assert.Equal(1, context.Members.Count());
        }

        [Fact]
        public async Task Loan_ShouldMarkBookAsUnavailable()
        {
            // Arrange
            using var context = CreateContext();

            var book = new Book("123", "Dracula", "Bram Stoker", 1897);
            var member = new Member("M002", "Daniel Aldemir", "Daniel.Aldemir@email.com");

            context.Books.Add(book);
            context.Members.Add(member);
            await context.SaveChangesAsync();

            // Act
            var loan = new Loan
            {
                BookId = book.Id,
                MemberId = member.Id,
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14)
            };

            context.Loans.Add(loan);
            book.IsAvailable = false;

            await context.SaveChangesAsync();

            // Assert
            Assert.False(book.IsAvailable);
            Assert.Equal(1, context.Loans.Count());
        }

        [Fact]
        public async Task OverdueLoan_ShouldBeDetected()
        {
            // Arrange
            using var context = CreateContext();

            var book = new Book("123", "Frankenstein", "Mary Shelley", 1818);
            var member = new Member("M003", "Alexander Carrizo", "Alexander.Carrizo@gmail.com");

            context.Books.Add(book);
            context.Members.Add(member);
            await context.SaveChangesAsync();

            var loan = new Loan
            {
                BookId = book.Id,
                MemberId = member.Id,
                LoanDate = DateTime.Now.AddDays(-20),
                DueDate = DateTime.Now.AddDays(-5) // försenad
            };

            context.Loans.Add(loan);
            await context.SaveChangesAsync();

            // Act
            var isOverdue = loan.DueDate < DateTime.Now;

            // Assert
            Assert.True(isOverdue);
        }
    }
}