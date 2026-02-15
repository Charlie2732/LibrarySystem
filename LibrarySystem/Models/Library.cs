using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Models;
namespace LibrarySystem.Models
{
    public class Library
    {
        // Listor för att lagra data
        public List<Book> Books { get; set; }
        public List<Member> Members { get; set; }
        public List<Loan> Loans { get; set; }

        // Konstruktor
        public Library()
        {
            Books = new List<Book>();
            Members = new List<Member>();
            Loans = new List<Loan>();
        }

        // === SÖKFUNKTION ===
        public List<Book> SearchBooks(string searchTerm)
        {
            var results = new List<Book>();

            foreach (var book in Books)
            {
                // Sök i titel, författare eller ISBN (case-insensitive)
                if (book.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    book.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    book.ISBN.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(book);
                }
            }

            return results;
        }

        // SORTERING
        public List<Book> SortBooksByTitle()
        {
            return Books.OrderBy(b => b.Title).ToList();
        }

        public List<Book> SortBooksByYear()
        {
            return Books.OrderBy(b => b.PublishedYear).ToList();
        }

        // STATISTIK
        public int GetTotalBooks()
        {
            return Books.Count;
        }

        public int GetBorrowedBooksCount()
        {
            return Loans.Count(l => l.IsReturned == false);
        }

        public Member GetMostActiveBorrower()
        {
            if (Members.Count == 0)
                return null;

            Member mostActive = Members[0];
            int maxLoans = 0;

            foreach (var member in Members)
            {
                int loanCount = member.BorrowedBooks.Count;
                if (loanCount > maxLoans)
                {
                    maxLoans = loanCount;
                    mostActive = member;
                }
            }

            return mostActive;
        }

        // UTLÅNING
        public bool BorrowBook(string isbn, string memberId)
        {
            // Hitta boken
            var book = Books.FirstOrDefault(b => b.ISBN == isbn);
            if (book == null || !book.IsAvailable)
                return false;

            // Hitta medlemmen
            var member = Members.FirstOrDefault(m => m.MemberId == memberId);
            if (member == null)
                return false;

            // Skapa lånet
            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));
            Loans.Add(loan);
            member.BorrowedBooks.Add(loan);
            book.IsAvailable = false;

            return true;
        }

        // RETURNERING
        public bool ReturnBook(string isbn)
        {
            // Hitta det aktiva lånet för boken
            var loan = Loans.FirstOrDefault(l => l.Book.ISBN == isbn && !l.IsReturned);
            if (loan == null)
                return false;

            // Returnera boken
            loan.ReturnDate = DateTime.Now;
            loan.Book.IsAvailable = true;

            return true;
        }
    }
}