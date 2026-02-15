using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class Member
    {
        // Properties
        public string MemberId { get; }  // Kan bara sättas i konstruktorn
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime MemberSince { get; }  // Medlemsdatum ändras inte
        public List<Loan> BorrowedBooks { get; set; }  // Lista över lån

        // Konstruktor
        public Member(string memberId, string name, string email)
        {
            MemberId = memberId;
            Name = name;
            Email = email;
            MemberSince = DateTime.Now;  // Sätts till dagens datum automatiskt
            BorrowedBooks = new List<Loan>();  // Tom lista från början
        }

        // Metod för att visa medlemsinformation
        public string GetInfo()
        {
            return $"Medlem: {Name} (ID: {MemberId})\nEmail: {Email}\nMedlem sedan: {MemberSince:yyyy-MM-dd}\nAntal aktiva lån: {BorrowedBooks.Count}";
        }
    }
}