using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Interfaces;
/*Properties: ISBN (string), Title (string), Author (string),
PublishedYear (int), IsAvailable (bool)
Konstruktor som tar obligatoriska parametrar
ISBN ska endast kunna sättas vid skapande
Metod GetInfo() som returnerar formaterad bokinformation*/

namespace LibrarySystem.Models
{
    public class Book : ISearchable
    {
        // Detta är properties
        public string ISBN { get; } // Kan läsas men inte ändras
        public string Title { get; set; } // get och set gör att den kan läsas och ändras
        public string Author { get; set; }
        public int PublishedYear { get; set; }
        public bool IsAvailable { get; set; }

        // Detta är en konstruktor
        public Book(string isbn, string title, string author, int publishedYear)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            PublishedYear = publishedYear;
            IsAvailable = true;
        }

        // Detta är en metod som returnerar
        public string GetInfo()
        {
            string availability = IsAvailable ? "Tillgänglig" : "Utlånad";
            return $"\"{Title}\" av {Author} ({PublishedYear}) - ISBN: {ISBN} - {availability}";
        }
        // Implementerar ISearchable
        public bool Matches(string searchTerm)
        {
            // Söker i titel, författare och ISBN (case-insensitive)
            return Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                   Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                   ISBN.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
        }

    }
}
