using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Models;

namespace LibrarySystem.Models
{
    public class Loan
    {
        // Properties
        public Book Book { get; set; }
        public Member Member { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }  // ? betyder att den kan vara null

        // Beräknade properties
        public bool IsOverdue
        {
            get
            {
                // Om boken är returnerad är den inte försenad
                if (ReturnDate != null)
                    return false;

                // Annars: kolla om dagens datum är efter DueDate
                return DateTime.Now > DueDate;
            }
        }

        public bool IsReturned
        {
            get
            {
                return ReturnDate != null;
            }
        }

        // Konstruktor
        public Loan(Book book, Member member, DateTime loanDate, DateTime dueDate)
        {
            Book = book;
            Member = member;
            LoanDate = loanDate;
            DueDate = dueDate;
            ReturnDate = null;  // Inte returnerad från början
        }
    }
}