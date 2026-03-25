using LibrarySystem.Core.Interfaces;
using LibrarySystem.Core.Models;
using Xunit;

namespace LibrarySystem.Tests
{
    public class BookTests
    {
        [Fact]
        public void Constructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange & Act
            var book = new Book("978-91-0-012345-6", "Testbok", "Testförfattare", 2024);

            // Assert
            Assert.Equal("978-91-0-012345-6", book.ISBN);
            Assert.Equal("Testbok", book.Title);
            Assert.Equal("Testförfattare", book.Author);
            Assert.Equal(2024, book.PublishedYear);
            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void IsAvailable_ShouldBeTrueForNewBook()
        {
            // Arrange & Act
            var book = new Book("123", "Test", "Author", 2024);

            // Assert
            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void GetInfo_ShouldReturnFormattedString()
        {
            // Arrange
            var book = new Book("123", "Sagan om ringen", "Tolkien", 1954);

            // Act
            var info = book.GetInfo();

            // Assert
            Assert.Contains("Sagan om ringen", info);
            Assert.Contains("Tolkien", info);
            Assert.Contains("1954", info);
            Assert.Contains("Tillgänglig", info);
        }
    }
}