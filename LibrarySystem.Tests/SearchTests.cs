using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Models;
using Xunit;

namespace LibrarySystem.Tests
{
    public class SearchTests
    {
        [Theory]
        [InlineData("Tolkien", true)]
        [InlineData("tolkien", true)]  // Case-insensitive
        [InlineData("Rowling", false)]
        public void Book_Matches_ShouldFindByAuthor(string searchTerm, bool expected)
        {
            // Arrange
            var book = new Book("123", "Sagan om ringen", "J.R.R. Tolkien", 1954);

            // Act
            var result = book.Matches(searchTerm);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Sagan", true)]
        [InlineData("ringen", true)]
        [InlineData("Harry Potter", false)]
        public void Book_Matches_ShouldFindByTitle(string searchTerm, bool expected)
        {
            // Arrange
            var book = new Book("123", "Sagan om ringen", "J.R.R. Tolkien", 1954);

            // Act
            var result = book.Matches(searchTerm);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("123", true)]
        [InlineData("456", false)]
        public void Book_Matches_ShouldFindByISBN(string searchTerm, bool expected)
        {
            // Arrange
            var book = new Book("123", "Sagan om ringen", "J.R.R. Tolkien", 1954);

            // Act
            var result = book.Matches(searchTerm);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}