using System;

namespace LibrarySystem.Core.Models
{
    public class Loan
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;

        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public bool IsOverdue
        {
            get
            {
                if (ReturnDate != null)
                    return false;

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

        public Loan()
        {
        }

        public Loan(Book book, Member member, DateTime loanDate, DateTime dueDate)
        {
            Book = book;
            Member = member;
            BookId = book.Id;
            MemberId = member.Id;
            LoanDate = loanDate;
            DueDate = dueDate;
            ReturnDate = null;
        }
    }
}